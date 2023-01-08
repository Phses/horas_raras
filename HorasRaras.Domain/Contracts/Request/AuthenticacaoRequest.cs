using System.ComponentModel.DataAnnotations;

namespace HorasRaras.Domain.Contracts.Request
{
    public class AuthenticacaoRequest
    {
        // ErroMessage tá informando "Email inválido"
        [Required(ErrorMessage = "O campo 'Senha' é obrigatorio")]
        
        public string Senha { get; set; }

        [Required(ErrorMessage = "O campo 'Email' é obrigatorio")]
        
        public string Email { get; set; }

    }
}
