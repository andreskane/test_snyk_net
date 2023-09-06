using ABI.API.Structure.ACL.Truck.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ABI.API.Structure.ACL.Truck.Infrastructure.EntityConfigurations
{
    public class EstructuraClienteTerritorioIOConfiguration : IEntityTypeConfiguration<EstructuraClienteTerritorioIO>
    {
        public void Configure(EntityTypeBuilder<EstructuraClienteTerritorioIO> builder)
        {
            var tableName = "et_io";
            builder.ToTable(tableName, TruckACLContext.ACL_SCHEMA);

            builder.Property(x => x.Id).HasColumnName($"{tableName}_id");
            builder.Property(x => x.ImportProcessId).HasColumnName($"{tableName}_proceso_id");
            builder.Property(x => x.GerenciaID).HasColumnName($"{tableName}_GerenciaID");
            builder.Property(x => x.CliId).HasColumnName($"{tableName}_CliId");
            builder.Property(x => x.CliNom).HasColumnName($"{tableName}_CliNom");
            builder.Property(x => x.CliSts).HasColumnName($"{tableName}_CliSts");
            builder.Property(x => x.CliTrrId).HasColumnName($"{tableName}_CliTrrId");
            builder.Property(x => x.EmpId).HasColumnName($"{tableName}_EmpId");

            builder.Property(x => x.Pais_ID).HasColumnName($"{tableName}_pais_id");
            builder.Property(x => x.Pais_Desc).HasColumnName($"{tableName}_pais_desc");

                  
        /*

             public string GerenciaID { get; set; }//": "100"
     public string CliId { get; set; }//: "047905",
    public string        CliNom { get; set; }//: "INC S.A.",
     public string         CliSts { get; set; }//: "1",
     public string         CliTrrId { get; set; }//: "29001",
     public string         EmpId { get; set; }//: "001"

     */
    }
    }
}
