using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Country : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "modelo_estructura_codigo",
                schema: "dbo",
                table: "Modelo_Estructura",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "pais_id",
                schema: "dbo",
                table: "Modelo_Estructura",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estructura_codigo",
                schema: "dbo",
                table: "Estructura",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Pais",
                schema: "dbo",
                columns: table => new
                {
                    pais_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pais_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    pais_codigo_iso_3166_1_alfa2 = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.pais_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_Estructura_pais_id",
                schema: "dbo",
                table: "Modelo_Estructura",
                column: "pais_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Modelo_Estructura_Pais_pais_id",
                schema: "dbo",
                table: "Modelo_Estructura",
                column: "pais_id",
                principalSchema: "dbo",
                principalTable: "Pais",
                principalColumn: "pais_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modelo_Estructura_Pais_pais_id",
                schema: "dbo",
                table: "Modelo_Estructura");

            migrationBuilder.DropTable(
                name: "Pais",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Modelo_Estructura_pais_id",
                schema: "dbo",
                table: "Modelo_Estructura");

            migrationBuilder.DropColumn(
                name: "modelo_estructura_codigo",
                schema: "dbo",
                table: "Modelo_Estructura");

            migrationBuilder.DropColumn(
                name: "pais_id",
                schema: "dbo",
                table: "Modelo_Estructura");

            migrationBuilder.DropColumn(
                name: "estructura_codigo",
                schema: "dbo",
                table: "Estructura");
        }
    }
}
