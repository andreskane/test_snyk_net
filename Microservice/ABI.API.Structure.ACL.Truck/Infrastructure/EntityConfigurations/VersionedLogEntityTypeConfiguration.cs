using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class VersionedLogEntityTypeConfiguration : IEntityTypeConfiguration<VersionedLog>
    {
        public void Configure(EntityTypeBuilder<VersionedLog> builder)
        {
            builder.ToTable("Versionado_Log", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("versionado_log_Id");
            builder.Ignore(e => e.Name);
            builder.Property(e => e.VersionedId).HasColumnName("versionado_id").IsRequired();
            builder.Property(e => e.Date).HasColumnName("versionado_log_fecha_hora").IsRequired();
            builder.Property(e => e.LogStatusId).HasColumnName("versionado_estado_log_Id").IsRequired();
            builder.Property(e => e.Detaill).HasColumnName("versionado_log_detalle").IsRequired();


        }
    }

}
