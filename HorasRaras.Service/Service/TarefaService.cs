using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using Microsoft.AspNetCore.Http;
using HorasRaras.Domain.Utils;
using Serilog;
using HorasRaras.Domain.Exceptions;
using HorasRaras.Domain.Interfaces.Service;
using System.Linq.Expressions;

namespace HorasRaras.Service;

public class TarefaService : BaseService<TarefaEntity>, ITarefaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IEmailService _emailService;
    private readonly IUsuarioProjetoRepository _usuarioProjetoRepository;
    private readonly IProjetoRepository _projetoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProjetoService _projetoService;

    public TarefaService(ITarefaRepository tarefaRepository, 
                         IUsuarioProjetoRepository usuarioProjetoRepository,
                         IProjetoRepository projetoRepository,
                         IUsuarioRepository usuarioRepository,
                         IHttpContextAccessor httpContextAccessor, 
                         IProjetoService projetoService, 
                         IEmailService emailService) : base(tarefaRepository, httpContextAccessor)
    {
        _tarefaRepository = tarefaRepository;
        _usuarioProjetoRepository = usuarioProjetoRepository;
        _httpContextAccessor = httpContextAccessor;
        _projetoRepository = projetoRepository;
        _usuarioRepository = usuarioRepository;
        _projetoService = projetoService;
        _emailService = emailService;

    }

    public override async Task<List<TarefaEntity>> ObterTodosAsync(Expression<Func<TarefaEntity, bool>> expression)
    {
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome)
        {
            var list = await _tarefaRepository.ListAsync(expression);
            var listRetorno = new List<TarefaEntity>();
            foreach(var tarefa in list)
            {
                if(tarefa.UsuarioId == UserId)
                {
                    listRetorno.Add(tarefa);
                }
            }
            return listRetorno;
        }
        return await _tarefaRepository.ListAsync(expression);
    }
   
    public override async Task DeletarAsync(int id)
    {
        var entity = await _tarefaRepository.FindAsync(id);
        if (entity == null) 
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome && UserId != entity.Id)
            throw new InformacaoException(Domain.Enums.StatusException.AcessoProibido, $"O usuário não tem permissão para deletar o Id {id} ");
        entity.UsuarioInclusao = UserId;
        entity.DataAlteracao = DateTime.Now;
        entity.Ativo = false;
        await _tarefaRepository.EditAsync(entity);
    }

    public override async Task<TarefaEntity> ObterPorIdAsync(int id)
    {
        if(UserPerfil == ConstanteUtil.PerfilColaboradorNome)
        {
            var entity = await _tarefaRepository.FindAsync(x => x.Id == id && 
                                                            x.UsuarioId == UserId 
                                                            && x.Ativo);
            if (entity == null)
                throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");
            return entity;
        } else
        {

            var entity = await _tarefaRepository.FindAsync(x => x.Id == id && x.Ativo);
            if (entity == null)
                throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");

            return entity;
        }
    }

    public override async Task<List<TarefaEntity>> ObterTodosAsync()
    {
        if(UserPerfil == ConstanteUtil.PerfilColaboradorNome)
        {
            return await _tarefaRepository.ListAsync(entity => entity.Ativo && UserId == entity.Id);
           
        }
        return await _tarefaRepository.ListAsync(entity => entity.Ativo);
    }

    public async Task AtualizarHoraFinalAsync(int id, DateTime horaFinal)
    {
        
        var entity = await ObterPorIdAsync(id);
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome && UserId != entity.Id)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "Usuario não tem permissão pra alterar esta tarefa");
        }
        entity.DataAlteracao = DateTime.Now;
        var tempoAlteracao = (entity.DataAlteracao - entity.DataInclusao).TotalHours;
        double limiteHorasAlteracao = 48;
        if (tempoAlteracao > limiteHorasAlteracao)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "Data de alteração expirada");
        }
        entity.HoraFinal = horaFinal;
        entity.HorasTotal = (entity.HoraFinal.Value - entity.HoraInicio).TotalHours;
        entity.UsuarioAlteracao = UserId;
        if (horaFinal.Hour < entity.HoraInicio.Hour)
        {
            throw new InformacaoException(Domain.Enums.StatusException.FormatoIncorreto, "A hora final não pode ser menor que a hora de inicio da tarefa");
        }
        
        await _tarefaRepository.EditAsync(entity);
    }

    public override async Task AlterarAsync(TarefaEntity entity)
    {
        var projeto = await _projetoRepository.FindAsNoTrackingAsync(x => x.Id == entity.ProjetoId && x.Ativo);
        var usuario = await _usuarioRepository.FindAsNoTrackingAsync(x => x.Id == entity.UsuarioId && x.Ativo);
        var usuarioProjeto = await _usuarioProjetoRepository.FindAsNoTrackingAsync(x => x.UsuarioId == entity.UsuarioId && x.ProjetoId == entity.ProjetoId);
        if (projeto == null || usuario == null || usuarioProjeto == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Dados de usuario e projeto não encontrados");
        }
        var find = await _tarefaRepository.FindAsNoTrackingAsync(x => x.Id == entity.Id && x.Ativo);
        if(find is null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Tarefa não existe no banco de dados");
        }
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome && UserId != entity.Id)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "N�o autorizado");
        }
        entity.DataAlteracao = DateTime.Now;
        var tempoAlteracao = (entity.DataAlteracao - find.DataInclusao).TotalHours;
        double limiteHorasAlteracao = 48;

        if (tempoAlteracao > limiteHorasAlteracao)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "Data de alteração expirada");
        }
             
        await _tarefaRepository.EditAsync(entity);
    }

    public override async Task AdicionarAsync(TarefaEntity entity)
    {
        if(UserPerfil == ConstanteUtil.PerfilColaboradorNome)
        {
            var vinculacaoProjeto = await _usuarioProjetoRepository.FindAsync(x => x.UsuarioId == UserId &&
                                                                                    x.ProjetoId == entity.ProjetoId);
            if(vinculacaoProjeto == null)
            {
                throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "Usuario não tem permissão para cadastrar uma tarefa neste projeto");
            }
        }
        entity.DataInclusao = DateTime.Now;
        if(entity.HoraFinal.HasValue)
        {
            entity.HorasTotal = (entity.HoraFinal.Value - entity.HoraInicio).TotalHours;
        }

        await _tarefaRepository.AddAsync(entity);
    }

    public async Task AdicionarTarefaTogglAsync(TarefaEntity entity, string projetoNome)
    {
        entity.UsuarioId = (int)UserId;
        var projetoEntity = await _projetoService.ObterAsync(p => p.Nome == projetoNome && p.Ativo);
        if(projetoEntity is null)
        {
            string descricao = $"O projeto {projetoNome} nao foi encontrado na nossa base de dados";
           _emailService.EnviaEmailErroIntegracao(UserEmail, descricao, entity.Descricao);
            return;
        }
        entity.ProjetoId = projetoEntity.Id;
        var vinculacaoProjeto = await _usuarioProjetoRepository.FindAsync(x => x.UsuarioId == UserId && 
                                                                                    x.ProjetoId == projetoEntity.Id);
        if(vinculacaoProjeto is null)
        {
            string descricao = $"O usuario {UserEmail} nao é um colaborador do {projetoNome}";
            _emailService.EnviaEmailErroIntegracao(UserEmail, descricao, entity.Descricao);
            return;
        }
        if(entity.HoraFinal.HasValue)
        {
            entity.HorasTotal = (entity.HoraFinal.Value - entity.HoraInicio).TotalHours;
        }
       
        await base.AdicionarAsync(entity);
    }

}