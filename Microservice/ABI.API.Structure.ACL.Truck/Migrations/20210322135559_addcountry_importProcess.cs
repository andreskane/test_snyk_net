using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class addcountry_importProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "estruct_territorio_io_pais_desc",
                schema: "acl",
                table: "estruct_territorio_io",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "estruct_territorio_io_pais_id",
                schema: "acl",
                table: "estruct_territorio_io",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estruct_territorio_io_pais_desc",
                schema: "acl",
                table: "estruct_territorio_io");

            migrationBuilder.DropColumn(
                name: "estruct_territorio_io_pais_id",
                schema: "acl",
                table: "estruct_territorio_io");
        }
    }
}
