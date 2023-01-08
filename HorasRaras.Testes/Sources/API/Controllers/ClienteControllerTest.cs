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
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
// using Serilog;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Controllers;

[Trait("Controller", "Controller de Clientes")]
public class ClienteControllerTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<ClienteController>> _mockLogger;
    private readonly Mock<IClienteService> _mockClienteService;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public ClienteControllerTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockLogger = new Mock<ILogger<ClienteController>>();
        _mockClienteService = new Mock<IClienteService>();
        _fixture = FixtureConfig.Get();
        _mapper = MapConfig.Get();
    }


    [Fact(DisplayName = "Cadastra um novo Cliente")]
    public async Task Post()
    {
        var request = _fixture.Create<ClienteRequest>();

        _mockClienteService.Setup(mock => mock.AdicionarAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);
        
        var controller = new ClienteController(_mockLogger.Object, _mapper, _mockClienteService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PostAsync(request);
        
        var objectResult = Assert.IsType<CreatedResult>(response);

        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Atualiza um Cliente existente")]
    public async Task Put()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<ClienteRequest>();

        _mockClienteService.Setup(mock => mock.AlterarAsync(It.IsAny<ClienteEntity>())).Returns(Task.CompletedTask);

        var controller = new ClienteController(_mockLogger.Object, _mapper, _mockClienteService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PutAsync(id, request);
        
        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca um Cliente por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<ClienteEntity>();

        _mockClienteService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var controller = new ClienteController(_mockLogger.Object, _mapper, _mockClienteService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetByIdAsync(entity.Id);
        
        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var clienteResponse = Assert.IsType<ClienteResponse>(objectResult.Value);

        Assert.Equal(clienteResponse.Id, entity.Id);
    }


    [Fact(DisplayName = "Busca todos os Clientes")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<ClienteEntity>>();

        _mockClienteService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new ClienteController(_mockLogger.Object, _mapper, _mockClienteService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var clienteResponse = Assert.IsType<List<ClienteResponse>>(objectResult.Value);

        Assert.True(clienteResponse.Count() > 0);
    }


    [Fact(DisplayName = "Remove um Cliente existente")]
    public async Task Delete()
    {
        var id = _fixture.Create<int>();

        _mockClienteService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new ClienteController(_mockLogger.Object, _mapper, _mockClienteService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

}