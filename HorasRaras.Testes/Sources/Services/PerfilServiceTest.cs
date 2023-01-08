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

[Trait("Service", "Service Perfil")]
public class PerfilServiceTest
{
    private readonly Mock<IPerfilRepository> _mockPerfilRepository;
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Fixture _fixture;
    private readonly Faker _faker;

    public PerfilServiceTest()
    {
        _mockPerfilRepository = new Mock<IPerfilRepository>();
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _fixture = FixtureConfig.Get();
        _faker = new Faker();
    }


    [Fact(DisplayName = "Cadastra um novo Perfil")]
    public async Task Post()
    {
        var entity = _fixture.Create<PerfilEntity>();

        _mockPerfilRepository.Setup(mock => mock.AddAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.AdicionarAsync(entity));
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Cadastra um novo Perfil")]
    public async Task PostWithTryCatch()
    {
        var entity = _fixture.Create<PerfilEntity>();

        _mockPerfilRepository.Setup(mock => mock.AddAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AdicionarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Atualiza um Perfil existente")]
    public async Task Put()
    {
        var entity = _fixture.Create<PerfilEntity>();

        _mockPerfilRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync(entity);
        _mockPerfilRepository.Setup(mock => mock.EditAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.Null(exception);
    }


    [Theory(DisplayName = "Atualiza um Perfil existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task PutWithTryCatch(string perfil)
    {
        var entity = _fixture.Create<PerfilEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockPerfilRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync(entity);
        _mockPerfilRepository.Setup(mock => mock.EditAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

        try
        {
            await service.AlterarAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Aciona uma exceção ao tentar atualizar um Perfil nulo")]
    public async Task PutPerfilNull()
    {
        var entity = new PerfilEntity();

        _mockPerfilRepository.Setup(mock => mock.FindAsNoTrackingAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync((PerfilEntity)null);
        _mockPerfilRepository.Setup(mock => mock.EditAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.AlterarAsync(entity));
        
        Assert.NotNull(exception);
    }


    [Theory(DisplayName = "Busca um Perfil por Id")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetById(string perfil)
    {
        var entity = _fixture.Create<PerfilEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockPerfilRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync(entity);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

        var response = await service.ObterPorIdAsync(entity.Id);

        Assert.Equal(response.Id, entity.Id);
    }


    [Theory(DisplayName = "Busca um Perfil por Id não existente")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetByIdInexistente(string perfil)
    {
        var id = _fixture.Create<int>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockPerfilRepository.Setup(mock => mock.FindAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync((PerfilEntity)null);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

        await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterPorIdAsync(id));
    }


    [Theory(DisplayName = "Busca todos Perfils")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task Get(string perfil)
    {
        var entities = _fixture.Create<List<PerfilEntity>>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockPerfilRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() > 0);
    }


    [Theory(DisplayName = "Busca uma lista de Perfils vazia")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task GetListaVazia(string perfil)
    {
        var entities = new List<PerfilEntity>();
        var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);

        _mockPerfilRepository.Setup(mock => mock.ListAsync(It.IsAny<Expression<Func<PerfilEntity, bool>>>())).ReturnsAsync(entities);
        _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);
        var response = await service.ObterTodosAsync();

        Assert.True(response.Count() == 0);
    }


    [Fact(DisplayName = "Remove um Perfil existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<PerfilEntity>();

        _mockPerfilRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync(entity);
        _mockPerfilRepository.Setup(mock => mock.RemoveAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);

        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(entity.Id));
        Assert.Null(exception);
    }
    
    
    [Fact(DisplayName = "Aciona uma exceção ao tentar remover um Perfil inexistente")]
    public async Task DeletePerfilNull()
    {
        var id = _fixture.Create<int>();

        _mockPerfilRepository.Setup(mock => mock.FindAsync(It.IsAny<int>())).ReturnsAsync((PerfilEntity)null);
        _mockPerfilRepository.Setup(mock => mock.RemoveAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var service = new PerfilService(_mockPerfilRepository.Object, _mockHttpContextAccessor.Object);
        var exception = await Record.ExceptionAsync(() => service.DeletarAsync(id));
        
        Assert.NotNull(exception);
    }

}