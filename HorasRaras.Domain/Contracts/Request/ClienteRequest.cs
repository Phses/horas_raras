using System.ComponentModel.DataAnnotations;

namespace HorasRaras.Domain.Contracts.Request
{
    public class ClienteRequest
    {
        [Required(ErrorMessage = "O campo 'Nome' é obrigatorio")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]*$", ErrorMessage = "Use apenas letras no campo 'Nome'")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatorio")]
        [RegularExpression(@"[^@ \t\r\n]+@[^@ \t\r\n]+\.[^@ \t\r\n]+", ErrorMessage = "Email inválido")]
        public string Email { get; set; }
    }
}
