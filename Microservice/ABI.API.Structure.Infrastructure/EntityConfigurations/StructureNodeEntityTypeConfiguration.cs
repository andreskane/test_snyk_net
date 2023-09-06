using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StructureNodeEntityTypeConfiguration : IEntityTypeConfiguration<StructureNode>
    {
        public void Configure(EntityTypeBuilder<StructureNode> builder)
        {
            builder.ToTable("Nodo", StructureContext.DEFAULT_SCHEMA);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("nodo_id");
            builder.Property(cr => cr.Code).HasColumnName("nodo_codigo").HasMaxLength(10).IsRequired();
            builder.Property(cr => cr.LevelId).HasColumnName("nivel_id");

            builder.Ignore(b => b.StructureNodoDefinition);
            builder.Ignore(b => b.StructureArista);

            builder.HasOne(d => d.Level)
               .WithMany(p => p.StructureNodos)
               .HasForeignKey(d => d.LevelId)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
