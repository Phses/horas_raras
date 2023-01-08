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

[Trait("Controller", "Controller de Perfis")]
public class PerfilControllerTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<PerfilController>> _mockLogger;
    private readonly Mock<IPerfilService> _mockPerfilService;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public PerfilControllerTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockLogger = new Mock<ILogger<PerfilController>>();
        _mockPerfilService = new Mock<IPerfilService>();
        _fixture = FixtureConfig.Get();
        _mapper = MapConfig.Get();
    }


    [Fact(DisplayName = "Cadastra um novo Perfil")]
    public async Task Post()
    {
        var request = _fixture.Create<PerfilRequest>();

        _mockPerfilService.Setup(mock => mock.AdicionarAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var controller = new PerfilController(_mockLogger.Object, _mapper, _mockPerfilService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PostAsync(request);
        var objectResult = Assert.IsType<CreatedResult>(response);

        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Atualiza um Perfil existente")]
    public async Task Put()
    {
        var id = _fixture.Create<int>();
        var request = _fixture.Create<PerfilRequest>();

        _mockPerfilService.Setup(mock => mock.AlterarAsync(It.IsAny<PerfilEntity>())).Returns(Task.CompletedTask);

        var controller = new PerfilController(_mockLogger.Object, _mapper, _mockPerfilService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.PutAsync(id, request);
        
        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Busca um Perfil por Id")]
    public async Task GetById()
    {
        var entity = _fixture.Create<PerfilEntity>();

        _mockPerfilService.Setup(mock => mock.ObterPorIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

        var controller = new PerfilController(_mockLogger.Object, _mapper, _mockPerfilService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetByIdAsync(entity.Id);
        
        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var PerfilResponse = Assert.IsType<PerfilResponse>(objectResult.Value);

        Assert.Equal(PerfilResponse.Id, entity.Id);
    }


    [Fact(DisplayName = "Busca todos os Perfis")]
    public async Task Get()
    {
        var entities = _fixture.Create<List<PerfilEntity>>();

        _mockPerfilService.Setup(mock => mock.ObterTodosAsync()).ReturnsAsync(entities);

        var controller = new PerfilController(_mockLogger.Object, _mapper, _mockPerfilService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.GetAsync();

        var objectResult = Assert.IsType<OkObjectResult>(response.Result);
        var PerfilResponse = Assert.IsType<List<PerfilResponse>>(objectResult.Value);

        Assert.True(PerfilResponse.Count() > 0);
    }


    [Fact(DisplayName = "Remove um Perfil existente")]
    public async Task Delete()
    {
        var id = _fixture.Create<int>();

        _mockPerfilService.Setup(mock => mock.DeletarAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        var controller = new PerfilController(_mockLogger.Object, _mapper, _mockPerfilService.Object, _mockHttpContextAccessor.Object);
        var response = await controller.DeleteAsync(id);

        var objectResult = Assert.IsType<NoContentResult>(response);

        Assert.Equal(StatusCodes.Status204NoContent, objectResult.StatusCode);
    }

}