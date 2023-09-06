using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.ACL.Truck.Migrations
{
    [ExcludeFromCodeCoverage]

    public partial class ACLAddStateVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Insert Versionado_Estad

            migrationBuilder.Sql("INSERT INTO acl.Versionado_Estado(versionado_estado_nombre) VALUES('Cancelado');");
            migrationBuilder.Sql("INSERT INTO acl.Versionado_Estado(versionado_estado_nombre) VALUES('Unificado');");


            //Insert Versionado_Estado_Log

            migrationBuilder.Sql("INSERT INTO acl.Versionado_Estado_Log(versionado_estado_log_Id,versionado_estado_log_nombre,Versionado_estado_log_codigo) VALUES(55,'Estado Cancelado','ECANC');");
            migrationBuilder.Sql("INSERT INTO acl.Versionado_Estado_Log(versionado_estado_log_Id,versionado_estado_log_nombre,Versionado_estado_log_codigo) VALUES(56,'Estado Unificado','EUNIF');");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
