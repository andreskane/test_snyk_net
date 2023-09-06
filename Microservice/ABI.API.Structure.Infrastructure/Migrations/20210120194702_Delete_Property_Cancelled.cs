using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Delete_Property_Cancelled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nodo_cancelado",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.DropColumn(
                name: "arista_cancelada",
                schema: "dbo",
                table: "Arista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "nodo_cancelado",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "arista_cancelada",
                schema: "dbo",
                table: "Arista",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
