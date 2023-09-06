using Microsoft.EntityFrameworkCore.Migrations;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    public partial class RecursoResponsable_Delete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recurso_responsable",
                schema: "acl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recurso_responsable",
                schema: "acl",
                columns: table => new
                {
                    recurso_responsable_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recurso_responsable_truck_id = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recurso_responsable_vacante = table.Column<bool>(type: "bit", nullable: false),
                    recurso_responsable_sociedad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    recurso_responsable_categoria_vendedor = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recurso_responsable", x => x.recurso_responsable_id);
                });
        }
    }
}
