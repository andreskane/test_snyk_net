using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class AttentionModeRoleEntityTypeConfiguration : IEntityTypeConfiguration<AttentionModeRole>
    {
        public void Configure(EntityTypeBuilder<AttentionModeRole> builder)
        {

            builder.ToTable("Modo_Atencion_Rol", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Id).HasColumnName("modo_atencion_rol_id");
            builder.Ignore(e => e.Name);
            builder.Property(e => e.AttentionModeId).HasColumnName("modo_atencion_id");
            builder.Property(e => e.RoleId).HasColumnName("rol_id");
            builder.Property(e => e.EsResponsable).HasColumnName("modo_atencion_rol_responsable");

            builder.HasOne(d => d.AttentionMode)
                .WithMany(p => p.AttentionModeRoles)
                .HasForeignKey(d => d.AttentionModeId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(d => d.Role)
                .WithMany(p => p.AttentionModeRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
