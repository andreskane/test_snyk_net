using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class MotiveStateEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.MotiveState>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.MotiveState> builder)
        {
            builder.ToTable("Motivo_Estado", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("motivo_estado_id");
            builder.Ignore(cr => cr.Name);
            builder.Property(cr => cr.StateId).HasColumnName("estado_id").IsRequired();
            builder.Property(cr => cr.MotiveId).HasColumnName("motivo_id").IsRequired();


            builder.HasOne(d => d.Motive)
           .WithMany(p => p.MotiveStates)
           .HasForeignKey(d => d.MotiveId)
           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.State)
            .WithMany(p => p.MotiveStates)
            .HasForeignKey(d => d.StateId)
            .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
