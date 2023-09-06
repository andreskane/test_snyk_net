using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StructureModelEntityTypeConfiguration : IEntityTypeConfiguration<StructureModel>
    {
        public void Configure(EntityTypeBuilder<StructureModel> builder)
        {
            builder.ToTable("Modelo_Estructura", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("modelo_estructura_id");
            builder.Property(cr => cr.Name).HasColumnName("modelo_estructura_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.ShortName).HasColumnName("modelo_estructura_nombre_corto").HasMaxLength(10).IsRequired();
            builder.Property(cr => cr.Description).HasColumnName("modelo_estructura_descripcion").HasMaxLength(140);
            builder.Property(cr => cr.Active).HasColumnName("modelo_estructura_activo").IsRequired();
            builder.Property(e => e.CanBeExportedToTruck).HasColumnName("modelo_estructura_exportar_truck").IsRequired();
            builder.Property(e => e.Code).HasColumnName("modelo_estructura_codigo").HasMaxLength(3).IsRequired();
            builder.Property(cr => cr.CountryId).HasColumnName("pais_id");

            builder.HasOne(d => d.Country)
                .WithMany(p => p.StructureModels)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
