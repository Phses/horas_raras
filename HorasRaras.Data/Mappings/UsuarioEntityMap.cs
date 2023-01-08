using HorasRaras.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorasRaras.Data.Mappings;


// FLUENT API: Fluent Mapping
public class UsuarioEntityMap : IEntityTypeConfiguration<UsuarioEntity>
{
    public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.HashSenha).IsRequired(false);
        
        //# AVALIAR ESSA RELAÇÃO
        builder.HasOne(x => x.Perfil).WithMany(x => x.Usuarios).OnDelete(DeleteBehavior.Restrict);

        

    // DISCUTIR O QUE VAI SER NECESSÁRIO DESSAS CONFIGURAÇÕES
        // builder.Property(x => x.Id).HasColumnName("id");
        // builder.Property(x => x.Ativo).HasColumnName("ativo");
        // builder.Property(x => x.DataCriaçao).HasColumnName("dataCriaçao");
        // builder.Property(x => x.DataAlteraçao).HasColumnName("dataAlteraçao");
        // builder.Property(x => x.Nome).HasColumnName("nome");
        // builder.Property(x => x.Telefone).HasColumnName("telefone");
        // builder.Property(x => x.Email).HasColumnName("email");
        // builder.Property(x => x.Senha).HasColumnName("senha");
        // builder.Property(x => x.CPF).HasColumnName("cpf");
        // builder.Property(x => x.DataNascimento).HasColumnName("dataNascimento");
        // builder.Property(x => x.EndereçoId).HasColumnName("endereçoId");
        // builder.Property(x => x.Telefone).IsRequired(false);
        // builder.ToTable("usuarios");
    }
}