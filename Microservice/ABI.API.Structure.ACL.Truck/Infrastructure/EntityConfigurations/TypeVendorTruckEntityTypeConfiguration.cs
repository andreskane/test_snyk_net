using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class TypeVendorTruckEntityTypeConfiguration : IEntityTypeConfiguration<TypeVendorTruck>
    {
        public void Configure(EntityTypeBuilder<TypeVendorTruck> builder)
        {
            builder.ToTable("Tipo_Vendedor_Truck", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("tipo_vendedor_truck_id");
            builder.Ignore(e => e.Name);
            builder.Property(e => e.AttentionModeRoleId).HasColumnName("modo_atencion_rol_id");
            builder.Property(e => e.VendorTruckId).HasColumnName("vendedor_truck_id");



        }
    }
}
