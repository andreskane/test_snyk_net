using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class change_schema_importProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "proceso_importacion",
                schema: "dbo",
                newName: "proceso_importacion",
                newSchema: "acl");

            migrationBuilder.RenameTable(
                name: "estruct_territorio_io",
                schema: "dbo",
                newName: "estruct_territorio_io",
                newSchema: "acl");

            migrationBuilder.RenameTable(
                name: "dias_periodicidad",
                schema: "dbo",
                newName: "dias_periodicidad",
                newSchema: "acl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "proceso_importacion",
                schema: "acl",
                newName: "proceso_importacion",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "estruct_territorio_io",
                schema: "acl",
                newName: "estruct_territorio_io",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "dias_periodicidad",
                schema: "acl",
                newName: "dias_periodicidad",
                newSchema: "dbo");
        }
    }
}
