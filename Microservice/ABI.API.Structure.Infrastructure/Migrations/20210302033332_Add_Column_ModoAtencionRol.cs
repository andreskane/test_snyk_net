using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Add_Column_ModoAtencionRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "modo_atencion_rol_responsable",
                schema: "dbo",
                table: "Modo_Atencion_Rol",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modo_atencion_rol_responsable",
                schema: "dbo",
                table: "Modo_Atencion_Rol");
        }
    }
}
