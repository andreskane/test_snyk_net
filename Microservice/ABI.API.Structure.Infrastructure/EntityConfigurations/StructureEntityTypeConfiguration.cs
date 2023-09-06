using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StructureEntityTypeConfiguration : IEntityTypeConfiguration<StructureDomain>
    {
        public void Configure(EntityTypeBuilder<StructureDomain> builder)
        {
            builder.ToTable("Estructura", StructureContext.DEFAULT_SCHEMA);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("estructura_id");
            builder.Property(cr => cr.Name).HasColumnName("estructura_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.StructureModelId).HasColumnName("modelo_estructura_id");
            builder.Property(cr => cr.RootNodeId).HasColumnName("nodo_raiz_id");
            builder.Property(cr => cr.ValidityFrom).HasColumnName("estructura_vigencia_desde");
            builder.Property(cr => cr.Code).HasColumnName("estructura_codigo").HasMaxLength(20).IsRequired();

            builder.HasOne(d => d.Node)
                  .WithMany(p => p.Structures)
                  .HasForeignKey(d => d.RootNodeId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.StructureModel)
                  .WithMany(p => p.Structures)
                  .HasForeignKey(d => d.StructureModelId)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
