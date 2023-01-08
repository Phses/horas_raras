using HorasRaras.Domain.Enums;
using HorasRaras.Domain.Utils;

namespace HorasRaras.Domain.Contracts.Response
{
    public class InformacaoResponse
    {
        public StatusException Codigo { get; set; }
        public string Descricao { get { return Codigo.Description(); } }
        public List<string> Mensagens { get; set; }
        public string Detalhe { get; set; }
    }

}
