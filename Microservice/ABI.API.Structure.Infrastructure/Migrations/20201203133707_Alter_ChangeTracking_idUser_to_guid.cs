using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Alter_ChangeTracking_idUser_to_guid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "seguimiento_cambio_id_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
