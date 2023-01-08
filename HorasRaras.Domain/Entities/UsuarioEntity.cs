
namespace HorasRaras.Domain.Entities;

public class UsuarioEntity : BaseEntity
{
    public UsuarioEntity()
    {
        HashEmailConfimacao = Guid.NewGuid();
    }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string HashSenha { get; set; }
    public DateTime? HashSenhaExpiracao { get; set; }
    public Guid HashEmailConfimacao { get; set; }
    public bool EmailConfirmado { get; set; }

    public int PerfilId { get; set; }
    public virtual PerfilEntity Perfil { get; set; }

    public virtual ICollection<TarefaEntity> Tarefas { get; set; }
    public virtual ICollection<UsuarioProjetoEntity> UsuariosProjeto { get; set; }


}