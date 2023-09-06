using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    public class PeriodicityDaysConfiguration : IEntityTypeConfiguration<PeriodicityDays>
    {
        public void Configure(EntityTypeBuilder<PeriodicityDays> builder)
        {
            var tableName = "dias_periodicidad";
            builder.ToTable(tableName, TruckACLContext.ACL_SCHEMA);
            builder.Property(x => x.Id).HasColumnName($"{tableName}_id");
            builder.Property(x => x.DaysCount).HasColumnName($"{tableName}_cantidad_dias");
        }
    }
}
