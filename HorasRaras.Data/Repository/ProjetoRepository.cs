using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Context;

namespace HorasRaras.Data.Repository;

public class ProjetoRepository : BaseRepository<ProjetoEntity>, IProjetoRepository
{
    public ProjetoRepository(HorasRarasContext context) : base(context)
    {

    }

}