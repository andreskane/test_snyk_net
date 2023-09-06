using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    [ExcludeFromCodeCoverage]
    public class VersionedEntityTypeConfiguration : IEntityTypeConfiguration<Versioned>
    {
        public void Configure(EntityTypeBuilder<Versioned> builder)
        {
            builder.ToTable("Versionado", TruckACLContext.ACL_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("versionado_id");
            builder.Property(e => e.StructureId).HasColumnName("estructura_id").IsRequired();
            builder.Ignore(e => e.Name);
            builder.Property(e => e.Date).HasColumnName("versionado_fecha_hora").IsRequired();
            builder.Property(e => e.Version).HasColumnName("versionado_version").HasMaxLength(10);
            builder.Property(e => e.GenerateVersionDate).HasColumnName("versionado_fecha_generado_version");
            builder.Property(e => e.Validity).HasColumnName("versionado_vigencia").IsRequired();
            builder.Property(e => e.StatusId).HasColumnName("versionado_estado_id").IsRequired();
            builder.Property(e => e.User).HasColumnName("versionado_usuario").HasMaxLength(50).IsRequired();

            builder.HasMany(d => d.VersionedsArista)
                .WithOne(e => e.Versioned)
                .HasForeignKey(d => d.VersionedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.VersionedsNode)
             .WithOne(e => e.Versioned)
             .HasForeignKey(d => d.VersionedId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.VersionedsLog)
             .WithOne(e => e.Versioned)
             .HasForeignKey(d => d.VersionedId)
             .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
