using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class AlterTablePais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "pais_codigo_iso_3166_1_alfa2",
                schema: "dbo",
                table: "Pais",
                type: "char(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldMaxLength: 2);

            //Corrección de Codigos
            migrationBuilder.Sql("Update Estructura SET estructura_codigo = 'AR_VTA_ARGENTINA'  WHERE estructura_nombre = 'ARGENTINA'");
            migrationBuilder.Sql("Update Estructura SET estructura_codigo = 'AR_VTA_ARGENTINA-TRU'  WHERE estructura_nombre = 'ARGENTINA - TRUCK DEMO'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "pais_codigo_iso_3166_1_alfa2",
                schema: "dbo",
                table: "Pais",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(2)",
                oldMaxLength: 2);
        }
    }
}
