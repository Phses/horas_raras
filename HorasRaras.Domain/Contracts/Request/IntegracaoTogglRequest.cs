using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorasRaras.Domain.Contracts.Request
{
    public class IntegracaoTogglRequest
    {
        public string workspaceId { get; set; }
        public string since { get; set; }
        public string until { get; set; }
        public string userAgent { get; set; }
        public string token { get; set; }
    }
}
