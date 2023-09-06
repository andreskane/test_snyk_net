using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Delete_RoleTypeEmployeeTruck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rol_Tipo_Empleado_Truck",
                schema: "acl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rol_Tipo_Empleado_Truck",
                schema: "acl",
                columns: table => new
                {
                    rol_tipo_empleado_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol_id = table.Column<int>(type: "int", nullable: false),
                    tipo_empleado_truck_id = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol_Tipo_Empleado_Truck", x => x.rol_tipo_empleado_id);
                    table.ForeignKey(
                        name: "FK_Rol_Tipo_Empleado_Truck_Rol_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "dbo",
                        principalTable: "Rol",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rol_Tipo_Empleado_Truck_rol_id",
                schema: "acl",
                table: "Rol_Tipo_Empleado_Truck",
                column: "rol_id");
        }
    }
}
