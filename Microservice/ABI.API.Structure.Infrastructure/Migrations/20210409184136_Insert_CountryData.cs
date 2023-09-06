using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Insert_CountryData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ///INSERTAR LOS PAISES DEFINIDOS EN US #430119
            migrationBuilder.Sql("INSERT INTO pais (pais_nombre, pais_codigo_iso_3166_1_alfa2) VALUES ('ARGENTINA','AR')");
            migrationBuilder.Sql("INSERT INTO pais (pais_nombre, pais_codigo_iso_3166_1_alfa2) VALUES ('BOLIVIA','BO')");
            migrationBuilder.Sql("INSERT INTO pais (pais_nombre, pais_codigo_iso_3166_1_alfa2) VALUES ('CHILE','CL')");
            migrationBuilder.Sql("INSERT INTO pais (pais_nombre, pais_codigo_iso_3166_1_alfa2) VALUES ('PARAGUAY','PY')");
            migrationBuilder.Sql("INSERT INTO pais (pais_nombre, pais_codigo_iso_3166_1_alfa2) VALUES ('URUGUAY','UY')");


            //CARGAR LOS DATOS DE ESTRUCTURAS INICIALES (VTA = Venta y Pais = Argentina)
            migrationBuilder.Sql("UPDATE Modelo_Estructura SET pais_id = (SELECT pais_id FROM pais WHERE pais_codigo_iso_3166_1_alfa2 = 'AR'), modelo_estructura_codigo = 'VTA'");

            //CARGAR LA COMPOSICION EN LA ESTRUCTURA ACTUAL
            migrationBuilder.Sql("UPDATE Estructura SET estructura_codigo = 'AR_VTA'");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE pais");
            migrationBuilder.Sql("UPDATE Modelo_Estructura SET pais_id = null, modelo_estructura_codigo = null");
            migrationBuilder.Sql("UPDATE Estructura SET estructura_codigo = null");
        }
    }
}
