using ABI.API.Structure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.Infrastructure.EntityConfigurations
{
    public class CountryEntityTypeConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Pais", StructureContext.DEFAULT_SCHEMA);
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Id).HasColumnName("pais_id");
            builder.Property(e => e.Name).HasColumnName("pais_nombre").HasMaxLength(50).IsRequired();
            builder.Property(e => e.Code).HasColumnName("pais_codigo_iso_3166_1_alfa2").HasMaxLength(2).HasColumnType("char(2)").IsRequired();
        }
    }
}
