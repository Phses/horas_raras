using AutoMapper;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces.Service;
using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace HorasRaras.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    [ProducesResponseType(typeof(InformacaoResponse), 400)]
    [ProducesResponseType(typeof(InformacaoResponse), 401)]
    [ProducesResponseType(typeof(InformacaoResponse), 403)]
    [ProducesResponseType(typeof(InformacaoResponse), 404)]
    [ProducesResponseType(typeof(InformacaoResponse), 500)]
    public class BaseController<TEntity, KRequest, YResponse> : ControllerBase where TEntity : BaseEntity
    {
        private readonly ILogger<BaseController<TEntity, KRequest, YResponse>> _logger;
        public readonly int? UserId;
        public readonly string UserPerfil;
        private readonly IMapper _mapper;
        private readonly IBaseService<TEntity> _service;

        public BaseController(ILogger<BaseController<TEntity, KRequest, YResponse>> logger,IMapper mapper, IBaseService<TEntity> service, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            UserId = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.SerialNumber).ToInt();
            UserPerfil = httpContextAccessor.HttpContext.GetClaim(ClaimTypes.Role);
            _mapper = mapper;
            _service = service;
        }

        /// <summary>
        /// Através dessa rota você será capaz de cadastrar um elemento.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso, e retorna uma lista de elementos</response>
        [HttpPost]
        [ProducesResponseType(201)]
        public virtual async Task<ActionResult> PostAsync([FromBody] KRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);

            await _service.AdicionarAsync(entity);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            return Created(nameof(PostAsync), new { id = entity.Id });
        }

        /// <summary>
        /// Através dessa rota você será capaz de alterar um elemento pelo o seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="204">Sucesso, e altera um elemento</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public virtual async Task<ActionResult> PutAsync([FromRoute] int id, [FromBody] KRequest request)
        {
            var entity = _mapper.Map<TEntity>(request);

            entity.Id = id;
            await _service.AlterarAsync(entity);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            return NoContent();
        }

        /// <summary>
        /// Através dessa rota você será capaz de deletar um elemento pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204">Sucesso, e deleta um elemento</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public virtual async Task<ActionResult> DeleteAsync([FromRoute] int id)
        {
            await _service.DeletarAsync(id);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");
            
            return NoContent();
        }

        /// <summary>
        /// Através dessa rota você será capaz de buscar uma lista de elementos
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e retorna uma lista de elementos</response>
        [HttpGet()]
        [ProducesResponseType(200)]
        public virtual async Task<ActionResult<List<YResponse>>> GetAsync()
        {
            var entities = await _service.ObterTodosAsync();
            var response = _mapper.Map<List<YResponse>>(entities);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");
            

            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de buscar um elemento pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e busca um elemento</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        public virtual async Task<ActionResult<YResponse>> GetByIdAsync([FromRoute] int id)
        {
            var entity = await _service.ObterPorIdAsync(id);
            var response = _mapper.Map<YResponse>(entity);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id =  {UserId} .");
            
            return Ok(response);
        }
    }
}
