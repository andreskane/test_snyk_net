using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class TimeZones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "seguimiento_cambio_estado_fecha_hora",
                schema: "dbo",
                table: "Seguimiento_Cambio_Estado",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "seguimiento_cambio_vigencia",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "seguimiento_cambio_fecha",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "nodo_definicion_vigencia_hasta",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "nodo_definicion_vigencia_desde",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "nodo_cliente_vigencia_hasta",
                schema: "dbo",
                table: "Nodo_Cliente",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "nodo_cliente_vigencia_desde",
                schema: "dbo",
                table: "Nodo_Cliente",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "estructura_vigencia_desde",
                schema: "dbo",
                table: "Estructura",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "arista_vigencia_hasta",
                schema: "dbo",
                table: "Arista",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "arista_vigencia_desde",
                schema: "dbo",
                table: "Arista",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.Sql("UPDATE dbo.Seguimiento_Cambio_Estado SET seguimiento_cambio_estado_fecha_hora = TODATETIMEOFFSET(seguimiento_cambio_estado_fecha_hora, '-03:00')");

            migrationBuilder.Sql("UPDATE dbo.Seguimiento_Cambio SET seguimiento_cambio_fecha = TODATETIMEOFFSET(seguimiento_cambio_fecha, '-03:00')");
            migrationBuilder.Sql("UPDATE dbo.Seguimiento_Cambio SET seguimiento_cambio_vigencia = TODATETIMEOFFSET(seguimiento_cambio_vigencia, '-03:00')");

            migrationBuilder.Sql("UPDATE dbo.Nodo_Definicion SET nodo_definicion_vigencia_desde = TODATETIMEOFFSET(nodo_definicion_vigencia_desde, '-03:00')");
            migrationBuilder.Sql("UPDATE dbo.Nodo_Definicion SET nodo_definicion_vigencia_hasta = TODATETIMEOFFSET(nodo_definicion_vigencia_hasta, '-03:00')");

            migrationBuilder.Sql("UPDATE dbo.Nodo_Cliente SET nodo_cliente_vigencia_desde = TODATETIMEOFFSET(nodo_cliente_vigencia_desde, '-03:00')");
            migrationBuilder.Sql("UPDATE dbo.Nodo_Cliente SET nodo_cliente_vigencia_hasta = TODATETIMEOFFSET(nodo_cliente_vigencia_hasta, '-03:00')");

            migrationBuilder.Sql("UPDATE dbo.Estructura SET estructura_vigencia_desde = TODATETIMEOFFSET(estructura_vigencia_desde, '-03:00')");

            migrationBuilder.Sql("UPDATE dbo.Arista SET arista_vigencia_desde = TODATETIMEOFFSET(arista_vigencia_desde, '-03:00')");
            migrationBuilder.Sql("UPDATE dbo.Arista SET arista_vigencia_hasta = TODATETIMEOFFSET(arista_vigencia_hasta, '-03:00')");

            migrationBuilder.Sql("UPDATE dbo.Arista SET arista_vigencia_hasta = '9999-12-31 20:59:59.9999999 -03:00' WHERE arista_vigencia_hasta = '9999-12-31 00:00:00.0000000 -03:00'");
            migrationBuilder.Sql("UPDATE dbo.Nodo_Definicion SET nodo_definicion_vigencia_hasta = '9999-12-31 20:59:59.9999999 -03:00' WHERE nodo_definicion_vigencia_hasta = '9999-12-31 00:00:00.0000000 -03:00'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "seguimiento_cambio_estado_fecha_hora",
                schema: "dbo",
                table: "Seguimiento_Cambio_Estado",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "seguimiento_cambio_vigencia",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "seguimiento_cambio_fecha",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "nodo_definicion_vigencia_hasta",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "nodo_definicion_vigencia_desde",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "nodo_cliente_vigencia_hasta",
                schema: "dbo",
                table: "Nodo_Cliente",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "nodo_cliente_vigencia_desde",
                schema: "dbo",
                table: "Nodo_Cliente",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "estructura_vigencia_desde",
                schema: "dbo",
                table: "Estructura",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "arista_vigencia_hasta",
                schema: "dbo",
                table: "Arista",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<DateTime>(
                name: "arista_vigencia_desde",
                schema: "dbo",
                table: "Arista",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }
    }
}
