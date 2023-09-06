using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class VersionedLogStateEntityTypeConfiguration : IEntityTypeConfiguration<VersionedLogStatus>
    {
        public void Configure(EntityTypeBuilder<VersionedLogStatus> builder)
        {
            builder.ToTable("Versionado_Estado_Log", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("versionado_estado_log_Id").ValueGeneratedNever();
            builder.Property(e => e.Name).HasColumnName("versionado_estado_log_nombre").HasMaxLength(100).IsRequired();
            builder.Property(e => e.Code).HasColumnName("versionado_estado_log_codigo").HasMaxLength(10).IsRequired();

            builder.HasMany(d => d.VersionedLogs)
             .WithOne(e => e.LogStatus)
             .HasForeignKey(d => d.LogStatusId)
             .OnDelete(DeleteBehavior.Restrict);

        }





    }

}
