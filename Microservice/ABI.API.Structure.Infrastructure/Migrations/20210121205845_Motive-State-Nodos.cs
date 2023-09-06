using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class MotiveStateNodos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupo_Estado",
                schema: "dbo",
                columns: table => new
                {
                    grupo_estado_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grupo_estado_nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo_Estado", x => x.grupo_estado_id);
                });

            migrationBuilder.CreateTable(
                name: "Motivo",
                schema: "dbo",
                columns: table => new
                {
                    motivo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    motivo_nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motivo", x => x.motivo_id);
                });

            migrationBuilder.CreateTable(
                name: "Estado",
                schema: "dbo",
                columns: table => new
                {
                    estado_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    grupo_estado_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado", x => x.estado_id);
                    table.ForeignKey(
                        name: "FK_Estado_Grupo_Estado_grupo_estado_id",
                        column: x => x.grupo_estado_id,
                        principalSchema: "dbo",
                        principalTable: "Grupo_Estado",
                        principalColumn: "grupo_estado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Motivo_Estado",
                schema: "dbo",
                columns: table => new
                {
                    motivo_estado_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_id = table.Column<int>(nullable: false),
                    motivo_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motivo_Estado", x => x.motivo_estado_id);
                    table.ForeignKey(
                        name: "FK_Motivo_Estado_Motivo_motivo_id",
                        column: x => x.motivo_id,
                        principalSchema: "dbo",
                        principalTable: "Motivo",
                        principalColumn: "motivo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Motivo_Estado_Estado_estado_id",
                        column: x => x.estado_id,
                        principalSchema: "dbo",
                        principalTable: "Estado",
                        principalColumn: "estado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estado_grupo_estado_id",
                schema: "dbo",
                table: "Estado",
                column: "grupo_estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_Motivo_Estado_motivo_id",
                schema: "dbo",
                table: "Motivo_Estado",
                column: "motivo_id");

            migrationBuilder.CreateIndex(
                name: "IX_Motivo_Estado_estado_id",
                schema: "dbo",
                table: "Motivo_Estado",
                column: "estado_id");



            migrationBuilder.InsertData("Motivo", new string[] { "motivo_id", "motivo_nombre" }, new object[] { 1, "Acción del Usuario" });

            migrationBuilder.InsertData("Grupo_Estado", new string[] { "grupo_estado_id", "grupo_estado_nombre" }, new object[] { 1, "Estados de Cambios Programados" });

            migrationBuilder.InsertData("Estado", new string[] { "estado_id", "estado_nombre", "grupo_estado_id" }, new object[] { 1, "Borrador", 1 });
            migrationBuilder.InsertData("Estado", new string[] { "estado_id", "estado_nombre", "grupo_estado_id" }, new object[] { 2, "Confirmado", 1 });
            migrationBuilder.InsertData("Estado", new string[] { "estado_id", "estado_nombre", "grupo_estado_id" }, new object[] { 3, "Cancelado", 1 });
            migrationBuilder.InsertData("Estado", new string[] { "estado_id", "estado_nombre", "grupo_estado_id" }, new object[] { 4, "Anulado", 1 });


            migrationBuilder.InsertData("Motivo_Estado", new string[] { "motivo_estado_id", "estado_id", "motivo_id" }, new object[] { 1, 1, 1 });
            migrationBuilder.InsertData("Motivo_Estado", new string[] { "motivo_estado_id", "estado_id", "motivo_id" }, new object[] { 2, 2, 1 });
            migrationBuilder.InsertData("Motivo_Estado", new string[] { "motivo_estado_id", "estado_id", "motivo_id" }, new object[] { 3, 3, 1 });
            migrationBuilder.InsertData("Motivo_Estado", new string[] { "motivo_estado_id", "estado_id", "motivo_id" }, new object[] { 4, 4, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Motivo_Estado",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Motivo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Estado",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Grupo_Estado",
                schema: "dbo");
        }
    }
}
