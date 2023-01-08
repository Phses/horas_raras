namespace HorasRaras.Domain.Entities
{
    public class TarefaEntity : BaseEntity
    {
        public string Descricao { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime? HoraFinal { get; set; }
        public double? HorasTotal { get; set; }

        public int ProjetoId { get; set; }
        public virtual ProjetoEntity Projeto { get; set; }

        public int UsuarioId { get; set; }
        public virtual UsuarioEntity Usuario { get; set; }
        
    }
}
