using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StructureNodeAristaEntityTypeConfiguration : IEntityTypeConfiguration<StructureArista>
    {
        public void Configure(EntityTypeBuilder<StructureArista> builder)
        {
            builder.ToTable("Arista", StructureContext.DEFAULT_SCHEMA);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("arista_id");
            builder.Property(cr => cr.StructureIdFrom).HasColumnName("estructura_id_desde");
            builder.Property(cr => cr.NodeIdFrom).HasColumnName("nodo_id_desde");
            builder.Property(cr => cr.StructureIdTo).HasColumnName("estructura_id_hasta");
            builder.Property(cr => cr.NodeIdTo).HasColumnName("nodo_id_hasta");
            builder.Property(cr => cr.TypeRelationshipId).HasColumnName("tipo_relacion_id");
            builder.Property(cr => cr.ValidityFrom).HasColumnName("arista_vigencia_desde").IsRequired();
            builder.Property(cr => cr.ValidityTo).HasColumnName("arista_vigencia_hasta").IsRequired();
            builder.Property(cr => cr.MotiveStateId).HasColumnName("motivo_estado_id").IsRequired().HasDefaultValue(2);

            builder.HasOne(d => d.StructureTo)
                .WithMany(p => p.AristaTo)
                .HasForeignKey(d => d.StructureIdTo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.StructureFrom)
                 .WithMany(p => p.AristaFrom)
                 .HasForeignKey(d => d.StructureIdFrom)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.NodeTo)
               .WithMany(p => p.AristasTo)
               .HasForeignKey(d => d.NodeIdTo)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.NodeFrom)
             .WithMany(p => p.AristasFrom)
             .HasForeignKey(d => d.NodeIdFrom)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.TypeRelationship)
             .WithMany(p => p.StructureArista)
             .HasForeignKey(d => d.TypeRelationshipId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.MotiveState)
         .WithMany(p => p.StructureAristas)
         .HasForeignKey(d => d.MotiveStateId)
         .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
