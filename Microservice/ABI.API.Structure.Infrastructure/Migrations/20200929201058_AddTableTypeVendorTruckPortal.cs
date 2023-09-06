using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AddTableTypeVendorTruckPortal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "rol_id",
                schema: "acl",
                table: "Nivel_Truck_Portal",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tipo_Vendedor_Truck_Portal",
                schema: "acl",
                columns: table => new
                {
                    tipo_vendedor_truck_portal_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vendedor_truck_id = table.Column<int>(nullable: false),
                    vendedor_truck_Name = table.Column<string>(maxLength: 50, nullable: false),
                    modo_atencion_id = table.Column<int>(nullable: true),
                    rol_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor_Truck_Portal", x => x.tipo_vendedor_truck_portal_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipo_Vendedor_Truck_Portal",
                schema: "acl");

            migrationBuilder.DropColumn(
                name: "rol_id",
                schema: "acl",
                table: "Nivel_Truck_Portal");
        }
    }
}
