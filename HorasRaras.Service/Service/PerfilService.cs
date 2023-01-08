using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HorasRaras.Service;

public class PerfilService : BaseService<PerfilEntity>, IPerfilService
{
    public PerfilService(IPerfilRepository perfilRepository, IHttpContextAccessor httpContextAccessor) : base(perfilRepository, httpContextAccessor)
    {
    }
}