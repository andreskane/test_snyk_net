using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class add_Table_Bandeja : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log_Impacto_Truck_Bandejas",
                schema: "acl",
                columns: table => new
                {
                    log_impacto_truck_bandeja_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_impacto_truck_id = table.Column<int>(nullable: false),
                    log_impacto_truck_bandejas = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Bandejas_log_impacto_truck_id",
                schema: "acl",
                table: "Log_Impacto_Truck_Bandejas",
                column: "log_impacto_truck_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log_Impacto_Truck_Bandejas",
                schema: "acl");
        }
    }
}
