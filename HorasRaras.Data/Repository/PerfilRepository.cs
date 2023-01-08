using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Context;

namespace HorasRaras.Data.Repository;

public class PerfilRepository : BaseRepository<PerfilEntity>, IPerfilRepository
{
    public PerfilRepository(HorasRarasContext context) : base(context)
    {

    }

}