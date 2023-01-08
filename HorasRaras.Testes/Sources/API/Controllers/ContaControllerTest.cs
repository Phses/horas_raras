using AutoFixture;
using AutoMapper;
using HorasRaras.Api.Controllers;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Shared;
using HorasRaras.Testes.Configs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Controllers;

[Trait("Controller", "Controller de Contas")]
public class ContaControllerTest
{
    private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
    private readonly Mock<ILogger<ContaController>> _mockLogger;
    private readonly Mock<IUsuarioService> _mockUsuarioService;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly Fixture _fixture;
    private readonly IMapper _mapper;

    public ContaControllerTest()
    {
        _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
        _mockLogger = new Mock<ILogger<ContaController>>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockEmailService = new Mock<IEmailService>();
        _fixture = FixtureConfig.Get();
        _mapper = MapConfig.Get();
    }


    [Fact(DisplayName = "Cadastra um novo usuário na conta")]
    public async Task CadastrarUsuarioAsyncTest()
    {
        var request = _fixture.Create<UsuarioRequest>();

        _mockUsuarioService.Setup(mock => mock.AdicionarAsync(It.IsAny<UsuarioEntity>())).Returns(Task.CompletedTask);

        var controller = new ContaController(_mockLogger.Object, _mockUsuarioService.Object, _mockEmailService.Object, _mapper, _mockHttpContextAccessor.Object);
        var actionResult = await controller.CadastrarUsuarioAsync(request);
        
        var objectResult = Assert.IsType<CreatedResult>(actionResult);

        Assert.Equal(StatusCodes.Status201Created, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Confirma o email do usuário")]
    public async Task ConfirmarEmailTest()
    {
        var entity = _fixture.Create<UsuarioEntity>();

        _mockUsuarioService.Setup(mock => mock.ConfirmarEmailAsync(It.IsAny<Guid>())).Returns(Task.CompletedTask);

        var controller = new ContaController(_mockLogger.Object, _mockUsuarioService.Object, _mockEmailService.Object, _mapper, _mockHttpContextAccessor.Object);
        var actionResult = await controller.ConfirmarEmail(entity.HashEmailConfimacao);

        var objectResult = Assert.IsType<OkResult>(actionResult);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }
    

    // OBSERVAR
    [Fact(DisplayName = "Autentica o usuário")]
    public async Task AutenticarAsyncTest()
    {
        var request = _fixture.Create<AuthenticacaoRequest>();
        var response = _fixture.Create<string>();

        _mockUsuarioService.Setup(mock => mock.AutenticarAsync(It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(response);

        var controller = new ContaController(_mockLogger.Object, _mockUsuarioService.Object, _mockEmailService.Object, _mapper, _mockHttpContextAccessor.Object);
        var actionResult = await controller.AutheticarAsync(request);

        var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        
        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }


    [Fact(DisplayName = "Solução para quando o usuário esquece a senha")]
    public async Task EsqueciSenhaAsyncTest ()
    {
        var request = _fixture.Create<EmailRequest>();

        _mockUsuarioService.Setup(mock => mock.AterarHashSenhaAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        var controller = new ContaController(_mockLogger.Object, _mockUsuarioService.Object, _mockEmailService.Object, _mapper, _mockHttpContextAccessor.Object);
        var response = await controller.EsqueciSenhaAsync(request);
        
        var objectResult = Assert.IsType<OkResult>(response);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    }
    

    [Fact(DisplayName = "Cria uma nova senha para o usuário")]
    public async Task ResetSenhaTest()
    {
        var emailRequest = _fixture.Create<EmailRequest>();

        _mockUsuarioService.Setup(mock => mock.AlterarSenhaAsync(It.IsAny<string>(), It.IsAny<NovaSenhaRequest>())).Returns(Task.CompletedTask);

        var controller = new ContaController(_mockLogger.Object, _mockUsuarioService.Object, _mockEmailService.Object, _mapper, _mockHttpContextAccessor.Object);
        var response = await controller.EsqueciSenhaAsync(emailRequest);
        
        var objectResult = Assert.IsType<OkResult>(response);

        Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
    } 
}