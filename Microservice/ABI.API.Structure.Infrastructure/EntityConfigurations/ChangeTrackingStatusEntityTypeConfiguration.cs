using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class ChangeTrackingStatusEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ChangeTrackingStatus>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ChangeTrackingStatus> builder)
        {
            builder.ToTable("Seguimiento_Cambio_Estado", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("seguimiento_cambio_estado_id");
            builder.Property(cr => cr.IdChangeTracking).HasColumnName("seguimiento_cambio_id").IsRequired();
            builder.Property(cr => cr.IdStatus).HasColumnName("estado_id").IsRequired();
            builder.Property(cr => cr.CreatedDate).HasColumnName("seguimiento_cambio_estado_fecha_hora").IsRequired();

            builder.HasOne(e => e.ChangeTracking).WithMany(p => p.ChangeTrackingStatusListItems).HasForeignKey(e => e.IdChangeTracking).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.Status).WithMany(p => p.ChangeStatus).HasForeignKey(d => d.IdStatus).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
