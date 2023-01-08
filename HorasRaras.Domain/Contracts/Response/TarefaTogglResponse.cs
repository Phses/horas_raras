
namespace HorasRaras.Domain.Contracts.Response
{
    public class TarefaTogglResponse
    {
        public string Descricao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime? HoraFinal { get; set; }
    }
}
