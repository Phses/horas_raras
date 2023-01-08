using AutoFixture;
using HorasRaras.Api.Controllers;
using HorasRaras.Api.Filters;
using HorasRaras.Domain.Enums;
using HorasRaras.Testes.Configs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Filters;

[Trait("Validation", "Validation Filter")]
public class ValidationFilterTest
{
    private readonly Fixture _fixture;
    private readonly ActionContext _actionContext;
    private readonly List<IFilterMetadata> _filterMetadata;
    private readonly Mock<UsuarioController> _mockUsuarioController = new Mock<UsuarioController>();

    public ValidationFilterTest()
    {
        _fixture = FixtureConfig.Get();

        var modelState = new ModelStateDictionary();

        modelState.AddModelError("", "error");

        var httpContext = new DefaultHttpContext();
        
        _actionContext = new ActionContext(httpContext: httpContext, routeData: new Microsoft.AspNetCore.Routing.RouteData(), 
                                            actionDescriptor: new ActionDescriptor(), modelState: modelState);

        _filterMetadata = new List<IFilterMetadata>();
    }



    [Fact(DisplayName = "Acionar uma validacao Exception")]
    public async Task OnResultExecutionAsyncTest()
    {
        
        var executingContext = new ResultExecutingContext(_actionContext, _filterMetadata, 
                                new ObjectResult(StatusException.FormatoIncorreto), 
                                Mock.Of<Controller>()) {};

        var executionDelegate = new Mock<ResultExecutionDelegate>();

        var validationFilter = new ValidationFilter();

        var result = await Record.ExceptionAsync(() => validationFilter
                                .OnResultExecutionAsync(executingContext, executionDelegate.Object));
        
        Assert.Null(result);
    }

    [Fact(DisplayName = "Teste ValidationFilter")]
    public async void OnValidation()
    {
        var validationContext = new ResultExecutingContext(_actionContext, _filterMetadata, 
                                                            new ObjectResult(StatusException.FormatoIncorreto), _fixture)
        {
            Cancel = true
        };        
        
        
        var executionDelegate = new Mock<ResultExecutionDelegate>();
        var validationFilter = new ValidationFilter();
        var result = await Record.ExceptionAsync(() => validationFilter
        .OnResultExecutionAsync(validationContext, executionDelegate.Object));
        
        Assert.Null(result);
    }
}