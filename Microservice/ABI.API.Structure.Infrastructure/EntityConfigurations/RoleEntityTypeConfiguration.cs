using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Rol", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(e => e.Id).HasColumnName("rol_id");
            builder.Property(cr => cr.Name).HasColumnName("rol_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.ShortName).HasColumnName("rol_nombre_corto").HasMaxLength(10).IsRequired();
            builder.Property(cr => cr.Active).HasColumnName("rol_activo").IsRequired();
            builder.Property(cr => cr.Tags).HasColumnName("rol_etiquetas").HasMaxLength(100);
        }

    }
}
