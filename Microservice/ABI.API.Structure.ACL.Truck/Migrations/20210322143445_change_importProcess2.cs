using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class change_importProcess2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "proceso_importacion_venta_promedio_diario",
                schema: "acl",
                table: "proceso_importacion");

            migrationBuilder.AddColumn<int>(
                name: "proceso_importacion_desde",
                schema: "acl",
                table: "proceso_importacion",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "proceso_importacion_hasta",
                schema: "acl",
                table: "proceso_importacion",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "proceso_importacion_desde",
                schema: "acl",
                table: "proceso_importacion");

            migrationBuilder.DropColumn(
                name: "proceso_importacion_hasta",
                schema: "acl",
                table: "proceso_importacion");

            migrationBuilder.AddColumn<decimal>(
                name: "proceso_importacion_venta_promedio_diario",
                schema: "acl",
                table: "proceso_importacion",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
