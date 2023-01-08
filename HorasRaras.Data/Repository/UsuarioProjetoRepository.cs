using HorasRaras.Domain.Interfaces;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Context;

namespace HorasRaras.Data.Repository;

public class UsuarioProjetoRepository : BaseRepository<UsuarioProjetoEntity>, IUsuarioProjetoRepository
{
    public UsuarioProjetoRepository(HorasRarasContext context) : base(context)
    {
    }
}