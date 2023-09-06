using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class RemoveFKACL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "Log_Impacto_Truck_Aristas",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Estado",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Impacto_Estado",
                schema: "ACL");

            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck",
                schema: "ACL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "acl");

            migrationBuilder.EnsureSchema(
                name: "ACL");

            migrationBuilder.CreateTable(
                name: "Modelo_Estructura_Empresa_Truck",
                schema: "acl",
                columns: table => new
                {
                    modelo_estructura_empresa_truck_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_estructura_empresa_truck_codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    modelo_estructura_empresa_truck_nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    modelo_estructurta_id = table.Column<int>(type: "int", nullable: false)
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
                    nivel_truck_portal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel_id = table.Column<int>(type: "int", nullable: false),
                    nivel_nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nivel_truck_portal_nivel_truck = table.Column<int>(type: "int", nullable: false),
                    nivel_truck_portal_nombre_truck = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    rol_id = table.Column<int>(type: "int", nullable: true),
                    nivel_truck_portal_tipo_Empleado = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
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
                    tipo_vendedor_truck_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modo_atencion_rol_id = table.Column<int>(type: "int", nullable: false),
                    vendedor_truck_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor_Truck", x => x.tipo_vendedor_truck_id);
                    table.ForeignKey(
                        name: "FK_Tipo_Vendedor_Truck_Modo_Atencion_Rol_modo_atencion_rol_id",
                        column: x => x.modo_atencion_rol_id,
                        principalSchema: "dbo",
                        principalTable: "Modo_Atencion_Rol",
                        principalColumn: "modo_atencion_rol_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tipo_Vendedor_Truck_Portal",
                schema: "acl",
                columns: table => new
                {
                    tipo_vendedor_truck_portal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modo_atencion_id = table.Column<int>(type: "int", nullable: true),
                    mapeo_truck_lectura = table.Column<bool>(type: "bit", nullable: true),
                    mapeo_truck_escritura = table.Column<bool>(type: "bit", nullable: true),
                    rol_id = table.Column<int>(type: "int", nullable: true),
                    vendedor_truck_id = table.Column<int>(type: "int", nullable: false),
                    vendedor_truck_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo_Vendedor_Truck_Portal", x => x.tipo_vendedor_truck_portal_id);
                });

            migrationBuilder.CreateTable(
                name: "Impacto_Estado",
                schema: "ACL",
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
                schema: "ACL",
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
                schema: "ACL",
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
                        principalSchema: "ACL",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Log_Impacto_Truck_Estado_Impacto_Estado_log_impacto_truck_estado",
                        column: x => x.log_impacto_truck_estado,
                        principalSchema: "ACL",
                        principalTable: "Impacto_Estado",
                        principalColumn: "impacto_estado_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Nodos",
                schema: "ACL",
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
                        principalSchema: "ACL",
                        principalTable: "Log_Impacto_Truck",
                        principalColumn: "log_impacto_truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_Vendedor_Truck_modo_atencion_rol_id",
                schema: "acl",
                table: "Tipo_Vendedor_Truck",
                column: "modo_atencion_rol_id");

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
                name: "IX_Log_Impacto_Truck_Estado_log_impacto_truck_estado",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado",
                column: "log_impacto_truck_estado");

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Nodos_log_impacto_truck_id",
                schema: "ACL",
                table: "Log_Impacto_Truck_Nodos",
                column: "log_impacto_truck_id");
        }
    }
}
