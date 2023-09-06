using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class MotiveEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.State>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.State> builder)
        {
            builder.ToTable("Estado", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("estado_id");
            builder.Property(cr => cr.Name).HasColumnName("estado_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.StateGroupId).HasColumnName("grupo_estado_id").IsRequired();

            builder.HasOne(d => d.StateGroup)
          .WithMany(p => p.States)
          .HasForeignKey(d => d.StateGroupId)
          .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
