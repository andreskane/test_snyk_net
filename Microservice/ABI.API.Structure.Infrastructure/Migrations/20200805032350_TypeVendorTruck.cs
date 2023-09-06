using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class TypeVendorTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tipo_Vendedor_Truck",
                schema: "acl",
                columns: table => new
                {
                    tipo_vendedor_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modo_atencion_rol_id = table.Column<int>(nullable: false),
                    vendedor_truck_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor_Truck", x => x.tipo_vendedor_truck_id);
                    table.ForeignKey(
                        name: "FK_Tipo_Vendedor_Truck_Modo_Atencion_Rol_modo_atencion_rol_id",
                        column: x => x.modo_atencion_rol_id,
                        principalSchema: "dbo",
                        principalTable: "Modo_Atencion_Rol",
                        principalColumn: "modo_atencion_rol_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Vendedor_Truck_modo_atencion_rol_id",
                schema: "acl",
                table: "Tipo_Vendedor_Truck",
                column: "modo_atencion_rol_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tipo_Vendedor_Truck",
                schema: "acl");
        }
    }
}
