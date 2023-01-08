using AutoFixture;
using AutoMapper;
using HorasRaras.Api.Controllers;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Testes.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Controllers;

[Trait("Controller", "Controller de Toggl")]
public class TogglControllerTest
{
    private readonly Mock<ITogglService> _mockTogglService;
    private readonly Fixture _fixture;
    public TogglControllerTest()
    {
        _mockTogglService = new Mock<ITogglService>();
        _fixture = FixtureConfig.Get();
    }


    [Theory(DisplayName = "Integra tarefas com o Toggl")]
    [InlineData("Colaborador")]
    [InlineData("Administrador")]
    public async Task IntegrarTarefasTogglAsync(string perfilLogadoNome)
    {
        var request = _fixture.Create<IntegracaoTogglRequest>();

        _mockTogglService.Setup(mock => mock.ObterTarefasApiToggl(request.workspaceId, 
                                                                        request.since, 
                                                                        request.until, 
                                                                        request.userAgent, 
                                                                        request.token))
                                                                        .Returns(Task.CompletedTask);

        var controller = new TogglController(_mockTogglService.Object);
        var response = await controller.IntegrarTarefasTogglAsync(request);
        var objectResult = Assert.IsType<OkResult>(response);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    // [Theory(DisplayName = "Integra tarefas com o Toggl")]
    // [InlineData("Colaborador")]
    // [InlineData("Administrador")]
    // public async Task IntegrarTarefasTogglAsyncBadRequest(string perfilLogadoNome)
    // {
    //     var request = new IntegracaoTogglRequest();

    //     _mockTogglService.Setup(mock => mock.ObterTarefasApiToggl(request.workspaceId, 
    //                                                                     request.since, 
    //                                                                     request.until, 
    //                                                                     request.userAgent, 
    //                                                                     request.token))
    //                                                                     .Returns((IntegracaoTogglRequest)null);

    //     var controller = new TogglController(_mockTogglService.Object);
    //     var response = await controller.IntegrarTarefasTogglAsync(request);
    //     var objectResult = Assert.IsType<BadRequestResult>(response);

    //     Assert.Equal(StatusCodes.Status400BadRequest, objectResult.StatusCode);
    // }
}