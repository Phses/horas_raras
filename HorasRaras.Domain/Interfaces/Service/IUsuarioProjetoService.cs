using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces.Service;

namespace HorasRaras.Domain.Interfaces;

public interface IUsuarioProjetoService : IBaseService<UsuarioProjetoEntity>
{
    Task VincularUsuarioProjeto(int usuarioId, int projetoId);
}