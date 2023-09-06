using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class StructureClientNodeEntityTypeConfiguration : IEntityTypeConfiguration<StructureClientNode>
    {
        public void Configure(EntityTypeBuilder<StructureClientNode> builder)
        {
            builder.ToTable("Nodo_Cliente", StructureContext.DEFAULT_SCHEMA);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("nodo_cliente_id");
            builder.Property(cr => cr.NodeId).HasColumnName("nodo_id").IsRequired();
            builder.Property(cr => cr.ClientId).HasColumnName("cliente_id").HasMaxLength(10).IsRequired();
            builder.Property(cr => cr.Name).HasColumnName("cliente_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.ClientState).HasColumnName("cliente_estado").HasMaxLength(1).IsRequired();
            builder.Property(cr => cr.ValidityFrom)
                                .HasColumnName("nodo_cliente_vigencia_desde")
                                .IsRequired();
            builder.Property(cr => cr.ValidityTo)
                                .HasColumnName("nodo_cliente_vigencia_hasta")
                                .IsRequired();

            builder.HasOne(d => d.Node)
               .WithMany(p => p.StructureClientNodes)
               .HasForeignKey(d => d.NodeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
