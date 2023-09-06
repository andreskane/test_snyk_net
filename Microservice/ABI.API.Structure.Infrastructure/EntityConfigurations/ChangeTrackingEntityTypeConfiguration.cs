using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class ChangeTrackingEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ChangeTracking>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ChangeTracking> builder)
        {
            builder.ToTable("Seguimiento_Cambio", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("seguimiento_cambio_id");
            builder.Property(cr => cr.IdStructure).HasColumnName("seguimiento_cambio_estructura_id").IsRequired();
            builder.Property(cr => cr.ValidityFrom).HasColumnName("seguimiento_cambio_vigencia").IsRequired();
            builder.Property(cr => cr.UserJson).HasColumnName("seguimiento_cambio_usuario").IsRequired();
            builder.Property(cr => cr.CreateDate).HasColumnName("seguimiento_cambio_fecha").IsRequired();
            builder.Property(cr => cr.IdObjectType).HasColumnName("tipo_id").IsRequired();
            builder.Property(cr => cr.IdChangeType).HasColumnName("seguimiento_cambio_id_tipo_cambio").IsRequired();
            builder.Property(cr => cr.ChangedValueJson).HasColumnName("seguimiento_cambio_atributo_valor").IsRequired();
            builder.Property(cr => cr.NodePathJson).HasColumnName("seguimiento_cambio_ruta_nodo").IsRequired();
            builder.Property(cr => cr.IdOrigen).HasColumnName("seguimiento_cambio_id_origen");
            builder.Property(cr => cr.IdDestino).HasColumnName("seguimiento_cambio_id_destino");

            builder.HasOne(e => e.ObjectType).WithMany(e => e.ChangeTracking).HasForeignKey(e => e.IdObjectType).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<StructureDomain>(e => e.Structure).WithMany(r => r.ChangeTrackingListItems).HasForeignKey(d => d.IdStructure);
              



        }
    }
}
