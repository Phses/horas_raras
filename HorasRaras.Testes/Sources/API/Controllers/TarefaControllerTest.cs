using AutoFixture;
using AutoMapper;
using HorasRaras.Api.Controllers;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Testes.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Controllers;

[Trait("Controller", "Controller de Tarefas")]
public class TarefaControllerTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<TarefaController>> _mockLogger;
    private readonly Mock<ITarefaService> _mockTarefaService;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public TarefaControllerTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockLogger = new Mock<ILogger<TarefaController>>();
        _mockTarefaService = new Mock<ITarefaService>();
        _fixture = FixtureConfig.Get();
        _mapper = MapConfig.Get();
    }


    [Fact(DisplayName = "Atualiza as horas finais de uma Tarefa")]
    public async Task Patch()
    {
        var request = _fixture.Create<TarefaRequest>();
        var tarefaHoraRequest = _fixture.Create<TarefaHoraRequest>();

        _mockTarefaService.Setup(mock => mock.AtualizarHoraFinalAsync(It.IsAny<int>(), It.IsAny<DateTime>())).Returns(Task.CompletedTask);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PatchAsync(request.ProjetoId, tarefaHoraRequest);
        var objectResult = Assert.IsType<OkResult>(response);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Cadastra uma nova Tarefa")]
    public async Task Post()
    {
        var request = _fixture.Create<TarefaRequest>();

        _mockTarefaService.Setup(mock => mock.AdicionarAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PostAsync(request);
        var objectResult = Assert.IsType<CreatedResult>(response);

        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Atualiza uma Tarefa existente")]
    public async Task Put()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<TarefaRequest>();

        _mockTarefaService.Setup(mock => mock.AlterarAsync(It.IsAny<TarefaEntity>())).Returns(Task.CompletedTask);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PutAsync(id, request);
        
        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca uma Tarefa por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<TarefaEntity>();

        _mockTarefaService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetByIdAsync(entity.Id);
        
        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var TarefaResponse = Assert.IsType<TarefaResponse>(objectResult.Value);

        Assert.Equal(TarefaResponse.Id, entity.Id);
    }


    [Fact(DisplayName = "Busca todas as Tarefas")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<TarefaEntity>>();

        _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var TarefaResponse = Assert.IsType<List<TarefaResponse>>(objectResult.Value);

        Assert.True(TarefaResponse.Count() > 0);
    }


    // [Fact(DisplayName = "Aciona aviso: tarefa nao finalizada")]
    // public async Task GetNaoFinalizada()
    // {
    //     var entities = _fixture.Create<List<TarefaEntity>>();
    //     var entitiesResponse = _mapper.Map<List<TarefaResponse>>(entities);
    //     var tarefaResponse = _fixture.Create<TarefaResponse>();
    //     DateTime? data = null;
    //     tarefaResponse.HoraFinal = data;

    //     var entitiesResponse = new List<TarefaResponse>();
    //     var entities = _mapper.Map<List<TarefaEntity>>(entitiesResponse);
            
    //     _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

    //     var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
    //     // var response = await controller.GetAsync();

    //     try
    //     {
    //         await controller.GetAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         Assert.Equal(ex.Message, "A tarefa não foi finalizada");
    //     }
    // }


    [Fact(DisplayName = "Busca todas as Tarefas realizadas dentro de uma semana")]
    public async Task ObterTarefasSemana()
    {
        var entities = _fixture.Create<List<TarefaEntity>>();

        _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.ObterTarefasSemana();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca todas as Tarefas realizadas dentro de um mes")]
    public async Task ObterTarefasEsteMes()
    {
        var entities = _fixture.Create<List<TarefaEntity>>();

        _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.ObterTarefasEsteMes();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca todas as Tarefas realizadas dentro de um dia")]
    public async Task ObterTarefasDia()
    {
        var entities = _fixture.Create<List<TarefaEntity>>();

        _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.ObterTarefasDia();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca todas as Tarefas relacionadas a um mesmo projeto")]
    public async Task ObterTarefasPorProjeto()
    {
        var entities = _fixture.Create<List<TarefaEntity>>();
        var projetoEntity = _fixture.Create<ProjetoEntity>();

        _mockTarefaService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.ObterTarefasPorProjeto(projetoEntity.Id);

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Remove uma Tarefa existente")]
    public async Task Delete()
    {
        var id = _fixture.Create<int>();

        _mockTarefaService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new TarefaController(_mockLogger.Object, _mapper, _mockTarefaService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

}