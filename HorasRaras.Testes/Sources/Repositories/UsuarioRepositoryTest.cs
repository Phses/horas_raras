using Microsoft.EntityFrameworkCore;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Repository;
using HorasRaras.Testes.Configs;
using HorasRaras.Data.Context;
using Moq.EntityFrameworkCore;
using AutoFixture;
using Xunit;
using Moq;


namespace HorasRaras.Testes.Sources.Repositories;

public class UsuarioRepositoryTest
{
    private readonly Fixture _fixture;
    private readonly Mock<HorasRarasContext> _mockHorasRarasContext;

    public UsuarioRepositoryTest()
    {
        _fixture = FixtureConfig.Get();
        _mockHorasRarasContext = new Mock<HorasRarasContext>(new DbContextOptionsBuilder<HorasRarasContext>().UseLazyLoadingProxies().Options);
    }


    [Fact(DisplayName = "Cadastrar um novo Usuario")]
    public async Task Post()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity>());

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);
        var exception = await Record.ExceptionAsync(() => repository.AddAsync(entity));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Cadastrar um novo Usuario")]
    public async Task PostNotNull()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity>());

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);

        Assert.NotNull(repository.AddAsync(entity));
    }


    [Fact(DisplayName = "Cadastra um novo Usuario")]
    public async Task PostWithException()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity> { new UsuarioEntity() });

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);

        try
        {
            await repository.AddAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Alterar um Usuario existente")]
    public async Task Put()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity>());

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);
        var exception = await Record.ExceptionAsync(() => repository.EditAsync(entity));
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Edita Usuario Existente")]
    public async Task PutWithException()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity> { new UsuarioEntity() });

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);

        try
        {
            await repository.EditAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }


    [Fact(DisplayName = "Listar todas os Usuarios")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<UsuarioEntity>>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(entities);

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);
        var response = await repository.ListAsync();

        Assert.True(response.Count() > 0);
    }


    [Fact(DisplayName = "Listar Usuario por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);

        var id = entity.Id;
        var response = await repository.FindAsync(id);

        Assert.Equal(response.Id, id);
    }


    [Fact(DisplayName = "Excluir um Usuario existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity>());

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);

        var exception = await Record.ExceptionAsync(() => repository.RemoveAsync(entity));
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Remove Usuario Existente")]
    public async Task DeleteWithException()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<UsuarioEntity>()).ReturnsDbSet(new List<UsuarioEntity> { entity });

        var repository = new UsuarioRepository(_mockHorasRarasContext.Object);

        try
        {
            await repository.RemoveAsync(entity);
        }
        catch (Exception)
        {
            Assert.True(false);
        }
    }
}