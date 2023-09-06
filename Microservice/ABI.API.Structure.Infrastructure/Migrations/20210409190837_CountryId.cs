using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class CountryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "pais_id",
                schema: "dbo",
                table: "Modelo_Estructura",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "pais_id",
                schema: "dbo",
                table: "Modelo_Estructura",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
