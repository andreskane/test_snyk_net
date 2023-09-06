using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class addTablasLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ACL");

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck",
                schema: "ACL",
                columns: table => new
                {
                    log_impacto_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_fecha_hora = table.Column<DateTime>(nullable: false),
                    log_impacto_truck_version_truck = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck", x => x.log_impacto_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Aristas",
                schema: "ACL",
                columns: table => new
                {
                    log_impacto_truck_arista_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_id = table.Column<int>(nullable: false),
                    arista_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Aristas", x => x.log_impacto_truck_arista_id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Aristas_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "ACL",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Estado",
                schema: "ACL",
                columns: table => new
                {
                    log_Impacto_truck_Estado_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_id = table.Column<int>(nullable: false),
                    log_impacto_truck_fecha_hora = table.Column<DateTime>(nullable: false),
                    log_impacto_truck_estado = table.Column<int>(nullable: false),
                    LogTruck = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Estado", x => x.log_Impacto_truck_Estado_Id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Estado_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "ACL",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "ACL",
                columns: table => new
                {
                    log_impacto_truck_nodo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_id = table.Column<int>(nullable: false),
                    nodo_definicion_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck_Nodos", x => x.log_impacto_truck_nodo_id);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Nodos_Log_Impacto_Truck_log_impacto_truck_id",
                        column: x => x.log_impacto_truck_id,
                        principalSchema: "ACL",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Aristas_log_impacto_truck_id",
                schema: "ACL",
                table: "Log_Impacto_Truck_Aristas",
                column: "log_impacto_truck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Estado_log_impacto_truck_id",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado",
                column: "log_impacto_truck_id");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Nodos_log_impacto_truck_id",
                schema: "ACL",
                table: "Log_Impacto_Truck_Nodos",
                column: "log_impacto_truck_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Aristas",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Estado",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck",
                schema: "ACL");
        }
    }
}
