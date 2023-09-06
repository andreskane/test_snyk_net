using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class remove_estado_truck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estado_Truck",
                schema: "dbo");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_Cambio_seguimiento_cambio_estructura_id",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_estructura_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seguimiento_Cambio_Estructura_seguimiento_cambio_estructura_id",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_estructura_id",
                principalSchema: "dbo",
                principalTable: "Estructura",
                principalColumn: "estructura_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seguimiento_Cambio_Estructura_seguimiento_cambio_estructura_id",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropIndex(
                name: "IX_Seguimiento_Cambio_seguimiento_cambio_estructura_id",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.CreateTable(
                name: "Estado_Truck",
                schema: "dbo",
                columns: table => new
                {
                    estado_truck_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_truck_nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Truck", x => x.estado_truck_id);
                });
        }
    }
}
