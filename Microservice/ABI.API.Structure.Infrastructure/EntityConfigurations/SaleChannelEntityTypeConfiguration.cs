using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class SaleChannelEntityTypeConfiguration : IEntityTypeConfiguration<SaleChannel>
    {
        public void Configure(EntityTypeBuilder<SaleChannel> builder)
        {
            builder.ToTable("Canal_Venta", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(e => e.Id).HasColumnName("canal_venta_id");
            builder.Property(e => e.Name).HasColumnName("canal_venta_nombre").HasMaxLength(50).IsRequired();
            builder.Property(e => e.ShortName).HasColumnName("canal_venta_nombre_corto").HasMaxLength(10).IsRequired();
            builder.Property(e => e.Description).HasColumnName("canal_venta_descripcion").HasMaxLength(140);
            builder.Property(cr => cr.Active).HasColumnName("canal_venta_activo").IsRequired();


        }
    }
}
