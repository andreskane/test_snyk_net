using ABI.API.Structure.Domain.AggregatesModel.StructureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class StructureNodeDefinitionEntityTypeConfiguration : IEntityTypeConfiguration<StructureNodeDefinition>
    {
        public void Configure(EntityTypeBuilder<StructureNodeDefinition> builder)
        {
            builder.ToTable("Nodo_Definicion", StructureContext.DEFAULT_SCHEMA);
            builder.Ignore(b => b.DomainEvents);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("nodo_definicion_id");
            builder.Property(cr => cr.NodeId).HasColumnName("nodo_id");
            builder.Property(cr => cr.ValidityFrom)
                                .HasColumnName("nodo_definicion_vigencia_desde")
                                .IsRequired();
            builder.Property(cr => cr.ValidityTo)
                                .HasColumnName("nodo_definicion_vigencia_hasta")
                                .IsRequired();
            builder.Property(cr => cr.AttentionModeId).HasColumnName("modo_atencion_id");
            builder.Property(cr => cr.RoleId).HasColumnName("rol_id");
            builder.Property(cr => cr.SaleChannelId).HasColumnName("canal_venta_id");
            builder.Property(cr => cr.EmployeeId).HasColumnName("empleado_id");
            builder.Property(cr => cr.Name).HasColumnName("nodo_nombre").HasMaxLength(50).IsRequired();
            builder.Property(cr => cr.Active).HasColumnName("nodo_activo");
            builder.Property(cr => cr.MotiveStateId).HasColumnName("motivo_estado_id").IsRequired().HasDefaultValue(2);
            builder.Property(cr => cr.VacantPerson).HasColumnName("nodo_definicion_persona_vacante");

            builder.HasOne(d => d.Node)
               .WithMany(p => p.StructureNodoDefinitions)
               .HasForeignKey(d => d.NodeId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.AttentionMode)
              .WithMany(p => p.StructureNodoDefinitions)
              .HasForeignKey(d => d.AttentionModeId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Role)
              .WithMany(p => p.StructureNodoDefinitions)
              .HasForeignKey(d => d.RoleId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.SaleChannel)
             .WithMany(p => p.StructureNodoDefinitions)
             .HasForeignKey(d => d.SaleChannelId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.MotiveState)
         .WithMany(p => p.StructureNodoDefinitions)
         .HasForeignKey(d => d.MotiveStateId)
         .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
