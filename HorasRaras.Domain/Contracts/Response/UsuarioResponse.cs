using HorasRaras.Domain.Contracts.Response;

namespace HorasRaras.Domain.Contracts;

public class UsuarioResponse : BaseResponse
{
    public string Nome { get; set; }
    public string Email { get; set; }

    public int PerfilId { get; set; }
}
