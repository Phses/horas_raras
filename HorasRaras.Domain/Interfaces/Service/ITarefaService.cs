using HorasRaras.Domain.Contracts;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces.Service;

namespace HorasRaras.Domain.Interfaces;

public interface ITarefaService : IBaseService<TarefaEntity>
{
    Task AtualizarHoraFinalAsync(int id, DateTime horaFinal);
    Task AdicionarTarefaTogglAsync(TarefaEntity entity, string projetoNome);

}