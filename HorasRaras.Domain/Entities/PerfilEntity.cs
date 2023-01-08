namespace HorasRaras.Domain.Entities
{
    public class PerfilEntity : BaseEntity
    {
        public string Nome { get; set; }

        public virtual ICollection<UsuarioEntity> Usuarios { get; set; }


    }
}
