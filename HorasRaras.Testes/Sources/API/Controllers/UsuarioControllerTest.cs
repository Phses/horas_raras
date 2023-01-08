using AutoFixture;
using AutoMapper;
using HorasRaras.Api.Controllers;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Testes.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
// using Serilog;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Controllers;

[Trait("Controller", "Controller de Usuarios")]
public class UsuarioControllerTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<IUsuarioService> _mockUsuarioService;
    private readonly Mock<ILogger<UsuarioController>> _mockLogger;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public UsuarioControllerTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockLogger = new Mock<ILogger<UsuarioController>>();
        _fixture = FixtureConfig.Get();
        _mapper = MapConfig.Get();
    }


    [Fact(DisplayName = "Cadastra um novo Usuario")]
    public async Task Post()
    {
        var request = _fixture.Create<UsuarioRequest>();

        _mockUsuarioService.Setup(mock => mock.AdicionarAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);
                
        var controller = new UsuarioController(_mockLogger.Object, _mapper, _mockUsuarioService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PostAsync(request);

        var objectResult = Assert.IsType<CreatedResult>(response);

        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Atualiza um Usuario existente")]
    public async Task Put()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<UsuarioRequest>();

        _mockUsuarioService.Setup(mock => mock.AlterarAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var controller = new UsuarioController(_mockLogger.Object, _mapper, _mockUsuarioService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PutAsync(id, request);
        
        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca um Usuario por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockUsuarioService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var controller = new UsuarioController(_mockLogger.Object, _mapper, _mockUsuarioService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetByIdAsync(entity.Id);
        
        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var UsuarioResponse = Assert.IsType<UsuarioResponse>(objectResult.Value);

        Assert.Equal(UsuarioResponse.Id, entity.Id);
    }


    [Fact(DisplayName = "Busca todos os Usuarios")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<UsuarioEntity>>();

        _mockUsuarioService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new UsuarioController(_mockLogger.Object, _mapper, _mockUsuarioService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var UsuarioResponse = Assert.IsType<List<UsuarioResponse>>(objectResult.Value);

        Assert.True(UsuarioResponse.Count() > 0);
    }


    [Fact(DisplayName = "Remove um Usuario existente")]
    public async Task Delete()
    {
        var id = _fixture.Create<int>();

        _mockUsuarioService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new UsuarioController(_mockLogger.Object, _mapper, _mockUsuarioService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

}