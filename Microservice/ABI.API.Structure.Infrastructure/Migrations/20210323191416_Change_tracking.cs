using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Change_tracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seguimiento_Cambio_Objeto_Tipo_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropIndex(
                name: "IX_Seguimiento_Cambio_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_valor_nuevo",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_valor_anterior",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_nombre_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.RenameColumn(
                name: "seguimiento_cambio_id_estructura",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                newName: "seguimiento_cambio_estructura_id");

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_atributo_valor",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_id_tipo_cambio",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_ruta_nodo",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "seguimiento_cambio_vigencia",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "seguimiento_cambio_id_tipo_cambio",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_atributo_valor",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_tipo_cambio",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_ruta_nodo",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_vigencia",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.RenameColumn(
                name: "seguimiento_cambio_estructura_id",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                newName: "seguimiento_cambio_id_estructura");

            migrationBuilder.AddColumn<int>(
                name: "seguimiento_cambio_id_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "seguimiento_cambio_id_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_valor_nuevo",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_valor_anterior",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "seguimiento_cambio_nombre_usuario",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_Cambio_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_id_tipo_objeto");

            migrationBuilder.AddForeignKey(
                name: "FK_Seguimiento_Cambio_Objeto_Tipo_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_id_tipo_objeto",
                principalSchema: "dbo",
                principalTable: "Objeto_Tipo",
                principalColumn: "objeto_tipo_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AlterColumn<string>(
                name: "seguimiento_cambio_id_tipo_cambio",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
