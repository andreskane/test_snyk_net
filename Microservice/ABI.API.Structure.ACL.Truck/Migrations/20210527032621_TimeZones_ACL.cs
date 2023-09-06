using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class TimeZones_ACL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "versionado_vigencia",
                schema: "acl",
                table: "Versionado",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "versionado_fecha_hora",
                schema: "acl",
                table: "Versionado",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.Sql("UPDATE acl.Versionado SET versionado_vigencia = TODATETIMEOFFSET(versionado_vigencia, '-03:00')");
            migrationBuilder.Sql("UPDATE acl.Versionado SET versionado_fecha_hora = TODATETIMEOFFSET(versionado_fecha_hora, '-03:00')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "versionado_vigencia",
                schema: "acl",
                table: "Versionado",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "versionado_fecha_hora",
                schema: "acl",
                table: "Versionado",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
