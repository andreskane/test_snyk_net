using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class change_importProcess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dias_periodicidad",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "estruct_territorio_io",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "proceso_importacion",
                schema: "dbo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dias_periodicidad",
                schema: "dbo",
                columns: table => new
                {
                    dias_periodicidad_id = table.Column<int>(type: "int", nullable: false),
                    dias_periodicidad_cantidad_dias = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dias_periodicidad", x => x.dias_periodicidad_id);
                });

            migrationBuilder.CreateTable(
                name: "estruct_territorio_io",
                schema: "dbo",
                columns: table => new
                {
                    estruct_territorio_io_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estruct_territorio_io_CliId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estruct_territorio_io_CliNom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estruct_territorio_io_CliSts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estruct_territorio_io_CliTrrId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estruct_territorio_io_EmpId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estruct_territorio_io_GerenciaID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estruct_territorio_io_proceso_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estruct_territorio_io", x => x.estruct_territorio_io_id);
                });

            migrationBuilder.CreateTable(
                name: "proceso_importacion",
                schema: "dbo",
                columns: table => new
                {
                    proceso_importacion_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    proceso_importacion_venta_promedio_diario = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    proceso_importacion_sociedad_id = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "01CL"),
                    proceso_importacion_condicion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proceso_importacion_id_usuario_alta = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    proceso_importacion_nombre_usuario_alta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proceso_importacion_fecha_alta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    proceso_importacion_fecha_fin_ejecucion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    proceso_importacion_eliminado = table.Column<bool>(type: "bit", nullable: false),
                    proceso_importacion_id_usuario_ultima_modificacion = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    proceso_importacion_nombre_usuario_ultima_modificacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    proceso_importacion_fecha_ultima_modificacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    proceso_importacion_periodicidad = table.Column<int>(type: "int", nullable: false),
                    proceso_importacion_fecha_proceso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    proceso_importacion_estado = table.Column<int>(type: "int", nullable: false),
                    proceso_importacion_cantidad_registros_procesados = table.Column<int>(type: "int", nullable: true),
                    proceso_importacion_origen = table.Column<int>(type: "int", nullable: false),
                    proceso_importacion_fecha_inicio_ejecucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_proceso_importacion", x => x.proceso_importacion_id);
                });
        }
    }
}
