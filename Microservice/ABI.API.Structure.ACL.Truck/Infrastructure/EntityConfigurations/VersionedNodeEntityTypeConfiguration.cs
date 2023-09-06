using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class VersionedNodeEntityTypeConfiguration : IEntityTypeConfiguration<VersionedNode>
    {
        public void Configure(EntityTypeBuilder<VersionedNode> builder)
        {
            builder.ToTable("Versionado_Nodo", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("versionado_nodo_id");
            builder.Ignore(e => e.Name);
            builder.Property(e => e.VersionedId).HasColumnName("versionado_id").IsRequired();
            builder.Property(e => e.NodeId).HasColumnName("nodo_id").IsRequired();
            builder.Property(e => e.NodeDefinitionId).HasColumnName("nodo_definicion_id").IsRequired();
        }
    }

}
