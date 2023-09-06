using ABI.API.Structure.ACL.Truck.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class ResourceResponsableEntityTypeConfiguration : IEntityTypeConfiguration<ResourceResponsable>
    {
        public void Configure(EntityTypeBuilder<ResourceResponsable> builder)
        {
            builder.ToTable("Recurso_responsable", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("recurso_responsable_id");
            builder.Property(cr => cr.ResourceId).HasColumnName("recurso_responsable_recurso_id").IsRequired(); 
            builder.Property(cr => cr.TruckId).HasColumnName("recurso_responsable_truck_id").IsRequired();
            builder.Property(cr => cr.IsVacant).HasColumnName("recurso_responsable_vacante").IsRequired();
            builder.Property(cr => cr.VendorCategory).HasColumnName("recurso_responsable_categoria_vendedor").HasMaxLength(1).IsRequired();
            builder.Property(cr => cr.CountryId).HasColumnName("recurso_responsable_paisId").IsRequired();
        }
    }
}
