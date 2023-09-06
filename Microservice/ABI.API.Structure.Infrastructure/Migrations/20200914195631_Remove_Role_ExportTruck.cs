using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Remove_Role_ExportTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "rol_exportar_truck",
            schema: "dbo",
            table: "Rol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
             name: "rol_exportar_truck",
             schema: "dbo",
             table: "Rol",
              nullable: false,
             defaultValue: true);
        }
    }
}
