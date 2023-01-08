using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorasRaras.Domain.Contracts.Request
{
    public class UsuarioProjetoRequest
    {
        public int UsuarioId { get; set; }
        public int ProjetoId { get; set; }
    }
}
