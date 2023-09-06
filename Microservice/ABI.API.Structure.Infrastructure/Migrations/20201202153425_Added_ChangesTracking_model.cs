using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Added_ChangesTracking_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "nodo_cancelado",
                schema: "dbo",
                table: "Nodo_Definicion",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "arista_cancelada",
                schema: "dbo",
                table: "Arista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ObjectType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estado_Truck",
                schema: "dbo",
                columns: table => new
                {
                    estado_truck_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_truck_nombre = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estado_Truck", x => x.estado_truck_id);
                });

            migrationBuilder.CreateTable(
                name: "Seguimiento_Cambio",
                schema: "dbo",
                columns: table => new
                {
                    seguimiento_cambio_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seguimiento_cambio_id_usuario = table.Column<int>(nullable: false),
                    seguimiento_cambio_nombre_usuario = table.Column<string>(nullable: false),
                    seguimiento_cambio_fecha = table.Column<DateTime>(nullable: false),
                    seguimiento_cambio_id_objeto = table.Column<int>(nullable: false),
                    seguimiento_cambio_id_tipo_objeto = table.Column<int>(nullable: false),
                    seguimiento_cambio_valor_anterior = table.Column<string>(nullable: true),
                    seguimiento_cambio_valor_nuevo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seguimiento_Cambio", x => x.seguimiento_cambio_id);
                    table.ForeignKey(
                        name: "FK_Seguimiento_Cambio_ObjectType_seguimiento_cambio_id_tipo_objeto",
                        column: x => x.seguimiento_cambio_id_tipo_objeto,
                        principalTable: "ObjectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seguimiento_Cambio_seguimiento_cambio_id_tipo_objeto",
                schema: "dbo",
                table: "Seguimiento_Cambio",
                column: "seguimiento_cambio_id_tipo_objeto");

            migrationBuilder.InsertData("Estado_Truck", new string[] { "estado_truck_id", "estado_truck_nombre" }, new object[] { 1, "No se envía a Truck" });
            migrationBuilder.InsertData("Estado_Truck", new string[] { "estado_truck_id", "estado_truck_nombre" }, new object[] { 2, "Pendiente de envío" });
            migrationBuilder.InsertData("Estado_Truck", new string[] { "estado_truck_id", "estado_truck_nombre" }, new object[] { 3, "Aceptado" });
            migrationBuilder.InsertData("Estado_Truck", new string[] { "estado_truck_id", "estado_truck_nombre" }, new object[] { 4, "Rechazado" });
            migrationBuilder.InsertData("Estado_Truck", new string[] { "estado_truck_id", "estado_truck_nombre" }, new object[] { 5, "Operativo" });
            migrationBuilder.InsertData("Estado_Truck", new string[] { "estado_truck_id", "estado_truck_nombre" }, new object[] { 6, "Error" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estado_Truck",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Seguimiento_Cambio",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ObjectType");

            migrationBuilder.DropColumn(
                name: "nodo_cancelado",
                schema: "dbo",
                table: "Nodo_Definicion");

            migrationBuilder.DropColumn(
                name: "arista_cancelada",
                schema: "dbo",
                table: "Arista");

            migrationBuilder.DeleteData("Estado_Truck", "estado_truck_id", 1);
            migrationBuilder.DeleteData("Estado_Truck", "estado_truck_id", 2);
            migrationBuilder.DeleteData("Estado_Truck", "estado_truck_id", 3);
            migrationBuilder.DeleteData("Estado_Truck", "estado_truck_id", 4);
            migrationBuilder.DeleteData("Estado_Truck", "estado_truck_id", 5);
            migrationBuilder.DeleteData("Estado_Truck", "estado_truck_id", 6);
        }
    }
}
