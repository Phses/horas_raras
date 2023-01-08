using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HorasRaras.Api.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();

                var response = new InformacaoResponse
                {
                    Codigo = StatusException.FormatoIncorreto,
                    Mensagens = errors
                };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = 400
                };
            }

            OnResultExecuting(context);
            if (!context.Cancel)
            {
                OnResultExecuted(await next());
            }
        }
    }

}
