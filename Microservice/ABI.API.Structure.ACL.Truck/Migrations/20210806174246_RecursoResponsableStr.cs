using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore.Migrations;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class RecursoResponsableStr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "acl",
                table: "Recurso_responsable");

            migrationBuilder.AlterColumn<string>(
                name: "recurso_responsable_truck_id",
                schema: "acl",
                table: "Recurso_responsable",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "recurso_responsable_truck_id",
                schema: "acl",
                table: "Recurso_responsable",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "acl",
                table: "Recurso_responsable",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
