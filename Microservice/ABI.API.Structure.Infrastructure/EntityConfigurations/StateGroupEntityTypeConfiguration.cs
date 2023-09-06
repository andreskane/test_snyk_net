using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StateGroupEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.StateGroup>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.StateGroup> builder)
        {
            builder.ToTable("Grupo_Estado", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("grupo_estado_id");
            builder.Property(cr => cr.Name).HasColumnName("grupo_estado_nombre").HasMaxLength(50).IsRequired();
        }
    }
}
