using Microsoft.EntityFrameworkCore;
using HorasRaras.Domain.Entities;
using HorasRaras.Data.Mappings;

namespace HorasRaras.Data.Context
{
    public class HorasRarasContext : DbContext
    {
        public HorasRarasContext() { }

        DbSet<ClienteEntity> Clientes { get; set; }
        DbSet<PerfilEntity> Perfis { get; set; }
        DbSet<ProjetoEntity> Projetos { get; set; }
        DbSet<TarefaEntity> Tarefas { get; set; }
        DbSet<UsuarioEntity> Usuarios { get; set; }
        DbSet<UsuarioProjetoEntity> UsuarioProjetos { get; set; }

        public HorasRarasContext(DbContextOptions<HorasRarasContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEntity>(new ClienteEntityMap().Configure);
            modelBuilder.Entity<PerfilEntity>(new PerfilEntityMap().Configure);
            modelBuilder.Entity<ProjetoEntity>(new ProjetoEntityMap().Configure);
            modelBuilder.Entity<TarefaEntity>(new TarefaEntityMap().Configure);
            modelBuilder.Entity<UsuarioEntity>(new UsuarioEntityMap().Configure);
            modelBuilder.Entity<UsuarioProjetoEntity>(new UsuarioProjetoEntityMap().Configure);

        }
    }

}

