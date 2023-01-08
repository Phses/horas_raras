using HorasRaras.Data.Context;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces;

namespace HorasRaras.Data.Repository;

public class UsuarioRepository : BaseRepository<UsuarioEntity>, IUsuarioRepository
{
    private readonly HorasRarasContext _context;

    public UsuarioRepository(HorasRarasContext context) : base(context)
    {
    }
}
