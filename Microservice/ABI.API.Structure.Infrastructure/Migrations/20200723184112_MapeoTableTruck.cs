using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class MapeoTableTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "acl");

            migrationBuilder.CreateTable(
                name: "Modelo_Estructura_Empresa_Truck",
                schema: "acl",
                columns: table => new
                {
                    modelo_estructura_empresa_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_estructura_empresa_truck_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    modelo_estructura_empresa_truck_codigo = table.Column<string>(maxLength: 10, nullable: false),
                    modelo_estructurta_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo_Estructura_Empresa_Truck", x => x.modelo_estructura_empresa_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Nivel_Truck_Portal",
                schema: "acl",
                columns: table => new
                {
                    nivel_truck_portal_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel_truck_portal_nivel_truck = table.Column<int>(nullable: false),
                    nivel_truck_portal_nombre_truck = table.Column<string>(maxLength: 50, nullable: false),
                    nivel_id = table.Column<int>(nullable: false),
                    nivel_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    nivel_truck_portal_tipo_Empleado = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nivel_Truck_Portal", x => x.nivel_truck_portal_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Modelo_Estructura_Empresa_Truck",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Nivel_Truck_Portal",
                schema: "acl");
        }
    }
}
