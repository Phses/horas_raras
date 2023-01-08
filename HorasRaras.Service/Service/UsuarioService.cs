using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Shared;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.SymbolStore;
using HorasRaras.Domain.Contracts.Request;
using System.Net.Mail;
using Serilog.Context;
using Serilog;
using HorasRaras.Domain.Exceptions;

namespace HorasRaras.Service;

public class UsuarioService : BaseService<UsuarioEntity>, IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IEmailService _emailService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioService(IUsuarioRepository usuarioRepository, IEmailService emailService,
        IHttpContextAccessor httpContextAccessor) : base(usuarioRepository, httpContextAccessor)
    {
        _usuarioRepository = usuarioRepository;
        _emailService = emailService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CriarUsuarioAsync(UsuarioEntity usuario)
    {
        int idAdministrador = 1;
        if(usuario.PerfilId == idAdministrador)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, "Não é possível cadastrar um admin por esta rota");
        }
        usuario.Senha = Cryptography.Encrypt(usuario.Senha);
        usuario.DataInclusao = DateTime.Now;
        usuario.EmailConfirmado = false;

        _emailService.EnviaEmailConfirmacaoAsync(usuario.Email, usuario.Nome, usuario.HashEmailConfimacao);
        await _usuarioRepository.AddAsync(usuario);

    }

    public async Task<string> AutenticarAsync(string email, string senha)
    {
        var entity = await ObterAsync(x => x.Email.Equals(email) && x.Ativo && x.EmailConfirmado);

        if (entity == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.FormatoIncorreto, "Usuário ou email incorreto");
        }

        var _senhaCriptografada = Cryptography.Encrypt(senha);

        if (entity.Senha != _senhaCriptografada)
        {
            throw new InformacaoException(Domain.Enums.StatusException.FormatoIncorreto, "Senha incorreta");
        }

        return Token.GenerateToken(entity);
    }

    public async Task ConfirmarEmailAsync(Guid hashConfirmacao)
    {
        var entity = await ObterAsync(x => x.HashEmailConfimacao == hashConfirmacao && x.Ativo);

        if (entity == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.FormatoIncorreto, "hash de confirmacao não confere");
        }

        entity.EmailConfirmado = true;
        await AlterarAsync(entity);

    }
    public override async Task AdicionarAsync(UsuarioEntity entity)
    {
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome)
            throw new InformacaoException(Domain.Enums.StatusException.AcessoProibido, $"O usuário não tem permissão para cadastrar um usuario nesta rota");
        entity.Senha = Cryptography.Encrypt(entity.Senha);
        entity.DataInclusao = DateTime.Now;
        entity.UsuarioInclusao = UserId;
        _emailService.EnviaEmailConfirmacaoAsync(entity.Email, entity.Nome, entity.HashEmailConfimacao);
        await _usuarioRepository.AddAsync(entity);
    }
    public override async Task AlterarAsync(UsuarioEntity entity)
    {
        var find = await _usuarioRepository.FindAsNoTrackingAsync(x => x.Id == entity.Id && x.Ativo);
        if (find == null)
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {entity.Id}");
        if(UserPerfil == ConstanteUtil.PerfilColaboradorNome && UserId != entity.Id)
            throw new InformacaoException(Domain.Enums.StatusException.AcessoProibido, $"O usuário não tem permissão para alterar o Id {entity.Id} ");
        //Nao permite que um usuario mude seu perfil
        entity.PerfilId = find.PerfilId;
        entity.DataInclusao = find.DataInclusao;
        entity.DataAlteracao = DateTime.Now;
        entity.UsuarioAlteracao = UserId;
        await _usuarioRepository.EditAsync(entity);
    }
    public override async Task DeletarAsync(int id)
    {
        var entity = await _usuarioRepository.FindAsync(id);
        if (entity == null)
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome && UserId != entity.Id)
            throw new InformacaoException(Domain.Enums.StatusException.AcessoProibido, $"O usuário não tem permissão para deletar o Id {id} ");
        entity.UsuarioAlteracao = UserId;
        entity.DataAlteracao = DateTime.Now;
        entity.Ativo = false;
        await _usuarioRepository.EditAsync(entity);
    }
    public override async Task<List<UsuarioEntity>> ObterTodosAsync()
    {
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome)
            return await _usuarioRepository.ListAsync(x => x.Ativo && x.Id == UserId && x.EmailConfirmado);
        else
            return await _usuarioRepository.ListAsync(x => x.Ativo);
    }

    public override async Task<UsuarioEntity> ObterPorIdAsync(int id)
    {
        var entity = await _usuarioRepository.FindAsync(x => x.Id == id && x.Ativo && x.EmailConfirmado);
        if (entity == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, $"Nenhum dado encontrado para o Id {id}");
        }
        if (UserPerfil == ConstanteUtil.PerfilColaboradorNome && UserId != entity.Id)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoAutorizado, $"Usuario nao autorizado");
        }

        return entity;
    }

    public async Task AterarHashSenhaAsync(string email)
    {
        //Estamos usando entity aqui e x em outros trechos
        var entity = await ObterAsync(entity => entity.Email == email && entity.Ativo && entity.EmailConfirmado);
        if (entity == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.FormatoIncorreto, "Email não encontrado na base de dados");
        }
        entity.HashSenha = Token.GenerateRandomToken();
        entity.HashSenhaExpiracao = DateTime.Now.AddHours(2);

        _emailService.EnviaEmailSenha(entity.Email, entity.Nome, entity.HashSenha);
              
        await AlterarAsync(entity);

    }
    public async Task AlterarSenhaAsync(string hashSenha, NovaSenhaRequest novaSenha)
    {
        if (novaSenha.NovaSenha != novaSenha.NovaSenhaConfirmacao)
        {
            throw new InformacaoException(Domain.Enums.StatusException.FormatoIncorreto, "Senhas não coferem");
        }
        //manter ou trocar o x
        var entity = await ObterAsync(x => hashSenha == x.HashSenha && x.Ativo && x.EmailConfirmado);
        if (entity == null)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Usuario não encontrado");
        }
        if (entity.HashSenhaExpiracao < DateTime.Now)
        {
            throw new InformacaoException(Domain.Enums.StatusException.NaoEncontrado, "Token expirado");
        }

        entity.Senha = Cryptography.Encrypt(entity.Senha);
        await AlterarAsync(entity);
    }
}