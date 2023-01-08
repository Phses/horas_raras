using AutoMapper;
using Azure;
using Azure.Core;
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
    [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
    public class TarefaController : BaseController<TarefaEntity, TarefaRequest, TarefaResponse>
    {
        private readonly ILogger<TarefaController> _logger;
        private readonly IMapper _mapper;
        private readonly ITarefaService _tarefaService;

        public TarefaController(ILogger<TarefaController> logger, IMapper mapper, ITarefaService tarefaService, IHttpContextAccessor httpContextAccessor) : base(logger, mapper, tarefaService, httpContextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _tarefaService = tarefaService;
        }

        /// <summary>
        /// Através dessa rota você será capaz de pelo Id de sua tarefa atualizar sua hora final
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e hora final atualizada</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> PatchAsync([FromRoute] int id, [FromBody] TarefaHoraRequest request)
        {
            await _tarefaService.AtualizarHoraFinalAsync(id, request.HoraFinal);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            
            return Ok();
        }

        /// <summary>
        /// Através dessa rota você será capaz de buscar uma lista de tarefas finalizadas
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e lista tarefas finalizadas</response>
        [HttpGet()]
        [ProducesResponseType(200)]
        public override async Task<ActionResult<List<TarefaResponse>>> GetAsync()
        {
            var entities = await _tarefaService.ObterTodosAsync();
            var entitiesResponse = _mapper.Map<List<TarefaResponse>>(entities);
            foreach (var tarefa in entitiesResponse)
            {
                if (!tarefa.HoraFinal.HasValue)
                {
                    tarefa.AvisoHoraFinal = "A tarefa não foi finalizada";
                }
            }
            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");


            return Ok(entitiesResponse);
        }

        /// <summary>
        /// Através dessa rota você será capaz de listar as tarefas dos últimos 7 dias
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e Lista os últimos 7 dias de tarefas </response>
        [HttpGet("semana")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<TarefaResponse>>> ObterTarefasSemana()
        {
            var entities = await _tarefaService.ObterTodosAsync(entity => entity.DataInclusao >= DateTime.Now.Date.AddDays(-7));
            var entitiesResponse = _mapper.Map<List<TarefaResponse>>(entities);
            foreach(var tarefa in entitiesResponse)
            {
                if (!tarefa.HoraFinal.HasValue)
                {
                    tarefa.AvisoHoraFinal = "A tarefa não foi finalizada";
                }
            }
            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            return Ok(entitiesResponse);
        }

        /// <summary>
        /// Através dessa rota você será capaz de listar as tarefas finalizadas do mês corrente
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e Lista as tarefas finalizadas do mês corrente</response>
        [HttpGet("mes")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<TarefaResponse>>> ObterTarefasEsteMes()
        {
            var entities = await _tarefaService.ObterTodosAsync(entity => entity.DataInclusao.Month == DateTime.Now.Month);
            var entitiesResponse = _mapper.Map<List<TarefaResponse>>(entities);
            foreach (var tarefa in entitiesResponse)
            {
                if (!tarefa.HoraFinal.HasValue)
                {
                    tarefa.AvisoHoraFinal = "A tarefa não foi finalizada";
                }
            }
            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            return Ok(entitiesResponse);
        }

        /// <summary>
        /// Através dessa rota você será capaz de listar as tarefas finalizadas do dia atual
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Sucesso, e Lista as tarefas finalizadas do dia atual</response>
        [HttpGet("dia")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<TarefaResponse>>> ObterTarefasDia()
        {
            var entities = await _tarefaService.ObterTodosAsync(entity => entity.DataInclusao.Date == DateTime.Now.Date);
            var entitiesResponse = _mapper.Map<List<TarefaResponse>>(entities);
            foreach (var tarefa in entitiesResponse)
            {
                if (!tarefa.HoraFinal.HasValue)
                {
                    tarefa.AvisoHoraFinal = "A tarefa não foi finalizada";
                }
            }
            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            return Ok(entitiesResponse);
        }

        /// <summary>
        /// Através dessa rota você será capaz de buscar pelo Id de um projeto todas as suas tarefas finalizadas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e Lista as tarefas finalizadas por projeto</response>
        [HttpGet("projeto/{id}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<TarefaResponse>>> ObterTarefasPorProjeto([FromRoute] int id)
        {
            var entities = await _tarefaService.ObterTodosAsync(entity => entity.ProjetoId == id);
            var entitiesResponse = _mapper.Map<List<TarefaResponse>>(entities);

            _logger.LogInformation($"API chamada por {UserPerfil}, Id = {UserId} .");

            return Ok(entitiesResponse);       
        }
    }
}
