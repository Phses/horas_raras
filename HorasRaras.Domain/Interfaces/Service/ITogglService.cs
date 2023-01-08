using HorasRaras.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorasRaras.Domain.Interfaces.Service
{
    public interface ITogglService
    {
        Task ObterTarefasApiToggl(string workspaceId, string since, string until, string userAgent, string token);
    }
}
