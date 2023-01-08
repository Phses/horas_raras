namespace HorasRaras.Domain.Entities
{
    public  class ClienteEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ProjetoEntity> Projetos { get; set; }

    }
}
