using AutoFixture;
using HorasRaras.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Bogus;
using Moq;
using Xunit;
using HorasRaras.Testes.Configs;
using HorasRaras.Domain.Entities;
using System.Linq.Expressions;
using HorasRaras.Service;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Exceptions;
using HorasRaras.Domain.Shared;
using HorasRaras.Domain.Contracts.Request;

namespace HorasRaras.Testes.Sources.Services;

[Trait("Service", "Service Usuario")]
public class UsuarioServiceTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Fixture _fixture;
    private readonly Faker _faker;
    public int UserId;

    public UsuarioServiceTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockEmailService = new Mock<IEmailService>();
        _fixture = FixtureConfig.Get();
        _faker = new Faker();
    }


    [Fact(DisplayName = "Criar um novo Usuario")]
    public async Task CriarUsuarioAsync()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.CriarUsuarioAsync(entity));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Aciona uma excecao ao tentar criar um novo Usuario administrador")]
    public async Task CriarUsuarioAsyncException()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        int idAdministrador = 1;
        entity.PerfilId = idAdministrador;

        _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.CriarUsuarioAsync(entity));
    }
    

    [Theory(DisplayName = "Autentica um Usuario existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task AutenticarAsync(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var senha = entity.Senha;
        var securityKey = Guid.NewGuid().ToString();
        entity.Senha = Cryptography.Encrypt(senha);

        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);
        
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(async () => await service.AutenticarAsync(entity.Email, senha));
        
        Assert.Null(exception);            
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar autenticar um Usuario inexistente")]
    public async Task AutenticarAsyncUsuarioNulo()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>()))
                                                                            .ReturnsAsync((UsuarioEntity)null);
        
        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AutenticarAsync(entity.Email, entity.Senha));
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar autenticar um Usuario passando uma senha incorreta")]
    public async Task AutenticarAsyncSenhaIncorreta()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var _senhaCriptografada = Cryptography.Encrypt("123456");
        
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        
        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AutenticarAsync(entity.Email, entity.Senha));
    }


    // CORRIGIR
    [Fact(DisplayName = "Confirma o email de um Usuario")]
    public async Task ConfirmarEmailAsyncUsuario()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockEmailService.Setup(mock => mock.EnviaEmailConfirmacaoAsync(entity.Email, entity.Nome, entity.HashEmailConfimacao));
        
        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        var exception = await Record.ExceptionAsync(() => service.ConfirmarEmailAsync(entity.HashEmailConfimacao));
        
        Assert.Null(exception);
    }
    
    
    [Fact(DisplayName = "Aciona uma exceção ao tentar confirmar o email de um Usuario inexistente")]
    public async Task ConfirmarEmailAsyncUsuarioNulo()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>()))
                                                                            .ReturnsAsync((UsuarioEntity)null);
        
        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ConfirmarEmailAsync(entity.HashEmailConfimacao));
    }

    
    [Fact(DisplayName = "Adiciona um novo Usuario")]
    public async Task AdicionarAsync()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AdicionarAsync(entity));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Adiciona um novo Usuario")]
    public async Task AdicionaAsyncWithTryCatch()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AdicionarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Theory(DisplayName = "Aciona uma excecao de acesso proibido quando um colaborador tenta adicionar um novo Usuario")]
    [InlineData("Colaborador")]
    public async Task AdicionarAsyncAcessoProibido(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);
    
        _mockUsuarioRepository.Setup(mock => mock.AddAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AdicionarAsync(entity));
    }


    [Theory(DisplayName = "Atualiza um Usuario existente")]
    [InlineData("Administrador")]
    public async Task AlterarAsync(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar atualizar um Usuario nulo")]
    public async Task AlterarAsyncNaoEncontrado()
    {
        var entity = new UsuarioEntity();

        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync((UsuarioEntity)null);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.NotNull(exception);
    }
    
    
    [Theory(DisplayName = "Aciona uma excecao quando um colaborador tenta editar um usuario")]
    [InlineData("Colaborador")]
    public async Task AlterarAsyncAcessoProibido(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AlterarAsync(entity));
    }


    [Theory(DisplayName = "Busca um Usuario por Id")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task ObterPorIdAsync(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        
        UserId = entity.Id;
        
        var claims = ClaimConfig.Get(UserId, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterPorIdAsync(entity.Id);

        Assert.Equal(response.Id, entity.Id);
    }


    [Fact(DisplayName = "Remove uma Usuario existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.RemoveAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(entity.Id));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar remover um Usuario inexistente")]
    public async Task DeleteUsuarioNaoEncontrado()
    {
        var id = _fixture.Create<int>();

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((UsuarioEntity)null);
        _mockUsuarioRepository.Setup(mock => mock.RemoveAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(id));
        
        Assert.NotNull(exception);
    }


    [Theory(DisplayName = "Aciona uma exceção quando um colaborador sem permissao stenta remover um Usuario")]
    [InlineData("Colaborador")]
    public async Task DeleteAcessoProibido(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.RemoveAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(entity.Id));
    }


    [Theory(DisplayName = "Busca todas Usuarios")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task ObterTodosAsync(string perfil)
    {
        var entities = _fixture.Create<List<UsuarioEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() > 0);
    }


    [Theory(DisplayName = "Busca uma lista de Usuarios vazia")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task ObterTodosAsyncListaVaxia(string perfil)
    {
        var entities = new List<UsuarioEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() == 0);
    }


    [Theory(DisplayName = "Busca um Usuario por Id não existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task ObterPorIdAsyncInexistente(string perfil)
    {
        var id = _fixture.Create<int>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync((UsuarioEntity)null);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(id));
    }


    // Esta excecao so ocorrer para colaborador, pois admin esta autorizado a buscar por qualquer id.
    [Theory(DisplayName = "Busca um Usuario por Id")]
    [InlineData("Colaborador")]
    public async Task ObterPorIdAsyncNaoAutorizado(string perfil)
    {
        var entity = _fixture.Create<UsuarioEntity>();
        
        UserId = 0;
        
        var claims = ClaimConfig.Get(UserId, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(entity.Id));
    }


    [Fact(DisplayName = "Atualiza a HashSenha de um Usuario existente")]
    public async Task AterarHashSenhaAsync()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        entity.HashSenha = Token.GenerateRandomToken();
        entity.HashSenhaExpiracao = DateTime.Now.AddHours(2);

        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AterarHashSenhaAsync(entity.Email));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Aciona uma excecao ao tentar atualizar a HashSenha de um Usuario nulo")]
    public async Task AterarHashSenhaAsyncUsuarioNulo()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        entity.HashSenha = Token.GenerateRandomToken();
        entity.HashSenhaExpiracao = DateTime.Now.AddHours(2);

        
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync((UsuarioEntity)null);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AterarHashSenhaAsync(entity.Email));
    }
    

    [Fact(DisplayName = "Atualiza a senha de um Usuario existente")]
    public async Task AlterarSenhaAsync()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var novaSenhaRequest = new NovaSenhaRequest()
        {
            NovaSenha = "1234Teste",
            NovaSenhaConfirmacao = "1234Teste"
        };

        entity.HashSenhaExpiracao = DateTime.Now.AddHours(2);

        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarSenhaAsync(entity.HashSenha, novaSenhaRequest));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Aciona uma excecao ao tentar atualizar a senha de um Usuario nulo")]
    public async Task AlterarSenhaAsyncFormatoIncorreto()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var novaSenha = _fixture.Create<NovaSenhaRequest>();
        novaSenha.NovaSenha = "123456";

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AlterarSenhaAsync(entity.HashSenha, novaSenha));
    }


    [Fact(DisplayName = "Aciona uma excecao ao tentar atualizar a senha de um Usuario nulo")]
    public async Task AlterarSenhaAsyncUsuarioNulo()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        var novaSenha = _fixture.Create<NovaSenhaRequest>();

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync((UsuarioEntity)null);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AlterarSenhaAsync(entity.HashSenha, novaSenha));
    }
    

    [Fact(DisplayName = "Aciona uma excecao ao tentar atualizar a senha quando o token já expirou")]
    public async Task AlterarSenhaAsyncTokenExpirado()
    {
        var entity = _fixture.Create<UsuarioEntity>();
        entity.HashSenhaExpiracao = DateTime.Now.AddHours(-50);

        var novaSenha = _fixture.Create<NovaSenhaRequest>();
        novaSenha.NovaSenha = novaSenha.NovaSenhaConfirmacao;

        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(entity);
        _mockUsuarioRepository.Setup(mock => mock.EditAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var service = new UsuarioService(_mockUsuarioRepository.Object, _mockEmailService.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AlterarSenhaAsync(entity.HashSenha, novaSenha));
    }

    

}

