using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class LevelTruckPortalEntityTypeConfiguration : IEntityTypeConfiguration<LevelTruckPortal>
    {
        public void Configure(EntityTypeBuilder<LevelTruckPortal> builder)
        {
            builder.ToTable("Nivel_Truck_Portal", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("nivel_truck_portal_id");
            builder.Ignore(e => e.Name);
            builder.Property(e => e.LevelTruck).HasColumnName("nivel_truck_portal_nivel_truck");
            builder.Property(e => e.LevelTruckName).HasColumnName("nivel_truck_portal_nombre_truck").HasMaxLength(50).IsRequired();
            builder.Property(e => e.LevelPortalId).HasColumnName("nivel_id");
            builder.Property(e => e.LevelPortalName).HasColumnName("nivel_nombre").HasMaxLength(50).IsRequired();
            builder.Property(e => e.TypeEmployeeTruck).HasColumnName("nivel_truck_portal_tipo_Empleado").HasMaxLength(10).IsRequired();
            builder.Property(e => e.RolPortalId).HasColumnName("rol_id");

        }
    }

}
