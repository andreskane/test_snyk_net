using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class BusinessTruckPortalEntityTypeConfiguration : IEntityTypeConfiguration<BusinessTruckPortal>
    {
        public void Configure(EntityTypeBuilder<BusinessTruckPortal> builder)
        {
            builder.ToTable("Modelo_Estructura_Empresa_Truck", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("modelo_estructura_empresa_truck_id");
            builder.Property(e => e.Name).HasColumnName("modelo_estructura_empresa_truck_nombre").HasMaxLength(50).IsRequired();
            builder.Property(e => e.BusinessCode).HasColumnName("modelo_estructura_empresa_truck_codigo").HasMaxLength(10).IsRequired();
            builder.Property(e => e.StructureModelId).HasColumnName("modelo_estructurta_id");

        }
    }

}
