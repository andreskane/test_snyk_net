using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class NewVersionedLogInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Insert Versionado_Estado_Log

            migrationBuilder.Sql("INSERT INTO acl.Versionado_Estado_Log(versionado_estado_log_Id,versionado_estado_log_nombre,Versionado_estado_log_codigo) VALUES(11,'Información General del Proceso','IGP');");
           

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
