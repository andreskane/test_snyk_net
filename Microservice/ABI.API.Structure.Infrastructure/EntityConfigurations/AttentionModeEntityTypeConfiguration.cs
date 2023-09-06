using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class AttentionModeEntityTypeConfiguration : IEntityTypeConfiguration<AttentionMode>
    {
        public void Configure(EntityTypeBuilder<AttentionMode> builder)
        {
            builder.ToTable("Modo_Atencion", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("modo_atencion_id");
            builder.Property(e => e.Name).HasColumnName("modo_atencion_nombre").HasMaxLength(50).IsRequired();
            builder.Property(e => e.ShortName).HasColumnName("modo_atencion_nombre_corto").HasMaxLength(10).IsRequired();
            builder.Property(e => e.Description).HasColumnName("modo_atencion_descripcion").HasMaxLength(140);
            builder.Property(cr => cr.Active).HasColumnName("modo_atencion_activo").IsRequired();



        }
    }
}
