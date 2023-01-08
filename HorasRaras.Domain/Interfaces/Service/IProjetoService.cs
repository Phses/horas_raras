using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Contracts.Response;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces.Service;

namespace HorasRaras.Domain.Interfaces;

public interface IProjetoService : IBaseService<ProjetoEntity>
{
    Task CriarProjetoAsync(ProjetoEntity entity, List<string> usuariosEmail);
    Task VincularUsuarioProjeto(int usuarioId, int projetoId);
    Task<HorasTotaisResponse> ObterHorasTotaisAsync(int id);
}