using AutoMapper;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HorasRaras.Api.Controllers
{
    [Authorize(Roles = ConstanteUtil.PerfilAdministradorNome)]
    public class ProjetoController : BaseController<ProjetoEntity, ProjetoRequest, ProjetoResponse>
    {
        private readonly ILogger<ProjetoController> _logger;
        private readonly IMapper _mapper;
        private readonly IProjetoService _projetoService;

        public ProjetoController(ILogger<ProjetoController> logger, IMapper mapper, IProjetoService projetoService, IHttpContextAccessor httpContextAccessor) : base(logger, mapper, projetoService, httpContextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _projetoService = projetoService;
        }

        /// <summary>
        /// Através dessa rota você será capaz de criar um projeto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso, e projeto criado</response>
        [HttpPost]
        [ProducesResponseType(201)]
        public override async Task<ActionResult> PostAsync([FromBody] ProjetoRequest request)
        {
            var entity = _mapper.Map<ProjetoEntity>(request);
            
            await _projetoService.CriarProjetoAsync(entity, request.UsuariosEmail);

            _logger.LogInformation($"API chamada por {UserPerfil}, {UserId}.");
            Log.ForContext("Action", $"Logs.CriarProjeto");

            return Created(nameof(PostAsync), new {id = entity.Id});           
            
        }

        /// <summary>
        /// Através dessa rota você será capaz de incluir um usuário a um projeto
        /// </summary>
        /// <param name="usuarioProjeto"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e usuário vinculado a projeto</response>
        [HttpPatch("usuario")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> PatchIncluirUsuarioAsync([FromBody] UsuarioProjetoRequest usuarioProjeto)
        {
            
             await _projetoService.VincularUsuarioProjeto(usuarioProjeto.UsuarioId, usuarioProjeto.ProjetoId);
            
            _logger.LogInformation($"API chamada por {UserPerfil}, {UserId}.");
            Log.ForContext("Action", $"Logs.VincularUsuarioProjeto");

            return Ok();
            
        }

        /// <summary>
        /// Através dessa rota você será capaz de ver o total de horas trabalhadas em um projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        [HttpGet("hora/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<HorasTotaisResponse>> GetHorasTotaisAsync([FromRoute] int id)
        {

            var response = await _projetoService.ObterHorasTotaisAsync(id);

            _logger.LogInformation($"API chamada por {UserPerfil}, {UserId}.");
            Log.ForContext("Action", $"Logs.VincularUsuarioProjeto");

            return Ok(response);

        }
    }
}
