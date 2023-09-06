using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class StructureModelCanBeExportedToTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
             name: "modelo_estructura_exportar_truck",
             schema: "dbo",
             table: "Modelo_Estructura",
              nullable: false,
             defaultValue: false);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
            name: "modelo_estructura_exportar_truck",
            schema: "dbo",
            table: "Modelo_Estructura");
        }
    }
}
