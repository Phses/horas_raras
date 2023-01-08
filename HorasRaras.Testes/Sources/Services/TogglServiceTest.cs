// using AutoFixture;
// using AutoMapper;
// using HorasRaras.Service;
// using HorasRaras.Domain.Contracts.Request;
// using HorasRaras.Domain.Contracts.Response;
// using HorasRaras.Domain.Entities;
// using HorasRaras.Domain.Interfaces;
// using HorasRaras.Domain.Interfaces.Service;
// using HorasRaras.Testes.Configs;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Moq;
// using Xunit;
// using HorasRaras.Domain.Exceptions;
// using Microsoft.AspNetCore.WebUtilities;
// using Microsoft.Net.Http.Headers;
// using System.Text;
// using System.Net.Http.Headers;
// using System.Net.Http.Json;
// using Moq.Protected;
// using Bogus;

// namespace HorasRaras.Testes.Sources.Services;

// [Trait("Service", "Service de Toggl")]
// public class TogglServiceTest
// {
//     private readonly Mock<IHttpClientFactory> _mockHttpClientFactory;
//     private readonly Mock<ITarefaService> _mockTarefaService;
//     private readonly IHttpClientFactory _httpClientFactory;
//     private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
//     private readonly Fixture _fixture;
//     private readonly IMapper _mapper;
//     private readonly Faker _faker;
//     public TogglServiceTest()
//     {
//         _mockHttpClientFactory = new Mock<IHttpClientFactory>();
//         _mockTarefaService = new Mock<ITarefaService>();
//         _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
//         _fixture = FixtureConfig.Get();
//         _mapper = MapConfig.Get();
//         _faker = new Faker();
//     }


    


//     [Fact(DisplayName = "Integra tarefas com o Toggl")]
//     public async Task ObterTarefasApiTogglTest()
//     {
//         var entity = _fixture.Create<TarefaEntity>();
//         var request = _fixture.Create<IntegracaoTogglRequest>();

//         _mockTarefaService.Setup(mock => mock.AdicionarAsync(entity)).Returns(Task.CompletedTask);

//         var service = new TogglService(_mockHttpClientFactory.Object, _mapper, _mockTarefaService.Object);
//         var exception = await Record.ExceptionAsync(() => service.ObterTarefasApiToggl(request.workspaceId, 
//                                                                                             request.since, 
//                                                                                             request.until, 
//                                                                                             request.userAgent, 
//                                                                                             request.token));
        
//         Assert.Null(exception);
//     }


//     [Fact(DisplayName = "Integra tarefas com o Toggl")]
//     public async Task ObterTarefasApiTogglTestException()
//     {
//         var entity = _fixture.Create<TogglTaskEntity>();
//         var request = _fixture.Create<IntegracaoTogglRequest>();

        
//         var url = _faker.Internet.Url();
//         var param = new Dictionary<string, string>() { {"workspace_id", request.workspaceId}, { "since", request.since }, { "until", request.until }, { "user_agent", request.userAgent } };
//         var newUrl = new Uri(QueryHelpers.AddQueryString(url, param));
//         //Esconder a rota no appSettings posteriormente, ver se funfa primeiro
//         var httpRequestMessage = new HttpRequestMessage(
//         HttpMethod.Get, newUrl)
//         {
//             Headers =
//             {
//             { HeaderNames.Authorization,$"Basic {Convert.ToBase64String(Encoding.Default.GetBytes(request.token + ":api_token"))}" },
//         }
//         };
//         var httpClient = _httpClientFactory.CreateClient();
//         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
//         var httpResponseMessage = new HttpResponseMessage()
//         {
//             StatusCode = System.Net.HttpStatusCode.InternalServerError,
//             Content = JsonContent.Create("Erro ao fazer a consulta.")
//         };

//         _mockHttpMessageHandler.Protected()
//             .Setup<Task<HttpResponseMessage>>(
//                 "SendAsync",
//                 ItExpr.IsAny<HttpRequestMessage>(),
//                 ItExpr.IsAny<CancellationToken>())
//             .ReturnsAsync(httpResponseMessage);

//         var service = new TogglService(_mockHttpClientFactory.Object, _mapper, _mockTarefaService.Object);

//         await Assert.ThrowsAnyAsync<InformacaoException>(() => service.ObterTarefasApiToggl(request.workspaceId, 
//                                                                                             request.since, 
//                                                                                             request.until, 
//                                                                                             request.userAgent, 
//                                                                                             request.token));

//     }
// }


