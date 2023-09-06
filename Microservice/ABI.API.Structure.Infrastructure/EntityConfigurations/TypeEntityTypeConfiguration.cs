using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class TypeEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Type>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Type> builder)
        {
            builder.ToTable("Tipo", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("tipo_id");
            builder.Property(cr => cr.Name).HasColumnName("tipo_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.Code).HasColumnName("tipo_codigo");
            builder.Property(cr => cr.TypeGroupId).HasColumnName("grupo_tipo_id");

            builder.HasOne(d => d.TypeGroup)
                .WithMany(p => p.Types)
                .HasForeignKey(d => d.TypeGroupId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
