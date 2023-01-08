using HorasRaras.Domain.Contracts.Request;
using HorasRaras.Domain.Entities;
using HorasRaras.Domain.Interfaces.Service;
using System.Threading.Tasks;

namespace HorasRaras.Domain.Interfaces;

public interface IUsuarioService : IBaseService<UsuarioEntity>
{
    Task CriarUsuarioAsync(UsuarioEntity usuario);
    Task<string> AutenticarAsync(string email, string senha);
    Task ConfirmarEmailAsync(Guid hashConfirmacao);
    Task AlterarSenhaAsync(string hashSenha, NovaSenhaRequest novaSenha);
    Task AterarHashSenhaAsync(string email);
}