using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Canal_Venta",
                schema: "dbo",
                columns: table => new
                {
                    canal_venta_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    canal_venta_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    canal_venta_nombre_corto = table.Column<string>(maxLength: 10, nullable: false),
                    canal_venta_descripcion = table.Column<string>(maxLength: 140, nullable: true),
                    canal_venta_activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canal_Venta", x => x.canal_venta_id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo_Tipo",
                schema: "dbo",
                columns: table => new
                {
                    grupo_tipo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grupo_tipo_nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo_Tipo", x => x.grupo_tipo_id);
                });

            migrationBuilder.CreateTable(
                name: "Modelo_Estructura",
                schema: "dbo",
                columns: table => new
                {
                    modelo_estructura_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_estructura_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    modelo_estructura_nombre_corto = table.Column<string>(maxLength: 10, nullable: false),
                    modelo_estructura_descripcion = table.Column<string>(maxLength: 140, nullable: true),
                    modelo_estructura_activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo_Estructura", x => x.modelo_estructura_id);
                });

            migrationBuilder.CreateTable(
                name: "Modo_Atencion",
                schema: "dbo",
                columns: table => new
                {
                    modo_atencion_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modo_atencion_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    modo_atencion_nombre_corto = table.Column<string>(maxLength: 10, nullable: false),
                    modo_atencion_descripcion = table.Column<string>(maxLength: 140, nullable: true),
                    modo_atencion_activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modo_Atencion", x => x.modo_atencion_id);
                });

            migrationBuilder.CreateTable(
                name: "Nivel",
                schema: "dbo",
                columns: table => new
                {
                    nivel_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nivel_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    nivel_nombre_corto = table.Column<string>(maxLength: 10, nullable: false),
                    nivel_descripcion = table.Column<string>(maxLength: 140, nullable: true),
                    nivel_activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nivel", x => x.nivel_id);
                });

            migrationBuilder.CreateTable(
                name: "Rol",
                schema: "dbo",
                columns: table => new
                {
                    rol_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rol_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    rol_nombre_corto = table.Column<string>(maxLength: 10, nullable: false),
                    rol_activo = table.Column<bool>(nullable: false),
                    rol_etiquetas = table.Column<string>(maxLength: 100, nullable: true),
                    rol_exportar_truck = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rol", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "Tipo",
                schema: "dbo",
                columns: table => new
                {
                    tipo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    grupo_tipo_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipo", x => x.tipo_id);
                    table.ForeignKey(
                        name: "FK_Tipo_Grupo_Tipo_grupo_tipo_id",
                        column: x => x.grupo_tipo_id,
                        principalSchema: "dbo",
                        principalTable: "Grupo_Tipo",
                        principalColumn: "grupo_tipo_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modelo_Estructura_Definicion",
                schema: "dbo",
                columns: table => new
                {
                    modelo_estructura_definicion_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_estructura_id = table.Column<int>(nullable: false),
                    nivel_id = table.Column<int>(nullable: false),
                    padre_nivel_id = table.Column<int>(nullable: true),
                    modelo_estructura_tiene_modo_atencion = table.Column<bool>(nullable: false),
                    modelo_estructura_tiene_canal_venta = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelo_Estructura_Definicion", x => x.modelo_estructura_definicion_id);
                    table.ForeignKey(
                        name: "FK_Modelo_Estructura_Definicion_Nivel_nivel_id",
                        column: x => x.nivel_id,
                        principalSchema: "dbo",
                        principalTable: "Nivel",
                        principalColumn: "nivel_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modelo_Estructura_Definicion_Nivel_padre_nivel_id",
                        column: x => x.padre_nivel_id,
                        principalSchema: "dbo",
                        principalTable: "Nivel",
                        principalColumn: "nivel_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modelo_Estructura_Definicion_Modelo_Estructura_modelo_estructura_id",
                        column: x => x.modelo_estructura_id,
                        principalSchema: "dbo",
                        principalTable: "Modelo_Estructura",
                        principalColumn: "modelo_estructura_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nodo",
                schema: "dbo",
                columns: table => new
                {
                    nodo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nodo_nombre = table.Column<string>(maxLength: 50, nullable: false),
                    nodo_codigo = table.Column<int>(nullable: false),
                    nivel_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodo", x => x.nodo_id);
                    table.ForeignKey(
                        name: "FK_Nodo_Nivel_nivel_id",
                        column: x => x.nivel_id,
                        principalSchema: "dbo",
                        principalTable: "Nivel",
                        principalColumn: "nivel_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Modo_Atencion_Rol",
                schema: "dbo",
                columns: table => new
                {
                    modo_atencion_id = table.Column<int>(nullable: false),
                    rol_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modo_Atencion_Rol", x => new { x.modo_atencion_id, x.rol_id });
                    table.ForeignKey(
                        name: "FK_Modo_Atencion_Rol_Modo_Atencion_modo_atencion_id",
                        column: x => x.modo_atencion_id,
                        principalSchema: "dbo",
                        principalTable: "Modo_Atencion",
                        principalColumn: "modo_atencion_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Modo_Atencion_Rol_Rol_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "dbo",
                        principalTable: "Rol",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Estructura",
                schema: "dbo",
                columns: table => new
                {
                    estructura_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    modelo_estructura_id = table.Column<int>(nullable: false),
                    nodo_raiz_id = table.Column<int>(nullable: true),
                    estructura_vigencia_desde = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estructura", x => x.estructura_id);
                    table.ForeignKey(
                        name: "FK_Estructura_Nodo_nodo_raiz_id",
                        column: x => x.nodo_raiz_id,
                        principalSchema: "dbo",
                        principalTable: "Nodo",
                        principalColumn: "nodo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estructura_Modelo_Estructura_modelo_estructura_id",
                        column: x => x.modelo_estructura_id,
                        principalSchema: "dbo",
                        principalTable: "Modelo_Estructura",
                        principalColumn: "modelo_estructura_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nodo_Definicion",
                schema: "dbo",
                columns: table => new
                {
                    nodo_definicion_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nodo_id = table.Column<int>(nullable: false),
                    nodo_definicion_vigencia_desde = table.Column<DateTime>(type: "date", nullable: false),
                    nodo_definicion_vigencia_hasta = table.Column<DateTime>(type: "date", nullable: false),
                    modo_atencion_id = table.Column<int>(nullable: true),
                    rol_id = table.Column<int>(nullable: true),
                    canal_venta_id = table.Column<int>(nullable: true),
                    empleado_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodo_Definicion", x => x.nodo_definicion_id);
                    table.ForeignKey(
                        name: "FK_Nodo_Definicion_Modo_Atencion_modo_atencion_id",
                        column: x => x.modo_atencion_id,
                        principalSchema: "dbo",
                        principalTable: "Modo_Atencion",
                        principalColumn: "modo_atencion_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nodo_Definicion_Nodo_nodo_id",
                        column: x => x.nodo_id,
                        principalSchema: "dbo",
                        principalTable: "Nodo",
                        principalColumn: "nodo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nodo_Definicion_Rol_rol_id",
                        column: x => x.rol_id,
                        principalSchema: "dbo",
                        principalTable: "Rol",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Nodo_Definicion_Canal_Venta_canal_venta_id",
                        column: x => x.canal_venta_id,
                        principalSchema: "dbo",
                        principalTable: "Canal_Venta",
                        principalColumn: "canal_venta_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Arista",
                schema: "dbo",
                columns: table => new
                {
                    arista_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estructura_id_desde = table.Column<int>(nullable: false),
                    nodo_id_desde = table.Column<int>(nullable: false),
                    estructura_id_hasta = table.Column<int>(nullable: false),
                    nodo_id_hasta = table.Column<int>(nullable: false),
                    tipo_relacion_id = table.Column<int>(nullable: false),
                    arista_vigencia_desde = table.Column<DateTime>(type: "Date", nullable: false),
                    arista_vigencia_hasta = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arista", x => x.arista_id);
                    table.ForeignKey(
                        name: "FK_Arista_Nodo_nodo_id_desde",
                        column: x => x.nodo_id_desde,
                        principalSchema: "dbo",
                        principalTable: "Nodo",
                        principalColumn: "nodo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arista_Nodo_nodo_id_hasta",
                        column: x => x.nodo_id_hasta,
                        principalSchema: "dbo",
                        principalTable: "Nodo",
                        principalColumn: "nodo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arista_Estructura_estructura_id_desde",
                        column: x => x.estructura_id_desde,
                        principalSchema: "dbo",
                        principalTable: "Estructura",
                        principalColumn: "estructura_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arista_Estructura_estructura_id_hasta",
                        column: x => x.estructura_id_hasta,
                        principalSchema: "dbo",
                        principalTable: "Estructura",
                        principalColumn: "estructura_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Arista_Tipo_tipo_relacion_id",
                        column: x => x.tipo_relacion_id,
                        principalSchema: "dbo",
                        principalTable: "Tipo",
                        principalColumn: "tipo_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arista_nodo_id_desde",
                schema: "dbo",
                table: "Arista",
                column: "nodo_id_desde");

            migrationBuilder.CreateIndex(
                name: "IX_Arista_nodo_id_hasta",
                schema: "dbo",
                table: "Arista",
                column: "nodo_id_hasta");

            migrationBuilder.CreateIndex(
                name: "IX_Arista_estructura_id_desde",
                schema: "dbo",
                table: "Arista",
                column: "estructura_id_desde");

            migrationBuilder.CreateIndex(
                name: "IX_Arista_estructura_id_hasta",
                schema: "dbo",
                table: "Arista",
                column: "estructura_id_hasta");

            migrationBuilder.CreateIndex(
                name: "IX_Arista_tipo_relacion_id",
                schema: "dbo",
                table: "Arista",
                column: "tipo_relacion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estructura_nodo_raiz_id",
                schema: "dbo",
                table: "Estructura",
                column: "nodo_raiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_Estructura_modelo_estructura_id",
                schema: "dbo",
                table: "Estructura",
                column: "modelo_estructura_id");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_Estructura_Definicion_nivel_id",
                schema: "dbo",
                table: "Modelo_Estructura_Definicion",
                column: "nivel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_Estructura_Definicion_padre_nivel_id",
                schema: "dbo",
                table: "Modelo_Estructura_Definicion",
                column: "padre_nivel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Modelo_Estructura_Definicion_modelo_estructura_id",
                schema: "dbo",
                table: "Modelo_Estructura_Definicion",
                column: "modelo_estructura_id");

            migrationBuilder.CreateIndex(
                name: "IX_Modo_Atencion_Rol_rol_id",
                schema: "dbo",
                table: "Modo_Atencion_Rol",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_nivel_id",
                schema: "dbo",
                table: "Nodo",
                column: "nivel_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_Definicion_modo_atencion_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                column: "modo_atencion_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_Definicion_nodo_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                column: "nodo_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_Definicion_rol_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                column: "rol_id");

            migrationBuilder.CreateIndex(
                name: "IX_Nodo_Definicion_canal_venta_id",
                schema: "dbo",
                table: "Nodo_Definicion",
                column: "canal_venta_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tipo_grupo_tipo_id",
                schema: "dbo",
                table: "Tipo",
                column: "grupo_tipo_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arista",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Modelo_Estructura_Definicion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Modo_Atencion_Rol",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Nodo_Definicion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Estructura",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Tipo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Modo_Atencion",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Rol",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Canal_Venta",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Nodo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Modelo_Estructura",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Grupo_Tipo",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Nivel",
                schema: "dbo");
        }
    }
}
