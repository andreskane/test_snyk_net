using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore.Migrations;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class MostVisitedFilters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filtro_Mas_Visitado",
                schema: "dbo",
                columns: table => new
                {
                    filtro_mas_visitado_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    filtro_mas_visitado_descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    filtro_mas_visitado_id_valor = table.Column<int>(type: "int", nullable: false),
                    filtro_mas_visitado_tipo_filtro = table.Column<int>(type: "int", nullable: false),
                    filtro_mas_visitado_cantidad = table.Column<int>(type: "int", nullable: false),
                    filtro_mas_visitado_estructura_id = table.Column<int>(type: "int", nullable: false),
                    filtro_mas_visitado_id_usuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filtro_Mas_Visitado", x => x.filtro_mas_visitado_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filtro_Mas_Visitado",
                schema: "dbo");
        }
    }
}
