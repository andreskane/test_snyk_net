using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class TypeVendorTruckPortalEntityTypeConfiguration : IEntityTypeConfiguration<TypeVendorTruckPortal>
    {
        public void Configure(EntityTypeBuilder<TypeVendorTruckPortal> builder)
        {
            builder.ToTable("Tipo_Vendedor_Truck_Portal", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("tipo_vendedor_truck_portal_id");
            builder.Property(e => e.VendorTruckId).HasColumnName("vendedor_truck_id").IsRequired();
            builder.Property(e => e.VendorTruckName).HasColumnName("vendedor_truck_Name").HasMaxLength(50).IsRequired();
            builder.Ignore(e => e.Name);
            builder.Property(e => e.AttentionModeId).HasColumnName("modo_atencion_id");
            builder.Property(e => e.RoleId).HasColumnName("rol_id");
            builder.Property(e => e.MappingTruckReading).HasColumnName("mapeo_truck_lectura");
            builder.Property(e => e.MappingTruckWriting).HasColumnName("mapeo_truck_escritura");
        }
    }
}
