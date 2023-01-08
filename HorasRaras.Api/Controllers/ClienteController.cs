using AutoMapper;
using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Authorization;

namespace HorasRaras.Api.Controllers
{
    [Authorize(Roles = ConstanteUtil.PerfilAdministradorNome)]
    public class ClienteController : BaseController<ClienteEntity, ClienteRequest, ClienteResponse>
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly IMapper _mapper;
        private readonly IClienteService _clienteService;

        public ClienteController(ILogger<ClienteController> logger, IMapper mapper, IClienteService clienteService, IHttpContextAccessor httpContextAccessor) : base(logger, mapper, clienteService, httpContextAccessor)
        {
            _logger = logger;
        }
    }
}
