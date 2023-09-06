using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class VersionedAristaEntityTypeConfiguration : IEntityTypeConfiguration<VersionedArista>
    {
        public void Configure(EntityTypeBuilder<VersionedArista> builder)
        {
            builder.ToTable("Versionado_Aristas", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("versionado_arista_id");
            builder.Ignore(e => e.Name);
            builder.Property(e => e.VersionedId).HasColumnName("versionado_id").IsRequired();
            builder.Property(e => e.AristaId).HasColumnName("arista_Id").IsRequired();
        }
    }

}
