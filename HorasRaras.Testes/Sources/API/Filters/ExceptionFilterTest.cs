using HorasRaras.Api.Filters;
using HorasRaras.Domain.Enums;
using HorasRaras.Domain.Exceptions;
using HorasRaras.Testes.Configs;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace HorasRaras.Testes.Sources.Api.Filters
{
    public class ExceptionFilterTest
    {
        private readonly ActionContext _actionContext;
        private readonly List<IFilterMetadata> _filterMetadata;

        public ExceptionFilterTest()
        {
            _actionContext = new ActionContext
            {
                ActionDescriptor = new ActionDescriptor(),
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData()
            };
            _filterMetadata = new List<IFilterMetadata>();
        }
        
        
        [Fact(DisplayName = "Aciona uma Informacao Exception de Acesso proibido")]
        public async void OnExceptionInformacaoExceptionAcessoProibido()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.AcessoProibido, "Acesso proibido")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona uma Informacao Exception de Nao Processado")]
        public async void OnExceptionInformacaoExceptionNaoProcessado()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.NaoProcessado, "Dado não processado")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona uma Informacao Exception de Formato Incorreto")]
        public async void OnExceptionInformacaoExceptionFormatoIncorreto()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.FormatoIncorreto, "Campo(s) com formato(s) incorreto(s)")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona uma Informacao Exception de Campo Obrigatorio")]
        public async void OnExceptionInformacaoExceptionCampoObrigatorio()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.Obrigatoriedade, "Campo(s) obrigatório(s) não informado(s)")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona uma Informacao Exception de Nao Encontrado")]
        public async void OnExceptionInformacaoExceptionNaoEncontrado()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.NaoEncontrado, "Nenhum dado encontrado.")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona status de algo inesperado")]
        public async void OnExceptionStatusExceptionErro()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.Erro, "Ocorreu algo inesperado")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona StatusException Nenhum")]
        public async void OnExceptionStatusExceptionNenhum()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.Nenhum, "Nenhum")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona acesso não autorizado")]
        public async void OnExceptionStatusExceptionNaoAutorizado()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new InformacaoException(StatusException.NaoAutorizado, "Acesso não autorizado")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Aciona uma Informacao Exception")]
        public async void OnExceptionIsException()
        {
            var exceptionContext = new ExceptionContext(_actionContext, _filterMetadata)
            {
                Exception = new Exception("Erro inesperado.")
            };

            var exceptionFilter = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exceptionFilter.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Informacao Exception Inner Exception")]
        public async Task OnExceptionFilterInformacaoExceptionInnerException()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new InformacaoException(StatusException.NaoEncontrado, "Nenhum dado encontrado", new Exception("Erro Inner Exception"))
            };
            
            var exception = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Exception Inner Exception")]
        public async Task OnExceptionFilterInnerException()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new Exception("Erro genérico", new Exception("Erro Inner Exception"))
            };
            
            var exception = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "InformacaoException null")]
        public async Task OnExceptionFilterInformacaoExceptionNull()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = new InformacaoException(StatusException.NaoEncontrado, new List<string>())
            };
            
            var exception = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }


        [Fact(DisplayName = "Exception Null")]
        public async Task OnExceptionFilterNull()
        {
            var actionContext = new ActionContext()
            {
                HttpContext = new DefaultHttpContext(),
                RouteData = new RouteData(),
                ActionDescriptor = new ActionDescriptor()
            };

            var exceptionContext = new ExceptionContext(actionContext, new List<IFilterMetadata>())
            {
                Exception = null
            };
           
            var exception = new ExceptionFilter();
            var result = await Record.ExceptionAsync(() => exception.OnExceptionAsync(exceptionContext));
            
            Assert.Null(result);
        }
    }
}
