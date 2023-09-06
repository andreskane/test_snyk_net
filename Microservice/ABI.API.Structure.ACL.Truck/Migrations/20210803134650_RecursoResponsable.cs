using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore.Migrations;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class RecursoResponsable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recurso_responsable",
                schema: "acl",
                columns: table => new
                {
                    recurso_responsable_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recurso_responsable_truck_id = table.Column<int>(type: "int", nullable: false),
                    recurso_responsable_vacante = table.Column<bool>(type: "bit", nullable: false),
                    recurso_responsable_sociedad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recurso_responsable_categoria_vendedor = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurso_responsable", x => x.recurso_responsable_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recurso_responsable",
                schema: "acl");
        }
    }
}
