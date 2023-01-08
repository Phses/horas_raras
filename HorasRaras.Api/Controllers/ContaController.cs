using AutoMapper;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorasRaras.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class ContaController : ControllerBase
    {
        private readonly ILogger<ContaController> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public ContaController(ILogger<ContaController> logger, IUsuarioService usuarioService, IEmailService emailService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _usuarioService = usuarioService;
            _emailService = emailService;
            _mapper = mapper;
        }

        /// <summary>
        /// Através dessa rota você será capaz de cadastrar um usuário
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="201">Sucesso, e cadastra um usuário</response>
        [HttpPost("cadastro")]
        [ProducesResponseType(201)]
        public async Task<ActionResult> CadastrarUsuarioAsync([FromBody] UsuarioRequest request)
        {
           
            var entity = _mapper.Map<UsuarioEntity>(request);
            
            await _usuarioService.CriarUsuarioAsync(entity);

            _logger.LogInformation($"Usuário criado.");


            return Created(nameof(CadastrarUsuarioAsync), new { id = entity.Id });
        }

        /// <summary>
        /// Através dessa rota você poderá confirmar seu cadastro através de um codigo recebido via email.
        /// </summary>
        /// <param name="hashEmailConfirmacao"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e cadastro confirmado</response>
        [HttpPatch("email")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ConfirmarEmail([FromQuery] Guid hashEmailConfirmacao)
        {
            await _usuarioService.ConfirmarEmailAsync(hashEmailConfirmacao);
            return Ok();
            //Configurar respostas
            try
            {
                await _usuarioService.ConfirmarEmailAsync(hashEmailConfirmacao);

                _logger.LogInformation($"Email Confirmado .");


                return Ok("Email cadastrado");
            }catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Através dessa rota você será capaz de gerar um Token pra autenticação de usuario.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e Login autenticado</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<string>> AutheticarAsync([FromBody] AuthenticacaoRequest request)
        {
            var response = await _usuarioService.AutenticarAsync(request.Email, request.Senha);

            _logger.LogInformation($"Login autenticado .");


            return Ok(response);
        }

        /// <summary>
        /// Através dessa rota você será capaz de alterar sua senha com um hash recebido via email
        /// </summary>
        /// <param name="emailRequest"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e hash recebido</response>
        [HttpPut("hash-senha")]
        [ProducesResponseType(200)]
        public async Task<ActionResult> EsqueciSenhaAsync ([FromBody] EmailRequest emailRequest)
        {
            await _usuarioService.AterarHashSenhaAsync(emailRequest.Email);

            _logger.LogInformation($"hash recebido.");

            return Ok();
        }

        /// <summary>
        /// Através dessa rota você será capaz de inserir o hash recebido por email, para alterar senha
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="novaSenha"></param>
        /// <returns></returns>
        /// <response code="200">Sucesso, e senha alterada</response>
        [HttpPut("senha/{hash}")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ResetSenha([FromRoute] string hash, [FromBody] NovaSenhaRequest novaSenha)
        {
            try
            {
                await _usuarioService.AlterarSenhaAsync(hash, novaSenha);

                _logger.LogInformation($"Senha Alterada.");


                return Ok();
            }catch
            {
               return BadRequest();
            }

        }
    }
}
