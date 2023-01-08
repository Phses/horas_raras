using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace HorasRaras.Service;

public class ClienteService : BaseService<ClienteEntity>, IClienteService
{
    public ClienteService(IClienteRepository clienteRepository, IHttpContextAccessor httpContextAccessor) : base(clienteRepository, httpContextAccessor)
    {
    }

}