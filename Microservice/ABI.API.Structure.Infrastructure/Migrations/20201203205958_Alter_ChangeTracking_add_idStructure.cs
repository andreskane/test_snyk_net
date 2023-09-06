using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Alter_ChangeTracking_add_idStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seguimiento_Cambio_ObjectType_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ObjectType",
                table: "ObjectType");

            migrationBuilder.RenameTable(
                name: "ObjectType",
                newName: "Objeto_Tipo",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "Objeto_Tipo",
                newName: "objeto_tipo_nombre");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "Objeto_Tipo",
                newName: "objeto_tipo_id");

            migrationBuilder.AddColumn<int>(
                name: "seguimiento_cambio_id_estructura",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "objeto_tipo_nombre",
                schema: "dbo",
                table: "Objeto_Tipo",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Objeto_Tipo",
                schema: "dbo",
                table: "Objeto_Tipo",
                column: "objeto_tipo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seguimiento_Cambio_Objeto_Tipo_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_id_tipo_objeto",
                principalSchema: "dbo",
                principalTable: "Objeto_Tipo",
                principalColumn: "objeto_tipo_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.InsertData("Objeto_Tipo", new string[] { "objeto_tipo_id", "objeto_tipo_nombre" }, new object[] { 1, "Nodo" });
            migrationBuilder.InsertData("Objeto_Tipo", new string[] { "objeto_tipo_id", "objeto_tipo_nombre" }, new object[] { 2, "Arista" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Seguimiento_Cambio_Objeto_Tipo_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Objeto_Tipo",
                schema: "dbo",
                table: "Objeto_Tipo");

            migrationBuilder.DropColumn(
                name: "seguimiento_cambio_id_estructura",
                schema: "dbo",
                table: "Seguimiento_Cambio");

            migrationBuilder.RenameTable(
                name: "Objeto_Tipo",
                schema: "dbo",
                newName: "ObjectType");

            migrationBuilder.RenameColumn(
                name: "objeto_tipo_nombre",
                table: "ObjectType",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "objeto_tipo_id",
                table: "ObjectType",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ObjectType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ObjectType",
                table: "ObjectType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Seguimiento_Cambio_ObjectType_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_id_tipo_objeto",
                principalTable: "ObjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DeleteData("Objeto_Tipo", "objeto_tipo_id", 1);
            migrationBuilder.DeleteData("Objeto_Tipo", "objeto_tipo_id", 2);
        }
    }
}
