using HorasRaras.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HorasRaras.Data.Mappings;


// FLUENT API: Fluent Mapping
public class PerfilEntityMap : IEntityTypeConfiguration<PerfilEntity>
{
    public void Configure(EntityTypeBuilder<PerfilEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
    // DISCUTIR O QUE VAI SER NECESSÁRIO DESSAS CONFIGURAÇÕES
        // builder.Property(x => x.Id).HasColumnName("id");
        // builder.Property(x => x.Ativo).HasColumnName("ativo");
        // builder.Property(x => x.DataCriaçao).HasColumnName("dataCriaçao");
        // builder.Property(x => x.DataAlteraçao).HasColumnName("dataAlteraçao");
        // builder.Property(x => x.Nome).HasColumnName("nome");
        // builder.ToTable("perfis");
    }
}