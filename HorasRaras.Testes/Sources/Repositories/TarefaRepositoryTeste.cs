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

public class TarefaRepositoryTest
{
    private readonly Fixture _fixture;
    private readonly Mock<HorasRarasContext> _mockHorasRarasContext;

    public TarefaRepositoryTest()
    {
        _fixture = FixtureConfig.Get();
        _mockHorasRarasContext = new Mock<HorasRarasContext>(new DbContextOptionsBuilder<HorasRarasContext>().UseLazyLoadingProxies().Options);
    }


    [Fact(DisplayName = "Cadastrar um novo Tarefa")]
    public async Task Post()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<TarefaEntity>()).ReturnsDbSet(new List<TarefaEntity>());

        var repository = new TarefaRepository(_mockHorasRarasContext.Object);
        var exception = await Record.ExceptionAsync(() => repository.AddAsync(entity));
        
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Cadastrar um novo Tarefa")]
    public async Task PostNotNull()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<TarefaEntity>()).ReturnsDbSet(new List<TarefaEntity>());

        var repository = new TarefaRepository(_mockHorasRarasContext.Object);
        var exception = await Record.ExceptionAsync(() => repository.AddAsync(entity));

        Assert.NotNull(repository.AddAsync(entity));
    }


    [Fact(DisplayName = "Alterar um Tarefa existente")]
    public async Task Put()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<TarefaEntity>()).ReturnsDbSet(new List<TarefaEntity>());

        var repository = new TarefaRepository(_mockHorasRarasContext.Object);
        var exception = await Record.ExceptionAsync(() => repository.EditAsync(entity));
        Assert.Null(exception);
    }


    [Fact(DisplayName = "Listar todas os Tarefas")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<TarefaEntity>>();

        _mockHorasRarasContext.Setup(mock => mock.Set<TarefaEntity>()).ReturnsDbSet(entities);

        var repository = new TarefaRepository(_mockHorasRarasContext.Object);
        var response = await repository.ListAsync();

        Assert.True(response.Count() > 0);
    }

    [Fact(DisplayName = "Listar Tarefa por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<TarefaEntity>().FindAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var repository = new TarefaRepository(_mockHorasRarasContext.Object);

        var id = entity.Id;
        var response = await repository.FindAsync(id);

        Assert.Equal(response.Id, id);
    }


    [Fact(DisplayName = "Excluir um Tarefa existente")]
    public async Task Delete()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockHorasRarasContext.Setup(mock => mock.Set<TarefaEntity>()).ReturnsDbSet(new List<TarefaEntity>());

        var repository = new TarefaRepository(_mockHorasRarasContext.Object);

        var exception = await Record.ExceptionAsync(() => repository.RemoveAsync(entity));
        Assert.Null(exception);
    }
}