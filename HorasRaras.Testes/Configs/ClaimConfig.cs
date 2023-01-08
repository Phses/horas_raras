using HorasRaras.Domain.Entities;
using System.Security.Claims;

namespace HorasRaras.Testes.Configs;

public static class ClaimConfig
{
    public static IEnumerable<Claim> Get(int id, string nome, string email, string perfil)
    {
        return new Claim[]
        {
            new Claim(ClaimTypes.SerialNumber, id.ToString()),
            new Claim(ClaimTypes.GivenName, nome),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, perfil)
        };
    }

    public static Claim[] Claims(this UsuarioEntity user)
    {
        return new Claim[]
        {
            new Claim(ClaimTypes.SerialNumber, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.Nome),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Perfil.Nome)
        };
    }
}
