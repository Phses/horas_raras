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

[Trait("Controller", "Controller de Projetos")]
public class ProjetoControllerTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<ProjetoController>> _mockLogger;
    private readonly Mock<IProjetoService> _mockProjetoService;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public ProjetoControllerTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockLogger = new Mock<ILogger<ProjetoController>>();
        _mockProjetoService = new Mock<IProjetoService>();
        _fixture = FixtureConfig.Get();
        _mapper = MapConfig.Get();
    }


    [Fact(DisplayName = "Cadastra um novo Projeto")]
    public async Task Post()
    {
        var request = _fixture.Create<ProjetoRequest>();

        _mockProjetoService.Setup(mock => mock.AdicionarAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var controller = new ProjetoController(_mockLogger.Object, _mapper, _mockProjetoService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PostAsync(request);
        var objectResult = Assert.IsType<CreatedResult>(response);

        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Atualiza um Projeto existente")]
    public async Task Put()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<ProjetoRequest>();

        _mockProjetoService.Setup(mock => mock.AlterarAsync(It.IsAny<ProjetoEntity>())).Returns(Task.CompletedTask);

        var controller = new ProjetoController(_mockLogger.Object, _mapper, _mockProjetoService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PutAsync(id, request);
        
        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca um Projeto por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<ProjetoEntity>();

        _mockProjetoService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var controller = new ProjetoController(_mockLogger.Object, _mapper, _mockProjetoService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetByIdAsync(entity.Id);
        
        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var ProjetoResponse = Assert.IsType<ProjetoResponse>(objectResult.Value);

        Assert.Equal(ProjetoResponse.Id, entity.Id);
    }


    [Fact(DisplayName = "Busca todos os Projetos")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<ProjetoEntity>>();

        _mockProjetoService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new ProjetoController(_mockLogger.Object, _mapper, _mockProjetoService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var ProjetoResponse = Assert.IsType<List<ProjetoResponse>>(objectResult.Value);

        Assert.True(ProjetoResponse.Count() > 0);
    }


    [Fact(DisplayName = "Remove um Projeto existente")]
    public async Task Delete()
    {
        var id = _fixture.Create<int>();

        _mockProjetoService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new ProjetoController(_mockLogger.Object, _mapper, _mockProjetoService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

}