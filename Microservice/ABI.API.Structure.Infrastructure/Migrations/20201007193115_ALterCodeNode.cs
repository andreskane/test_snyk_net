using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ALterCodeNode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "nodo_codigo",
                schema: "dbo",
                table: "Nodo",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "nodo_codigo",
                schema: "dbo",
                table: "Nodo",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }
    }
}
