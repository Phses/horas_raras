using AutoMapper;
using Azure.Core;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HorasRaras.Api.Controllers
{
    [Authorize(Roles = ConstanteUtil.PerfilLogadoNome)]
    public class UsuarioController : BaseController<UsuarioEntity, UsuarioRequest, UsuarioResponse>
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(ILogger<UsuarioController> logger, IMapper mapper, IUsuarioService usuarioService, IHttpContextAccessor httpContextAccessor) : base(logger, mapper, usuarioService, httpContextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }
    }
}
