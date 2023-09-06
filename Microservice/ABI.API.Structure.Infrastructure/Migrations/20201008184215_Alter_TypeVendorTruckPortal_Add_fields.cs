using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Alter_TypeVendorTruckPortal_Add_fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "mapeo_truck_lectura",
                schema: "acl",
                table: "Tipo_Vendedor_Truck_Portal",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "mapeo_truck_escritura",
                schema: "acl",
                table: "Tipo_Vendedor_Truck_Portal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mapeo_truck_lectura",
                schema: "acl",
                table: "Tipo_Vendedor_Truck_Portal");

            migrationBuilder.DropColumn(
                name: "mapeo_truck_escritura",
                schema: "acl",
                table: "Tipo_Vendedor_Truck_Portal");
        }
    }
}
