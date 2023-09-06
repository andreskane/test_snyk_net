using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class TypeGroupEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.TypeGroup>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.TypeGroup> builder)
        {
            builder.ToTable("Grupo_Tipo", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("grupo_tipo_id");
            builder.Property(cr => cr.Name).HasColumnName("grupo_tipo_nombre").HasMaxLength(50).IsRequired();

        }
    }
}
