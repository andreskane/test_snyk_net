
using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    internal static class AuditableConfiguration
    {
        internal static void AddAuditConfiguration<T>(this EntityTypeBuilder<T> builder, string tableName)
            where T : AuditableEntity
        {
            builder.Property(x => x.CreatedBy).HasColumnName($"{tableName}_id_usuario_alta");
            builder.Property(x => x.CreatedByName).HasColumnName($"{tableName}_nombre_usuario_alta");
            builder.Property(x => x.CreatedDate).HasColumnName($"{tableName}_fecha_alta");
            builder.Property(x => x.LastModifiedBy).HasColumnName($"{tableName}_id_usuario_ultima_modificacion");
            builder.Property(x => x.LastModifiedByName).HasColumnName($"{tableName}_nombre_usuario_ultima_modificacion");
            builder.Property(x => x.LastModifiedDate).HasColumnName($"{tableName}_fecha_ultima_modificacion");
            builder.Property(x => x.CompanyId).HasColumnName($"{tableName}_sociedad_id").HasDefaultValue("01AR");
        }
    }
}
