using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class change_name_et : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_estruct_territorio_io",
                schema: "acl",
                table: "estruct_territorio_io");

            migrationBuilder.RenameTable(
                name: "estruct_territorio_io",
                schema: "acl",
                newName: "et_io",
                newSchema: "acl");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_pais_id",
                schema: "acl",
                table: "et_io",
                newName: "et_io_pais_id");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_pais_desc",
                schema: "acl",
                table: "et_io",
                newName: "et_io_pais_desc");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_proceso_id",
                schema: "acl",
                table: "et_io",
                newName: "et_io_proceso_id");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_GerenciaID",
                schema: "acl",
                table: "et_io",
                newName: "et_io_GerenciaID");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_EmpId",
                schema: "acl",
                table: "et_io",
                newName: "et_io_EmpId");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_CliTrrId",
                schema: "acl",
                table: "et_io",
                newName: "et_io_CliTrrId");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_CliSts",
                schema: "acl",
                table: "et_io",
                newName: "et_io_CliSts");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_CliNom",
                schema: "acl",
                table: "et_io",
                newName: "et_io_CliNom");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_CliId",
                schema: "acl",
                table: "et_io",
                newName: "et_io_CliId");

            migrationBuilder.RenameColumn(
                name: "estruct_territorio_io_id",
                schema: "acl",
                table: "et_io",
                newName: "et_io_id");

            migrationBuilder.AlterColumn<string>(
                name: "proceso_importacion_sociedad_id",
                schema: "acl",
                table: "proceso_importacion",
                nullable: true,
                defaultValue: "01AR",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldDefaultValue: "01CL");

            migrationBuilder.AddPrimaryKey(
                name: "PK_et_io",
                schema: "acl",
                table: "et_io",
                column: "et_io_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_et_io",
                schema: "acl",
                table: "et_io");

            migrationBuilder.RenameTable(
                name: "et_io",
                schema: "acl",
                newName: "estruct_territorio_io",
                newSchema: "acl");

            migrationBuilder.RenameColumn(
                name: "et_io_pais_id",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_pais_id");

            migrationBuilder.RenameColumn(
                name: "et_io_pais_desc",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_pais_desc");

            migrationBuilder.RenameColumn(
                name: "et_io_proceso_id",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_proceso_id");

            migrationBuilder.RenameColumn(
                name: "et_io_GerenciaID",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_GerenciaID");

            migrationBuilder.RenameColumn(
                name: "et_io_EmpId",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_EmpId");

            migrationBuilder.RenameColumn(
                name: "et_io_CliTrrId",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_CliTrrId");

            migrationBuilder.RenameColumn(
                name: "et_io_CliSts",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_CliSts");

            migrationBuilder.RenameColumn(
                name: "et_io_CliNom",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_CliNom");

            migrationBuilder.RenameColumn(
                name: "et_io_CliId",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_CliId");

            migrationBuilder.RenameColumn(
                name: "et_io_id",
                schema: "acl",
                table: "estruct_territorio_io",
                newName: "estruct_territorio_io_id");

            migrationBuilder.AlterColumn<string>(
                name: "proceso_importacion_sociedad_id",
                schema: "acl",
                table: "proceso_importacion",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "01CL",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "01AR");

            migrationBuilder.AddPrimaryKey(
                name: "PK_estruct_territorio_io",
                schema: "acl",
                table: "estruct_territorio_io",
                column: "estruct_territorio_io_id");
        }
    }
}
