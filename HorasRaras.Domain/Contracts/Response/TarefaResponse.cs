namespace HorasRaras.Domain.Contracts.Response
{
    public class TarefaResponse : BaseResponse
    {
        public string Descricao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime? HoraFinal { get; set; }
        public double HorasTotal { get; set; }
        public string AvisoHoraFinal { get; set; }

        public int ProjetoId { get; set; }
        public int UsuarioId { get; set; }

    }
}
