namespace HorasRaras.Domain.Entities
{
    public class UsuarioProjetoEntity 
    {
        public int UsuarioId { get; set; }
        public virtual UsuarioEntity Usuario { get; set; }

        public int ProjetoId { get; set; }
        public virtual ProjetoEntity Projeto { get; set; }
      
    }
}
