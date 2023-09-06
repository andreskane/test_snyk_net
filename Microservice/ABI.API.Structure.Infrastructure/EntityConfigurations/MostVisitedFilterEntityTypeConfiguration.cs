using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class MostVisitedFilterEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.MostVisitedFilter>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.MostVisitedFilter> builder)
        {
            builder.ToTable("Filtro_Mas_Visitado", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("filtro_mas_visitado_id");
            builder.Property(cr => cr.StructureId).HasColumnName("filtro_mas_visitado_estructura_id").IsRequired();
            builder.Property(cr => cr.Description).HasColumnName("filtro_mas_visitado_descripcion").IsRequired();
            builder.Property(cr => cr.FilterType).HasColumnName("filtro_mas_visitado_tipo_filtro").IsRequired();
            builder.Property(cr => cr.IdValue).HasColumnName("filtro_mas_visitado_id_valor").IsRequired();
            builder.Property(cr => cr.Quantity).HasColumnName("filtro_mas_visitado_cantidad").IsRequired();
            builder.Property(cr => cr.UserId).HasColumnName("filtro_mas_visitado_id_usuario").IsRequired();
        }
    }
}
