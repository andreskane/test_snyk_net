using Microsoft.EntityFrameworkCore.Migrations;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    public partial class ResourceResponsable_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "recurso_responsableo_id",
                schema: "acl",
                table: "Recurso_responsable",
                newName: "recurso_responsable_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "recurso_responsable_id",
                schema: "acl",
                table: "Recurso_responsable",
                newName: "recurso_responsableo_id");
        }
    }
}
