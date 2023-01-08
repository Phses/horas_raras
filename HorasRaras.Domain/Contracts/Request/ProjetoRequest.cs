using System.ComponentModel.DataAnnotations;

namespace HorasRaras.Domain.Contracts.Request
{
    public class ProjetoRequest
    {
        
        [Required(ErrorMessage = "O campo 'Nome' é obrigatorio")]
        public string Nome { get; set; }

        public float? CustoPorHora { get; set; }
        public DateTime? DataFinal { get; set; }

        [Required(ErrorMessage = "O campo 'Id' é obrigatorio")]
        public int ClienteId { get; set; }
        public List<string> UsuariosEmail { get; set; }
    }
}
