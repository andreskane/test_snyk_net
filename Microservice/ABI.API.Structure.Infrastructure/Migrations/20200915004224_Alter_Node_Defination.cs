using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Alter_Node_Defination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nodo_activo",
                schema: "dbo",
                table: "Nodo");

            migrationBuilder.DropColumn(
                name: "nodo_nombre",
                schema: "dbo",
                table: "Nodo");

            migrationBuilder.AddColumn<bool>(
                name: "nodo_activo",
                schema: "dbo",
                table: "Nodo_Definicion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nodo_nombre",
                schema: "dbo",
                table: "Nodo_Definicion",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nodo_activo",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.DropColumn(
                name: "nodo_nombre",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.AddColumn<bool>(
                name: "nodo_activo",
                schema: "dbo",
                table: "Nodo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "nodo_nombre",
                schema: "dbo",
                table: "Nodo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
