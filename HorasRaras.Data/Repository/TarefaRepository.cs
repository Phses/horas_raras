using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Context;

namespace HorasRaras.Data.Repository;

public class TarefaRepository : BaseRepository<TarefaEntity>, ITarefaRepository
{
    public TarefaRepository(HorasRarasContext context) : base(context)
    {

    }

}