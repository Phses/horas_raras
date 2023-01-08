
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace HorasRaras.Domain.Contracts.Request
{
    public class NovaSenhaRequest
    {
        public string NovaSenha { get; set; }
        public string NovaSenhaConfirmacao { get; set; }
    }
}
