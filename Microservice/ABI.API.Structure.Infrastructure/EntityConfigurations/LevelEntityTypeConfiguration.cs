using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class LevelEntityTypeConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.ToTable("Nivel", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(e => e.Id).HasColumnName("nivel_id");
            builder.Property(cr => cr.Name).HasColumnName("nivel_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.ShortName).HasColumnName("nivel_nombre_corto").HasMaxLength(10).IsRequired();
            builder.Property(cr => cr.Description).HasColumnName("nivel_descripcion").HasMaxLength(140);
            builder.Property(cr => cr.Active).HasColumnName("nivel_activo").IsRequired();

        }
    }
}
