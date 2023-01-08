using System.ComponentModel.DataAnnotations;

namespace HorasRaras.Domain.Contracts.Request
{
    public class TarefaRequest
    {
        [Required(ErrorMessage = "O campo 'Id' é obrigatorio")]
        public int ProjetoId { get; set; }

        [Required(ErrorMessage = "O campo 'Id' é obrigatorio")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo 'Descrição' é obrigatorio")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo 'HoraInicio' é obrigatorio")]
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFinal { get; set; }
    }
}
