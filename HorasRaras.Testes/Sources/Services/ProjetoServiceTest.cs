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

namespace HorasRaras.Testes.Sources.Services;

[Trait("Service", "Service Projeto")]
public class ProjetoServiceTest
{
    private readonly Mock<IUsuarioProjetoRepository> _mockUsuarioProjetoRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly Mock<IProjetoRepository> _mockProjetoRepository;
    private readonly Mock<ITarefaRepository> _mockTarefaRepository;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Fixture _fixture;
    private readonly Faker _faker;

    public ProjetoServiceTest()
    {
        _mockUsuarioProjetoRepository = new Mock<IUsuarioProjetoRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockProjetoRepository = new Mock<IProjetoRepository>();
        _mockTarefaRepository = new Mock<ITarefaRepository>();
        _mockEmailService = new Mock<IEmailService>();
        _fixture = FixtureConfig.Get();
        _faker = new Faker();
    }


    [Fact(DisplayName = "Cadastra um novo Projeto")]
    public async Task Post()
    {
        var entity = _fixture.Create<ProjetoEntity>();

        _mockProjetoRepository.Setup(mock => mock.AddAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.AdicionarAsync(entity));
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Cadastra um novo Projeto")]
    public async Task PostWithTryCatch()
    {
        var entity = _fixture.Create<ProjetoEntity>();

        _mockProjetoRepository.Setup(mock => mock.AddAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AdicionarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    // OBSERVAR METODO CriarProjetoAsync EM PROJETOSERVICE
    [Fact(DisplayName = "Cria um novo Projeto")]
    public async Task CriarProjetoAsync()
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var usuarioProjeto = _fixture.Create<UsuarioProjetoEntity>();
        var usuariosEntities = _fixture.Create<List<string>>();

        _mockProjetoRepository.Setup(mock => mock.AddAsync(entity)).Returns(Task.CompletedTask);
        _mockUsuarioProjetoRepository.Setup(mock => mock.AddAsync(usuarioProjeto)).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.CriarProjetoAsync(entity, usuariosEntities));
        Assert.Null(exception);
    }


    // OBSERVAR METODO CriarProjetoAsync EM PROJETOSERVICE
    [Fact(DisplayName = "Aciona uma excecao ao tentar cadastrar projeto existente")]
    public async Task CriarProjetoAsyncException()
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var usuariosEntities = _fixture.Create<List<string>>();

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entity);
        // _mockProjetoRepository.Setup(mock => mock.AddAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                _mockEmailService.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.CriarProjetoAsync(entity, usuariosEntities));
    }


    [Fact(DisplayName = "Vincula um usuario a um Projeto")]
    public async Task VincularUsuarioProjeto()
    {
        var usuarioEntity = _fixture.Create<UsuarioEntity>();
        var projetoEntity = _fixture.Create<ProjetoEntity>();

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(projetoEntity);
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<UsuarioEntity, bool>>>())).ReturnsAsync(usuarioEntity);
        _mockUsuarioProjetoRepository.Setup(mock => mock.AddAsync(It.IsAny<UsuarioProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.VincularUsuarioProjeto(usuarioEntity.Id, projetoEntity.Id));
        
        Assert.Null(exception);
    }


    // OBSERVAR METODO VincularUsuarioProjeto EM PROJETOSERVICE
    [Fact(DisplayName = "Aciona uma excecao ao tentat vincular um usuario nulo a um Projeto nulo")]
    public async Task VincularUsuarioProjetoException()
    {
        var usuarioEntity = _fixture.Create<UsuarioEntity>();
        var projetoEntity = _fixture.Create<ProjetoEntity>();

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((ProjetoEntity)null);
        _mockUsuarioRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((UsuarioEntity)null);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                _mockEmailService.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.VincularUsuarioProjeto(usuarioEntity.Id, projetoEntity.Id));
    }


    // Apenas administradores são autorizados a fazer esse tipo de consulta.
    [Theory(DisplayName = "Obtem horas totais de um projeto")]
    [InlineData("Administrador")]
    public async Task ObterHorasTotaisAsync(string perfil)
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var entities = _fixture.Create<List<TarefaEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.ObterHorasTotaisAsync(entity.Id));
        
        Assert.Null(exception);
    }


    // Apenas administradores são autorizados a fazer esse tipo de consulta.
    [Theory(DisplayName = "Aciona uma excecao quando um colaborador tenta obter as horas totais de um projeto")]
    [InlineData("Colaborador")]
    public async Task ObterHorasTotaisAsyncNaoAutorizado(string perfil)
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var entities = _fixture.Create<List<TarefaEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entity);
        _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterHorasTotaisAsync(entity.Id));
    }
    
    
    [Theory(DisplayName = "Aciona uma excecao ao tentar obter as horas de um projeto nao existente")]
    [InlineData("Administrador")]
    public async Task ObterHorasTotaisAsyncNaoEncontrado(string perfil)
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var entities = _fixture.Create<List<TarefaEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync((ProjetoEntity)null);
        _mockTarefaRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<TarefaEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterHorasTotaisAsync(entity.Id));
    }
    

    [Fact(DisplayName = "Atualiza um Projeto existente")]
    public async Task Put()
    {
        var entity = _fixture.Create<ProjetoEntity>();

        _mockProjetoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entity);
        _mockProjetoRepository.Setup(mock => mock.EditAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.Null(exception);
    }


    [Theory(DisplayName = "Atualiza um Projeto existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task PutWithTryCatch(string perfil)
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entity);
        _mockProjetoRepository.Setup(mock => mock.EditAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);
        try
        {
            await service.AlterarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar atualizar um Projeto nulo")]
    public async Task PutProjetoNull()
    {
        var entity = new ProjetoEntity();

        _mockProjetoRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync((ProjetoEntity)null);
        _mockProjetoRepository.Setup(mock => mock.EditAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.NotNull(exception);
    }


    [Theory(DisplayName = "Busca um Projeto por Id")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetById(string perfil)
    {
        var entity = _fixture.Create<ProjetoEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var response = await service.ObterPorIdAsync(entity.Id);

        Assert.Equal(response.Id, entity.Id);
    }


    [Theory(DisplayName = "Busca um Projeto por Id não existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetByIdInexistente(string perfil)
    {
        var id = _fixture.Create<int>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync((ProjetoEntity)null);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(id));
    }


    [Theory(DisplayName = "Busca todos Projetos")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task Get(string perfil)
    {
        var entities = _fixture.Create<List<ProjetoEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() > 0);
    }


    [Theory(DisplayName = "Busca uma lista de Projetos vazia")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetListaVazia(string perfil)
    {
        var entities = new List<ProjetoEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockProjetoRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<ProjetoEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() == 0);
    }


    [Fact(DisplayName = "Remove um Projeto existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<ProjetoEntity>();

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockProjetoRepository.Setup(mock => mock.RemoveAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(entity.Id));
        Assert.Null(exception);
    }
    
    
    [Fact(DisplayName = "Aciona uma exceção ao tentar remover um Projeto inexistente")]
    public async Task DeleteProjetoNull()
    {
        var id = _fixture.Create<int>();

        _mockProjetoRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((ProjetoEntity)null);
        _mockProjetoRepository.Setup(mock => mock.RemoveAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var service = new ProjetoService(_mockUsuarioProjetoRepository.Object, _mockUsuarioRepository.Object,
                                        _mockProjetoRepository.Object, _mockTarefaRepository.Object,
                                        _mockEmailService.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(id));
        
        Assert.NotNull(exception);
    }

}