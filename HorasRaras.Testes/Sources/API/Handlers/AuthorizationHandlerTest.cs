// using HorasRaras.Api.Filters;
// using HorasRaras.Domain.Enums;
// using HorasRaras.Domain.Exceptions;
// using HorasRaras.Testes.Configs;
// using AutoFixture;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Abstractions;
// using Microsoft.AspNetCore.Mvc.Filters;
// using Microsoft.AspNetCore.Routing;
// using Xunit;
// using Microsoft.AspNetCore.Authorization;
// using Moq;
// using Microsoft.AspNetCore.Authorization.Policy;
// using HorasRaras.Domain.Contracts.Response;
// using System.Security.Claims;
// using System.Reflection.Metadata;
// using Bogus;
// using HorasRaras.Api.Handlers;

// namespace HorasRaras.Testes.Sources.Api.Handlers
// {
//     public class AuthorizationHandlerTest
//     {
//         private readonly Mock<IAuthorizationMiddlewareResultHandler> _handler;
//         private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
//         private readonly Fixture _fixture;
//         private readonly Faker _faker;
//         private readonly ActionContext _actionContext;
//         private readonly InformacaoResponse _informacaoResponse;
//         private readonly List<IFilterMetadata> _filterMetadata;

//         public AuthorizationHandlerTest()
//         {
//             _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
//             _fixture = FixtureConfig.Get();
//             _actionContext = new ActionContext
//             {
//                 ActionDescriptor = new ActionDescriptor(),
//                 HttpContext = new DefaultHttpContext(),
//                 RouteData = new RouteData()
//             };
//             _filterMetadata = new List<IFilterMetadata>();
//             _handler = new Mock<IAuthorizationMiddlewareResultHandler>();
//             _informacaoResponse = new InformacaoResponse();
//             _faker = new Faker();
//         }


//         [Fact(DisplayName = "Aciona Succeeded")]
//         public async Task HandleAsyncSucceeded(string perfil)
//         {
            
//             // RequestDelegate requestDelegate,
//             // HttpContext httpContext,
//             // AuthorizationPolicy authorizationPolicy,
//             // PolicyAuthorizationResult policyAuthorizationResult)
            
//             // await _handler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);


//             var claims = ClaimConfig.Get(_faker.UniqueIndex, _faker.Person.FullName, _faker.Person.Email, perfil);
//             _mockHttpContextAccessor.Setup(mock => mock.HttpContext.User.Claims).Returns(claims);


//             var authorization = new AuthorizationHandlerContext()
//             {
                
//             };

//             var authorizationHandler = new AuthorizationHandler(_handler);

//             var result = await authorizationHandler.HandleAsync();
//             Assert.Null(result);
//         }
        
        
//         [Fact(DisplayName = "Aciona Forbidden")]
//         [ProducesResponseType(403)] // ?
//         public async Task HandleAsyncForbidden()
//         {
//             throw new NotImplementedException();
//         }
        
        
//         [Fact(DisplayName = "Aciona Denied")]
//         [ProducesResponseType(401)] // ?
//         public async Task HandleAsyncDenied()
//         {
//             throw new NotImplementedException();
//         }




//     }
// }


// //         public async Task HandleAsync(
// //             RequestDelegate requestDelegate,
// //             HttpContext httpContext,
// //             AuthorizationPolicy authorizationPolicy,
// //             PolicyAuthorizationResult policyAuthorizationResult)
// //         {
// //             var informacaoResponse = new InformacaoResponse();

// //             if (!policyAuthorizationResult.Succeeded)
// //             {
// //                 if (policyAuthorizationResult.Forbidden)
// //                 {
// //                     httpContext.Response.StatusCode = 403;
// //                     informacaoResponse = new InformacaoResponse
// //                     {
// //                         Codigo = StatusException.AcessoProibido,
// //                         Mensagens = new List<string> { "Acesso não permitido" }
// //                     };
// //                 }
// //                 else
// //                 {
// //                     httpContext.Response.StatusCode = 401;
// //                     informacaoResponse = new InformacaoResponse
// //                     {
// //                         Codigo = StatusException.NaoAutorizado,
// //                         Mensagens = new List<string> { "Acesso negado" }
// //                     };
// //                 }

// //                 await httpContext.Response.WriteAsJsonAsync(informacaoResponse);
// //             }
// //             else
// //                 await _handler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
// //         }
// //     }
