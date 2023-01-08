using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System.Security.Claims;

namespace HorasRaras.Api.Middlewares
{
    public class RequestSerilogMiddleware
    {
        private readonly RequestDelegate _next;


        public RequestSerilogMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;


        }

        public Task Invoke(HttpContext context)
        {
          
                     return _next.Invoke(context);
            
        }

        private string GetCorrelationId(HttpContext httpContext)
        {
            httpContext.Request.Headers.TryGetValue("Cko-Correlation-Id", out StringValues correlationId);
            return correlationId.FirstOrDefault() ?? httpContext.TraceIdentifier;
        }
    }
}
