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

namespace HorasRaras.Testes.Sources.Services;

[Trait("Service", "Service Cliente")]
public class ClienteServiceTest
{
    private readonly Mock<IClienteRepository> _mockClienteRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Fixture _fixture;
    private readonly Faker _faker;

    public ClienteServiceTest()
    {
        _mockClienteRepository = new Mock<IClienteRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _fixture = FixtureConfig.Get();
        _faker = new Faker();
    }


    [Fact(DisplayName = "Cadastra um novo Cliente")]
    public async Task Post()
    {
        var entity = _fixture.Create<ClienteEntity>();

        _mockClienteRepository.Setup(mock => mock.AddAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.AdicionarAsync(entity));
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Cadastra um novo Cliente")]
    public async Task PostWithTryCatch()
    {
        var entity = _fixture.Create<ClienteEntity>();

        _mockClienteRepository.Setup(mock => mock.AddAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AdicionarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Atualiza um Cliente existente")]
    public async Task Put()
    {
        var entity = _fixture.Create<ClienteEntity>();

        _mockClienteRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync(entity);
        _mockClienteRepository.Setup(mock => mock.EditAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.Null(exception);
    }


    [Theory(DisplayName = "Atualiza um Cliente existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task PutWithTryCatch(string perfil)
    {
        var entity = _fixture.Create<ClienteEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockClienteRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync(entity);
        _mockClienteRepository.Setup(mock => mock.EditAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AlterarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar atualizar um Cliente nulo")]
    public async Task PutClienteNull()
    {
        var entity = new ClienteEntity();

        _mockClienteRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync((ClienteEntity)null);
        _mockClienteRepository.Setup(mock => mock.EditAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.NotNull(exception);
    }


    [Theory(DisplayName = "Busca um Cliente por Id")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetById(string perfil)
    {
        var entity = _fixture.Create<ClienteEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockClienteRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.ObterPorIdAsync(entity.Id);

        Assert.Equal(response.Id, entity.Id);
    }


    [Theory(DisplayName = "Busca um Cliente por Id não existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetByIdInexistente(string perfil)
    {
        var id = _fixture.Create<int>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockClienteRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync((ClienteEntity)null);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);
        
        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(id));
    }


    [Theory(DisplayName = "Busca todos Clientes")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task Get(string perfil)
    {
        var entities = _fixture.Create<List<ClienteEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockClienteRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() > 0);
    }


    [Theory(DisplayName = "Busca uma lista de Clientes vazia")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetListaVazia(string perfil)
    {
        var entities = new List<ClienteEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockClienteRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<ClienteEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() == 0);
    }


    [Fact(DisplayName = "Remove um Cliente existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<ClienteEntity>();

        _mockClienteRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockClienteRepository.Setup(mock => mock.RemoveAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(entity.Id));
        Assert.Null(exception);
    }
    
    
    [Fact(DisplayName = "Aciona uma exceção ao tentar remover um Cliente inexistente")]
    public async Task DeleteClienteNull()
    {
        var id = _fixture.Create<int>();

        _mockClienteRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((ClienteEntity)null);
        _mockClienteRepository.Setup(mock => mock.RemoveAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var service = new ClienteService(_mockClienteRepository.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(id));
        
        Assert.NotNull(exception);
    }

}