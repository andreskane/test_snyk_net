using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ChangeTrackingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                newName: "tipo_id");

            migrationBuilder.AddColumn<string>(
                name: "tipo_codigo",
                schema: "dbo",
                table: "Tipo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Seguimiento_Cambio_Estado",
                schema: "dbo",
                columns: table => new
                {
                    seguimiento_cambio_estado_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_id = table.Column<int>(nullable: false),
                    seguimiento_cambio_id = table.Column<int>(nullable: false),
                    seguimiento_cambio_estado_fecha_hora = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguimiento_Cambio_Estado", x => x.seguimiento_cambio_estado_id);
                    table.ForeignKey(
                        name: "FK_Seguimiento_Cambio_Estado_Seguimiento_Cambio_seguimiento_cambio_id",
                        column: x => x.seguimiento_cambio_id,
                        principalSchema: "dbo",
                        principalTable: "Seguimiento_Cambio",
                        principalColumn: "seguimiento_cambio_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Seguimiento_Cambio_Estado_Estado_estado_id",
                        column: x => x.estado_id,
                        principalSchema: "dbo",
                        principalTable: "Estado",
                        principalColumn: "estado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_Cambio_tipo_id",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "tipo_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_Cambio_Estado_seguimiento_cambio_id",
                schema: "dbo",
                table: "Seguimiento_Cambio_Estado",
                column: "seguimiento_cambio_id");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_Cambio_Estado_estado_id",
                schema: "dbo",
                table: "Seguimiento_Cambio_Estado",
                column: "estado_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seguimiento_Cambio_Tipo_tipo_id",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "tipo_id",
                principalSchema: "dbo",
                principalTable: "Tipo",
                principalColumn: "tipo_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seguimiento_Cambio_Tipo_tipo_id",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropTable(
                name: "Seguimiento_Cambio_Estado",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Seguimiento_Cambio_tipo_id",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "tipo_codigo",
                schema: "dbo",
                table: "Tipo");

            migrationBuilder.RenameColumn(
                name: "tipo_id",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                newName: "seguimiento_cambio_id_tipo_objeto");
        }
    }
}
