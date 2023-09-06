using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class NodeDefinition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "nodo_definicion_vigencia_desde",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "nodo_definicion_vigencia_desde",
                schema: "dbo",
                table: "Nodo_Definicion",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
