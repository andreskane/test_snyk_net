using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ADD_MotiveStateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "motivo_estado_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "motivo_estado_id",
                schema: "dbo",
                table: "Arista",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_Definicion_motivo_estado_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                column: "motivo_estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_Arista_motivo_estado_id",
                schema: "dbo",
                table: "Arista",
                column: "motivo_estado_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arista_Motivo_Estado_motivo_estado_id",
                schema: "dbo",
                table: "Arista",
                column: "motivo_estado_id",
                principalSchema: "dbo",
                principalTable: "Motivo_Estado",
                principalColumn: "motivo_estado_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nodo_Definicion_Motivo_Estado_motivo_estado_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                column: "motivo_estado_id",
                principalSchema: "dbo",
                principalTable: "Motivo_Estado",
                principalColumn: "motivo_estado_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arista_Motivo_Estado_motivo_estado_id",
                schema: "dbo",
                table: "Arista");

            migrationBuilder.DropForeignKey(
                name: "FK_Nodo_Definicion_Motivo_Estado_motivo_estado_id",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.DropIndex(
                name: "IX_Nodo_Definicion_motivo_estado_id",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.DropIndex(
                name: "IX_Arista_motivo_estado_id",
                schema: "dbo",
                table: "Arista");

            migrationBuilder.DropColumn(
                name: "motivo_estado_id",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.DropColumn(
                name: "motivo_estado_id",
                schema: "dbo",
                table: "Arista");
        }
    }
}
