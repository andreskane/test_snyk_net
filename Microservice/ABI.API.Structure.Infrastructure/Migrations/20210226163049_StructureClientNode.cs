using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class StructureClientNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodo_Cliente",
                schema: "dbo",
                columns: table => new
                {
                    nodo_cliente_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nodo_id = table.Column<int>(nullable: false),
                    cliente_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    cliente_id = table.Column<string>(maxLength: 10, nullable: false),
                    cliente_estado = table.Column<string>(maxLength: 1, nullable: false),
                    nodo_cliente_vigencia_desde = table.Column<DateTime>(type: "date", nullable: false),
                    nodo_cliente_vigencia_hasta = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodo_Cliente", x => x.nodo_cliente_id);
                    table.ForeignKey(
                        name: "FK_Nodo_Cliente_Nodo_nodo_id",
                        column: x => x.nodo_id,
                        principalSchema: "dbo",
                        principalTable: "Nodo",
                        principalColumn: "nodo_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_Cliente_nodo_id",
                schema: "dbo",
                table: "Nodo_Cliente",
                column: "nodo_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nodo_Cliente",
                schema: "dbo");
        }
    }
}
