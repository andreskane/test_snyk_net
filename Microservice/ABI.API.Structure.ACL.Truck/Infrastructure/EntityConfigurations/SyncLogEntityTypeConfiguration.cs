using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class SyncLogEntityTypeConfiguration : IEntityTypeConfiguration<SyncLog>
    {
        public void Configure(EntityTypeBuilder<SyncLog> builder)
        {
            builder.ToTable("Log_Sincronizacion", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("log_sincronizacion_id");
            builder.Property(e => e.Timestamp).HasColumnName("log_sincronizacion_timestamp").IsRequired();
            builder.Property(e => e.Message).HasColumnName("log_sincronizacion_mensaje").IsRequired();
        }
    }
}
