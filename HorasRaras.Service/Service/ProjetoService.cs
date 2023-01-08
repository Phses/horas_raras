using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Utils;
using System.Security.Claims;
using HorasRaras.Domain.Exceptions;
using HorasRaras.Domain.Contracts.Response;

namespace HorasRaras.Service;

public class ProjetoService : BaseService<ProjetoEntity>, IProjetoService
{
    private readonly IUsuarioProjetoRepository _usuarioProjetoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IProjetoRepository _projetoRepository;
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProjetoService(IUsuarioProjetoRepository usuarioProjetoRepository,
                                 IUsuarioRepository usuarioRepository,
                                 IProjetoRepository projetoRepository,
                                 ITarefaRepository tarefaRepository,
                                 IEmailService emailService,
                                 IHttpContextAccessor httpContextAccessor) : base(projetoRepository, httpContextAccessor)
    {
        _usuarioProjetoRepository = usuarioProjetoRepository;
        _usuarioRepository = usuarioRepository;
        _projetoRepository = projetoRepository;
        _emailService = emailService;
        _tarefaRepository = tarefaRepository;
        _httpContextAccessor = httpContextAccessor;
        
    }

    public async Task CriarProjetoAsync(ProjetoEntity entity, List<string> usuariosEmail)
    {
       
        var projeto = await _projetoRepository.FindAsync(projeto => projeto.Id == entity.Id && projeto.Ativo);
        if (projeto != null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Projeto inexistente");
        }

        await base.AdicionarAsync(entity);

        foreach (var usuarioEmail in usuariosEmail)
        {
            var usuario = await _usuarioRepository.FindAsync(usuario => usuario.Email == usuarioEmail 
                                                                        && usuario.EmailConfirmado 
                                                                        && usuario.Ativo);
            if (usuario == null)
            {
                //Se o usuario nao existir a iteracao continua
                //Envia email notificando que nao cadastrou
                _emailService.EnviaEmailErroCadastro(UserEmail, usuarioEmail);
                continue;
            }
            var usuarioProjeto = new UsuarioProjetoEntity()
            {
                UsuarioId = usuario.Id,
                ProjetoId = entity.Id,
            };
            await _usuarioProjetoRepository.AddAsync(usuarioProjeto);
        }
    }
    
    public async Task VincularUsuarioProjeto(int usuarioId, int projetoId)
    {
        // A vinculacao so podera ser feita por uma admin, essa restricao e definida na controller
        var usuario = await _usuarioRepository.FindAsync(usuario => usuario.Id == usuarioId && 
                                                                    usuario.EmailConfirmado && 
                                                                    usuario.Ativo);

        var projeto = await _projetoRepository.FindAsync(projeto => projeto.Id == projetoId && projeto.Ativo);
        if (usuario == null || projeto == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Usuario ou projeto n�o existe no db e a vincula��o n�o pode ser feita");
        }
        
        var vinculacao = new UsuarioProjetoEntity()
        {
            UsuarioId = usuarioId,
            ProjetoId = projetoId,
        };
        await _usuarioProjetoRepository.AddAsync(vinculacao);
    }

    public async Task<HorasTotaisResponse> ObterHorasTotaisAsync(int projetoId)
    {
        if(UserPerfil != ConstanteUtil.PerfilAdministradorNome)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "Usuario nao autorizado");
        }
        var find = await ObterPorIdAsync(projetoId);
        if (find == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Projeto nao existe na base de dados");
        }
        var list = await _tarefaRepository.ListAsync(x => x.ProjetoId == projetoId && x.Ativo);

        if (list.Count() == 0)
        {
            var totalHoras = new HorasTotaisResponse()
            {
                HorasTotais = 0
            };
            return totalHoras;
        }
        double total = 0.0;
        foreach (var tarefa in list)
        {
            if (tarefa.HorasTotal.HasValue)
            {
                total += tarefa.HorasTotal.Value;
            }
        }
        var response = new HorasTotaisResponse()
        {
            HorasTotais = total
        };
        return response;
    }

}