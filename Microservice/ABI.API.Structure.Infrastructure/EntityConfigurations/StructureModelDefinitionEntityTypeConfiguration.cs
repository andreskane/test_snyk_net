using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StructureModelDefinitionEntityTypeConfiguration : IEntityTypeConfiguration<StructureModelDefinition>
    {
        public void Configure(EntityTypeBuilder<StructureModelDefinition> builder)
        {
            builder.ToTable("Modelo_Estructura_Definicion", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("modelo_estructura_definicion_id");
            builder.Ignore(e => e.Name);
            builder.Property(cr => cr.StructureModelId).HasColumnName("modelo_estructura_id");
            builder.Property(cr => cr.LevelId).HasColumnName("nivel_id");
            builder.Property(cr => cr.ParentLevelId).HasColumnName("padre_nivel_id");
            builder.Property(cr => cr.IsAttentionModeRequired).HasColumnName("modelo_estructura_tiene_modo_atencion").IsRequired();
            builder.Property(cr => cr.IsSaleChannelRequired).HasColumnName("modelo_estructura_tiene_canal_venta").IsRequired();

            builder.HasOne(d => d.StructureModel)
                .WithMany(p => p.StructureModelsDefinitions)
                .HasForeignKey(d => d.StructureModelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Level)
                .WithMany(p => p.StructureModelsDefinitions)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.ParentLevel)
                .WithMany(p => p.ParentStructureModelsDefinitions)
                .HasForeignKey(d => d.ParentLevelId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
