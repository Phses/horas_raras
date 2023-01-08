using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Context;

namespace HorasRaras.Data.Repository;

public class ClienteRepository : BaseRepository<ClienteEntity>, IClienteRepository
{
    public ClienteRepository(HorasRarasContext context) : base(context)
    {

    }

}