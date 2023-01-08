namespace HorasRaras.Domain.Entities
{
    public class ProjetoEntity : BaseEntity
    {
        public string Nome { get; set; }
        public float? CustoPorHora { get; set; }
        public DateTime DataFinal { get; set; }

        public int ClienteId { get; set; }
        public virtual ClienteEntity Cliente { get; set; }
        
        public virtual ICollection<TarefaEntity> Tarefas { get; set; }
        public virtual ICollection<UsuarioProjetoEntity> UsuariosProjeto { get; set; }

    }
}
