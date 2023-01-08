using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorasRaras.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
    public class TogglController : ControllerBase
    {
        private readonly ITogglService _togglService;
        //private readonly ILogger<TogglController> _logger;

        public TogglController(ITogglService togglService)
        {
            //_logger = logger;
            _togglService = togglService;
        }

        /// <summary>
        /// Atravs dessa rota acontece a integrao com a ferramenta Toggl
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, Integrao realizada</response>
        [HttpPost("integracao")]
        [ProducesResponseType(200)]
        [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
        public async Task<ActionResult> IntegrarTarefasTogglAsync([FromBody] IntegracaoTogglRequest request)
        {
            
            try
            {
                await _togglService.ObterTarefasApiToggl(request.workspaceId, 
                                                        request.since, 
                                                        request.until, 
                                                        request.userAgent, 
                                                        request.token);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
