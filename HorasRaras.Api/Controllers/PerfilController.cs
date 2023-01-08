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
    public class PerfilController : BaseController<PerfilEntity, PerfilRequest, PerfilResponse>
    {
        private readonly ILogger<PerfilController> _logger;
        private readonly IMapper _mapper;
        private readonly IPerfilService _perfilService;

        public PerfilController(ILogger<PerfilController> logger, IMapper mapper, IPerfilService perfilService, IHttpContextAccessor httpContextAccessor) : base(logger, mapper, perfilService, httpContextAccessor)
        {
            _logger = logger;
        }
    }
}
