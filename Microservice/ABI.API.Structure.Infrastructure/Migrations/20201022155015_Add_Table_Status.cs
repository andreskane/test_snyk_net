using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Add_Table_Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "log_Impacto_Truck_Estado_Id",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado",
                newName: "log_Impacto_truck_Estado_Id");

            migrationBuilder.CreateTable(
                name: "Impacto_Estado",
                schema: "ACL",
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

            migrationBuilder.CreateIndex(
                name: "IX_Log_Impacto_Truck_Estado_log_impacto_truck_estado",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado",
                column: "log_impacto_truck_estado");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Impacto_Truck_Estado_Impacto_Estado_log_impacto_truck_estado",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado",
                column: "log_impacto_truck_estado",
                principalSchema: "ACL",
                principalTable: "Impacto_Estado",
                principalColumn: "impacto_estado_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Impacto_Truck_Estado_Impacto_Estado_log_impacto_truck_estado",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado");

            migrationBuilder.DropTable(
                name: "Impacto_Estado",
                schema: "ACL");

            migrationBuilder.DropIndex(
                name: "IX_Log_Impacto_Truck_Estado_log_impacto_truck_estado",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado");

            migrationBuilder.RenameColumn(
                name: "log_Impacto_truck_Estado_Id",
                schema: "ACL",
                table: "Log_Impacto_Truck_Estado",
                newName: "log_Impacto_Truck_Estado_Id");
        }
    }
}
