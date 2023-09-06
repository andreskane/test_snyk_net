using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class VersionedStatusEntityTypeConfiguration : IEntityTypeConfiguration<VersionedStatus>
    {
        public void Configure(EntityTypeBuilder<VersionedStatus> builder)
        {
            builder.ToTable("Versionado_Estado", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("versionado_estado_id");
            builder.Property(e => e.Name).HasColumnName("versionado_estado_nombre").HasMaxLength(50).IsRequired();

            builder.HasMany(d => d.Versioneds)
             .WithOne(e => e.VersionedStatus)
             .HasForeignKey(d => d.StatusId)
             .OnDelete(DeleteBehavior.Restrict);


        }
    }

}
