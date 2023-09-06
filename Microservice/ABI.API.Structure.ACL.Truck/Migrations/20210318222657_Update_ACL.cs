using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Update_ACL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Aristas",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Bandejas",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Estado",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Impacto_Estado",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck",
                schema: "acl");

            migrationBuilder.AlterColumn<DateTime>(
                name: "versionado_fecha_generado_version",
                schema: "acl",
                table: "Versionado",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "versionado_fecha_generado_version",
                schema: "acl",
                table: "Versionado",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Impacto_Estado",
                schema: "acl",
                columns: table => new
                {
                    impacto_estado_Id = table.Column<int>(type: "int", nullable: false),
                    impacto_estado_codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    impacto_estado_nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impacto_Estado", x => x.impacto_estado_Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck",
                schema: "acl",
                columns: table => new
                {
                    log_impacto_truck_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_fecha_hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estructura_id = table.Column<int>(type: "int", nullable: true),
                    log_impacto_truck_version_truck = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck", x => x.log_impacto_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Aristas",
                schema: "acl",
                columns: table => new
                {
                    log_impacto_truck_arista_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    arista_Id = table.Column<int>(type: "int", nullable: false),
                    log_impacto_truck_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Aristas", x => x.log_impacto_truck_arista_id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Aristas_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "acl",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Bandejas",
                schema: "acl",
                columns: table => new
                {
                    log_impacto_truck_bandeja_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_id = table.Column<int>(type: "int", nullable: false),
                    log_impacto_truck_bandejas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Bandejas", x => x.log_impacto_truck_bandeja_id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Bandejas_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "acl",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Estado",
                schema: "acl",
                columns: table => new
                {
                    log_Impacto_truck_Estado_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_fecha_hora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    log_impacto_truck_id = table.Column<int>(type: "int", nullable: false),
                    LogTruck = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    log_impacto_truck_estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Estado", x => x.log_Impacto_truck_Estado_Id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Estado_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "acl",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Estado_Impacto_Estado_log_impacto_truck_estado",
                        column: x => x.log_impacto_truck_estado,
                        principalSchema: "acl",
                        principalTable: "Impacto_Estado",
                        principalColumn: "impacto_estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "acl",
                columns: table => new
                {
                    log_impacto_truck_nodo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_id = table.Column<int>(type: "int", nullable: false),
                    nodo_definicion_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Nodos", x => x.log_impacto_truck_nodo_id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Nodos_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "acl",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Aristas_log_impacto_truck_id",
                schema: "acl",
                table: "Log_Impacto_Truck_Aristas",
                column: "log_impacto_truck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Bandejas_log_impacto_truck_id",
                schema: "acl",
                table: "Log_Impacto_Truck_Bandejas",
                column: "log_impacto_truck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Estado_log_impacto_truck_id",
                schema: "acl",
                table: "Log_Impacto_Truck_Estado",
                column: "log_impacto_truck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Estado_log_impacto_truck_estado",
                schema: "acl",
                table: "Log_Impacto_Truck_Estado",
                column: "log_impacto_truck_estado");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Nodos_log_impacto_truck_id",
                schema: "acl",
                table: "Log_Impacto_Truck_Nodos",
                column: "log_impacto_truck_id");
        }
    }
}
