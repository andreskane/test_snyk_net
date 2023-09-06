using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StateEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Motive>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Motive> builder)
        {
            builder.ToTable("Motivo", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("motivo_id");
            builder.Property(cr => cr.Name).HasColumnName("motivo_nombre").HasMaxLength(50).IsRequired();
        }
    }
}
