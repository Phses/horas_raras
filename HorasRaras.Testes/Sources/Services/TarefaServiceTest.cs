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
using HorasRaras.Domain.Exceptions;
using HorasRaras.Domain.Interfaces.Service;

namespace HorasRaras.Testes.Sources.Services;

[Trait("Service", "Service Tarefa")]
public class TarefaServiceTest
{
    private readonly Mock<IUsuarioProjetoRepository> _mockUsuarioProjetoRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<IProjetoRepository> _mockProjetoRepository;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly Mock<ITarefaRepository> _mockTarefaRepository;
    private readonly Mock<IProjetoService> _mockProjetoService;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Fixture _fixture;
    private readonly Faker _faker;

    public TarefaServiceTest()
    {
        _mockUsuarioProjetoRepository = new Mock<IUsuarioProjetoRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockProjetoRepository = new Mock<IProjetoRepository>();
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockTarefaRepository = new Mock<ITarefaRepository>();
        _mockProjetoService = new Mock<IProjetoService>();
        _mockEmailService = new Mock<IEmailService>();
        _fixture = FixtureConfig.Get();
        _faker = new Faker();
    }


    [Theory(DisplayName = "Cadastra uma nova Tarefa")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task Post(string perfil)
    {
        var entity = _fixture.Create<TarefaEntity>();
        entity.HoraInicio = DateTime.Today;
        entity.HoraFinal = DateTime.Today;

        _mockTarefaRepository.Setup(mock => mock.AddAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);

        var exception = await Record.ExceptionAsync(() => service.AdicionarAsync(entity));
        
        Assert.Null(exception);
    }


    // Esta exceçao so ocorre com colaborador, visto que administrador pode cadastrar qualquer tarefa.
    [Theory(DisplayName = "Aciona um InformacaoException NaoAutorizado quando um colaborador tentar cadastrar uma tarefa em que não está vinculado")]
    [InlineData("Colaborador")]
    public async Task PostException(string perfil)
    {
        var entity = _fixture.Create<TarefaEntity>();

        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockUsuarioProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioProjetoEntity, bool>>>()))
                                                        .ReturnsAsync((UsuarioProjetoEntity)null);

        _mockTarefaRepository.Setup(mock => mock.AddAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AdicionarAsync(entity));
    }


    [Theory(DisplayName = "Atualiza uma Tarefa existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task Put(string perfil)
    {
        var projeto = _fixture.Create<ProjetoEntity>();
        var usuario = _fixture.Create<UsuarioEntity>();
        var usuarioProjeto = _fixture.Create<UsuarioProjetoEntity>();
        var entity = _fixture.Create<TarefaEntity>();
        entity.DataInclusao = DateTime.Now;
        entity.DataAlteracao = DateTime.Now;

        var UserId = entity.Id;

        var claims = ClaimConfig.Get(UserId, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(projeto);
        _mockUsuarioRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(usuario);
        _mockUsuarioProjetoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<UsuarioProjetoEntity, bool>>>())).ReturnsAsync(usuarioProjeto);
        _mockTarefaRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.EditAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar atualizar uma Tarefa nula")]
    public async Task PutTarefaNull()
    {
        var entity = new TarefaEntity();
        entity.DataInclusao = DateTime.Now;
        entity.DataAlteracao = DateTime.Now;

        _mockTarefaRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync((TarefaEntity)null);
        _mockTarefaRepository.Setup(mock => mock.EditAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.NotNull(exception);
    }


    [Theory(DisplayName = "Aciona ArgumentException quando o colaborador tenta atualizar uma Tarefa (apenas Admin pode atualizar)")]
    [InlineData("Colaborador")]
    public async Task PutAcionandoExceptionNaoAutorizado(string perfil)
    {
        var entity = _fixture.Create<TarefaEntity>();
        entity.DataInclusao = DateTime.Now;
        entity.DataAlteracao = DateTime.Now;

        entity.Id = 1;
        var UserId = 0;

        var claims = ClaimConfig.Get(UserId, _faker.Person.FullName, _faker.Person.Email, perfil);        

        _mockTarefaRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.EditAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AlterarAsync(entity));
    }


    [Theory(DisplayName = "Aciona ArgumentException ao tentar atualizar uma Tarefa com data expirada")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task PutAcionandoExceptionDataExpirada(string perfil)
    {
        var entity = _fixture.Create<TarefaEntity>();
        entity.DataAlteracao = DateTime.Now; 
        entity.DataInclusao = entity.DataAlteracao.AddHours(-50);

        var UserId = entity.Id;

        var claims = ClaimConfig.Get(UserId, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockTarefaRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.EditAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.AlterarAsync(entity));
    }


    [Theory(DisplayName = "Busca uma Tarefa por Id")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetById(string perfil)
    {
        var entity = _fixture.Create<TarefaEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);

        var response = await service.ObterPorIdAsync(entity.Id);

        Assert.Equal(response.Id, entity.Id);
    }


    [Theory(DisplayName = "Busca uma Tarefa por Id não existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetByIdInexistente(string perfil)
    {
        var id = _fixture.Create<int>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync((TarefaEntity)null);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(id));
    }


    [Theory(DisplayName = "Busca todas Tarefas")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task Get(string perfil)
    {
        var entities = _fixture.Create<List<TarefaEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() > 0);
    }


    [Theory(DisplayName = "Busca uma lista de Tarefas vazia")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetListaVazia(string perfil)
    {
        var entities = new List<TarefaEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() == 0);
    }


    [Fact(DisplayName = "Remove uma Tarefa existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.RemoveAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);

        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(entity.Id));
        Assert.Null(exception);
    }
    
    
    [Fact(DisplayName = "Aciona uma exceção ao tentar remover uma Tarefa inexistente")]
    public async Task DeleteTarefaNull()
    {
        var id = _fixture.Create<int>();

        _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((TarefaEntity)null);
        _mockTarefaRepository.Setup(mock => mock.RemoveAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.DeletarAsync(id));
    }
    
    
    [Theory(DisplayName = "Aciona uma InformacaoException NaoAutorizado quando um colaborador nao vinculado ao projeto tenta remover uma Tarefa")]
    [InlineData("Colaborador")]
    public async Task DeleteTarefaNaoAutorizado(string perfil)
    {
        var entity = _fixture.Create<TarefaEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockTarefaRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.RemoveAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new TarefaService(_mockTarefaRepository.Object, 
                                        _mockUsuarioProjetoRepository.Object,
                                        _mockProjetoRepository.Object, 
                                        _mockUsuarioRepository.Object,
                                        _mockHttpContextAccessor.Object, 
                                        _mockProjetoService.Object, 
                                        _mockEmailService.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.DeletarAsync(entity.Id));
    }
}

