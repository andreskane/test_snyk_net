using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddIdsChangeTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "seguimiento_cambio_id_origen",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "seguimiento_cambio_id_destino",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "int",
                nullable: false,
                defaultValue: 0);            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_destino",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_origen",
                schema: "dbo",
                table: "Seguimiento_Cambio");
        }
    }
}
