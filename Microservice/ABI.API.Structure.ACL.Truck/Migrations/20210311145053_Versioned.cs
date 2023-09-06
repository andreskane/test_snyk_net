using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]

    public partial class Versioned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Versionado_Estado",
                schema: "acl",
                columns: table => new
                {
                    versionado_estado_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    versionado_estado_nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionado_Estado", x => x.versionado_estado_id);
                });

            migrationBuilder.CreateTable(
                name: "Versionado_Estado_Log",
                schema: "acl",
                columns: table => new
                {
                    versionado_estado_log_Id = table.Column<int>(nullable: false),
                    versionado_estado_log_nombre = table.Column<string>(maxLength: 100, nullable: false),
                    versionado_estado_log_codigo = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionado_Estado_Log", x => x.versionado_estado_log_Id);
                });

            migrationBuilder.CreateTable(
                name: "Versionado",
                schema: "acl",
                columns: table => new
                {
                    versionado_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estructura_id = table.Column<int>(nullable: false),
                    versionado_fecha_hora = table.Column<DateTime>(nullable: false),
                    versionado_version = table.Column<int>(maxLength: 10, nullable: false),
                    versionado_vigencia = table.Column<DateTime>(nullable: false),
                    versionado_estado_id = table.Column<int>(nullable: false),
                    versionado_usuario = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionado", x => x.versionado_id);
                    table.ForeignKey(
                        name: "FK_Versionado_Versionado_Estado_versionado_estado_id",
                        column: x => x.versionado_estado_id,
                        principalSchema: "acl",
                        principalTable: "Versionado_Estado",
                        principalColumn: "versionado_estado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Versionado_Aristas",
                schema: "acl",
                columns: table => new
                {
                    versionado_arista_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    versionado_id = table.Column<int>(nullable: false),
                    arista_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionado_Aristas", x => x.versionado_arista_id);
                    table.ForeignKey(
                        name: "FK_Versionado_Aristas_Versionado_versionado_id",
                        column: x => x.versionado_id,
                        principalSchema: "acl",
                        principalTable: "Versionado",
                        principalColumn: "versionado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Versionado_Log",
                schema: "acl",
                columns: table => new
                {
                    versionado_log_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    versionado_id = table.Column<int>(nullable: false),
                    versionado_log_fecha_hora = table.Column<DateTime>(nullable: false),
                    versionado_estado_log_Id = table.Column<int>(nullable: false),
                    versionado_log_detalle = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionado_Log", x => x.versionado_log_Id);
                    table.ForeignKey(
                        name: "FK_Versionado_Log_Versionado_Estado_Log_versionado_estado_log_Id",
                        column: x => x.versionado_estado_log_Id,
                        principalSchema: "acl",
                        principalTable: "Versionado_Estado_Log",
                        principalColumn: "versionado_estado_log_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Versionado_Log_Versionado_versionado_id",
                        column: x => x.versionado_id,
                        principalSchema: "acl",
                        principalTable: "Versionado",
                        principalColumn: "versionado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Versionado_Nodo",
                schema: "acl",
                columns: table => new
                {
                    versionado_nodo_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    versionado_id = table.Column<int>(nullable: false),
                    nodo_id = table.Column<int>(nullable: false),
                    nodo_definicion_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versionado_Nodo", x => x.versionado_nodo_id);
                    table.ForeignKey(
                        name: "FK_Versionado_Nodo_Versionado_versionado_id",
                        column: x => x.versionado_id,
                        principalSchema: "acl",
                        principalTable: "Versionado",
                        principalColumn: "versionado_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Versionado_versionado_estado_id",
                schema: "acl",
                table: "Versionado",
                column: "versionado_estado_id");

            migrationBuilder.CreateIndex(
                name: "IX_Versionado_Aristas_versionado_id",
                schema: "acl",
                table: "Versionado_Aristas",
                column: "versionado_id");

            migrationBuilder.CreateIndex(
                name: "IX_Versionado_Log_versionado_estado_log_Id",
                schema: "acl",
                table: "Versionado_Log",
                column: "versionado_estado_log_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Versionado_Log_versionado_id",
                schema: "acl",
                table: "Versionado_Log",
                column: "versionado_id");

            migrationBuilder.CreateIndex(
                name: "IX_Versionado_Nodo_versionado_id",
                schema: "acl",
                table: "Versionado_Nodo",
                column: "versionado_id");


            //Insert Versionado_Estado_Log

            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 1, "Creación Nueva Versión", "CNV" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 2, "Envía a Bandejas", "EB" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 3, "Aplica Cambios Programados", "ACP" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 4, "Aplica Cambios en el momento", "ACM" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 5, "Desestima la Versión", "DV" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 6, "Cambio de fecha de Impacto", "CFI" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 7, "Datos enviados a las Bandejas", "DEB" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 8, "Pasa a edición la Versión", "PEV" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 9, "Rechazo Versión", "RV" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 10, "Transformación Portal a Truck", "TPT" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 50, "Estado Pendiente de envió", "EPE" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 51, "Estado Procesando", "EPR" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 52, "Estado Aceptado", "EAC" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 53, "Estado Operativo", "EOP" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 54, "Estado Rechazado", "EREC" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 100, "Error Api envió al Crear nueva Versión", "ECNV" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 101, "Error Api Truck al Enviar a las Bandejas ", "EEB" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 102, "Error Api Truck envió Aplica Cambios Programados", "EACP" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 103, "Error Api Truck envió Aplica Cambios en el momento", "EACM" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 104, "Error Api Truck envió Desestima la Versión", "EDV" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 105, "Error Api Truck envió Cambio de fecha de Impacto", "ECFI" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 106, "Error en Bandeja, hay errores informados por truck", "EBT" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado_Log", new string[] { "versionado_estado_log_Id", "versionado_estado_log_nombre", "Versionado_estado_log_codigo" }, new object[] { 107, "Error en transformación de Portal a Truck", "ETPT" }, "ACL");

            //Insert Versionado_Estado
            migrationBuilder.InsertData("Versionado_Estado", new string[] { "versionado_estado_nombre" }, new object[] { "PendienteDeEnvio" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado", new string[] { "versionado_estado_nombre" }, new object[] { "Procesando" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado", new string[] { "versionado_estado_nombre" }, new object[] { "Aceptado" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado", new string[] { "versionado_estado_nombre" }, new object[] { "Operativo" }, "ACL");
            migrationBuilder.InsertData("Versionado_Estado", new string[] { "versionado_estado_nombre" }, new object[] { "Rechazado" }, "ACL");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Versionado_Aristas",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Versionado_Log",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Versionado_Nodo",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Versionado_Estado_Log",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Versionado",
                schema: "acl");

            migrationBuilder.DropTable(
                name: "Versionado_Estado",
                schema: "acl");
        }
    }
}
