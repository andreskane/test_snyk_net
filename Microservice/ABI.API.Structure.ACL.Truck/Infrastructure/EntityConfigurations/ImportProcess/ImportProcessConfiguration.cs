using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    public class ImportProcessConfiguration : IEntityTypeConfiguration<ImportProcess>
    {
        public void Configure(EntityTypeBuilder<ImportProcess> builder)
        {
            var tableName = "proceso_importacion";

            builder.ToTable(tableName, TruckACLContext.ACL_SCHEMA);

            builder.Property(x => x.Id).HasColumnName($"{tableName}_id");
            builder.Property(x => x.ProcessDate).HasColumnName($"{tableName}_fecha_proceso");
            builder.Property(x => x.Condition).HasColumnName($"{tableName}_condicion");
            builder.Property(x => x.Periodicity).HasColumnName($"{tableName}_periodicidad");
            builder.Property(x => x.ProcessState).HasColumnName($"{tableName}_estado");
            builder.Property(x => x.StartDate).HasColumnName($"{tableName}_fecha_inicio_ejecucion");
            builder.Property(x => x.EndDate).HasColumnName($"{tableName}_fecha_fin_ejecucion");
            builder.Property(x => x.ProcessedRows).HasColumnName($"{tableName}_cantidad_registros_procesados");
            builder.Property(x => x.IsDeleted).HasColumnName($"{tableName}_eliminado");
            builder.Property(x=> x.Source).HasColumnName($"{tableName}_origen");
            builder.Property(x => x.From).HasColumnName($"{tableName}_desde");
            builder.Property(x => x.To).HasColumnName($"{tableName}_hasta");
            builder.AddAuditConfiguration(tableName);
        }
    }
}
