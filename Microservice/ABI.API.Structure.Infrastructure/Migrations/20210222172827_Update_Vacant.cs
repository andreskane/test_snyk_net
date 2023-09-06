using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Update_Vacant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //No vacante
            migrationBuilder.Sql("update Nodo_Definicion Set nodo_definicion_persona_vacante = 1 where rol_id is null");
            migrationBuilder.Sql("update Nodo_Definicion Set nodo_definicion_persona_vacante = 1 where rol_id is not null and empleado_id is null");
            
            //Vacante
            migrationBuilder.Sql("update Nodo_Definicion Set nodo_definicion_persona_vacante = 0 where rol_id is not null and empleado_id is not null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update Nodo_Definicion Set nodo_definicion_persona_vacante = null");
        }
    }
}
