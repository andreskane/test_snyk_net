using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class ObjectTypeEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ObjectType>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ObjectType> builder)
        {
            builder.ToTable("Objeto_Tipo", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("objeto_tipo_id");
            builder.Property(cr => cr.Name).HasColumnName("objeto_tipo_nombre").HasMaxLength(50).IsRequired();
        }
    }
}
