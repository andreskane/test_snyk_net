using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class ADD_TAbLE_ACL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "acl");

            migrationBuilder.CreateTable(
                name: "Impacto_Estado",
                schema: "acl",
                columns: table => new
                {
                    impacto_estado_Id = table.Column<int>(nullable: false),
                    impacto_estado_nombre = table.Column<string>(maxLength: 100, nullable: false),
                    impacto_estado_codigo = table.Column<string>(maxLength: 10, nullable: false)
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
                    log_impacto_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_fecha_hora = table.Column<DateTime>(nullable: false),
                    log_impacto_truck_version_truck = table.Column<string>(maxLength: 10, nullable: false),
                    estructura_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_Impacto_Truck", x => x.log_impacto_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Modelo_Estructura_Empresa_Truck",
                schema: "acl",
                columns: table => new
                {
                    modelo_estructura_empresa_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_estructura_empresa_truck_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    modelo_estructura_empresa_truck_codigo = table.Column<string>(maxLength: 10, nullable: false),
                    modelo_estructurta_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo_Estructura_Empresa_Truck", x => x.modelo_estructura_empresa_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Nivel_Truck_Portal",
                schema: "acl",
                columns: table => new
                {
                    nivel_truck_portal_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel_truck_portal_nivel_truck = table.Column<int>(nullable: false),
                    nivel_truck_portal_nombre_truck = table.Column<string>(maxLength: 50, nullable: false),
                    nivel_id = table.Column<int>(nullable: false),
                    nivel_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    nivel_truck_portal_tipo_Empleado = table.Column<string>(maxLength: 10, nullable: false),
                    rol_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nivel_Truck_Portal", x => x.nivel_truck_portal_id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Vendedor_Truck",
                schema: "acl",
                columns: table => new
                {
                    tipo_vendedor_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modo_atencion_rol_id = table.Column<int>(nullable: false),
                    vendedor_truck_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor_Truck", x => x.tipo_vendedor_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Vendedor_Truck_Portal",
                schema: "acl",
                columns: table => new
                {
                    tipo_vendedor_truck_portal_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vendedor_truck_id = table.Column<int>(nullable: false),
                    vendedor_truck_Name = table.Column<string>(maxLength: 50, nullable: false),
                    modo_atencion_id = table.Column<int>(nullable: true),
                    rol_id = table.Column<int>(nullable: true),
                    mapeo_truck_lectura = table.Column<bool>(nullable: true),
                    mapeo_truck_escritura = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor_Truck_Portal", x => x.tipo_vendedor_truck_portal_id);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Aristas",
                schema: "acl",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Aristas",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Estado",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Modelo_Estructura_Empresa_Truck",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Nivel_Truck_Portal",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Tipo_Vendedor_Truck",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Tipo_Vendedor_Truck_Portal",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Impacto_Estado",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck",
                schema: "acl");
        }
    }
}
