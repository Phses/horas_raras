namespace HorasRaras.Domain.Contracts.Response
{
    public class ProjetoResponse : BaseResponse
    {
        public string Nome { get; set; }      
        public float CustoPorHora { get; set; }
        public DateTime DataFinal { get; set; }

        public int ClienteId { get; set; }

    }
}
