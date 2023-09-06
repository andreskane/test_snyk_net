using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class InsertDataChangeTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM grupo_tipo WHERE grupo_tipo_id=2 AND grupo_tipo_nombre = 'Tipo Objeto Cambio') " +
            "BEGIN " +
            "INSERT INTO grupo_tipo(grupo_tipo_nombre) " +
            "VALUES('Tipo Objeto Cambio') " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM grupo_tipo WHERE grupo_tipo_id=3 AND grupo_tipo_nombre = 'Tipo de Cambio') " +
            "BEGIN " +
            "INSERT INTO grupo_tipo(grupo_tipo_nombre) " +
            "VALUES('Tipo de Cambio') " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM tipo WHERE tipo_id=2 AND tipo_nombre = 'NODO' AND grupo_tipo_id = 2) " +
            "BEGIN " +
            "INSERT INTO tipo(tipo_nombre, grupo_tipo_id) " +
            "VALUES('NODO', 2) " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM tipo WHERE tipo_id=3 AND tipo_nombre = 'ARISTA' AND grupo_tipo_id = 2) " +
            "BEGIN " +
            "INSERT INTO tipo(tipo_nombre, grupo_tipo_id) " +
            "VALUES('ARISTA', 2) " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM tipo WHERE tipo_id=4 AND tipo_nombre = 'ESTRUCTURA' AND grupo_tipo_id = 3) " +
            "BEGIN " +
            "INSERT INTO tipo(tipo_nombre, grupo_tipo_id) " +
            "VALUES('ESTRUCTURA', 3) " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM tipo WHERE tipo_id=5 AND tipo_nombre = 'ROL' AND grupo_tipo_id = 3) " +
            "BEGIN " +
            "INSERT INTO tipo(tipo_nombre, grupo_tipo_id) " +
            "VALUES('ROL', 3) " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM tipo WHERE tipo_id=6 AND tipo_nombre = 'PERSONA' AND grupo_tipo_id = 3) " +
            "BEGIN " +
            "INSERT INTO tipo(tipo_nombre, grupo_tipo_id) " +
            "VALUES('PERSONA', 3) " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM grupo_estado WHERE grupo_estado_id=2 AND grupo_estado_nombre = 'Estados de Seguimiento de Cambios') " +
            "BEGIN " +
            "INSERT INTO grupo_estado(grupo_estado_nombre) " +
            "VALUES('Estados de Seguimiento de Cambios') " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM estado WHERE estado_id=5 AND estado_nombre = 'Confirmado' AND grupo_estado_id=2) " +
            "BEGIN " +
            "INSERT INTO estado(estado_nombre, grupo_estado_id) " +
            "VALUES('Confirmado',2) " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM estado WHERE estado_id=6 AND estado_nombre = 'Cancelado' AND grupo_estado_id=2) " +
            "BEGIN " +
            "INSERT INTO estado(estado_nombre, grupo_estado_id) " +
            "VALUES('Cancelado',2) " +
            "END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF EXISTS(SELECT * FROM grupo_tipo WHERE grupo_tipo_id=2 AND grupo_tipo_nombre = 'Tipo Objeto Cambio') " +
            "BEGIN " +
            "DELETE grupo_tipo WHERE grupo_tipo_nombre = 'Tipo Objeto Cambio' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM grupo_tipo WHERE grupo_tipo_id=3 AND grupo_tipo_nombre = 'Tipo de Cambio') " +
            "BEGIN " +
            "DELETE grupo_tipo WHERE grupo_tipo_nombre = 'Tipo de Cambio' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM tipo WHERE tipo_id=2 AND tipo_nombre = 'NODO' AND grupo_tipo_id = 2) " +
            "BEGIN " +
            "DELETE tipo WHERE tipo_nombre = 'NODO' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM tipo WHERE tipo_id=3 AND tipo_nombre = 'ARISTA' AND grupo_tipo_id = 2) " +
            "BEGIN " +
            "DELETE tipo WHERE tipo_nombre,grupo_tipo_id = 'ARISTA' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM tipo WHERE tipo_id=4 AND tipo_nombre = 'ESTRUCTURA' AND grupo_tipo_id = 3) " +
            "BEGIN " +
            "DELETE tipo WHERE tipo_nombre = 'ESTRUCTURA' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM tipo WHERE tipo_id=5 AND tipo_nombre = 'ROL' AND grupo_tipo_id = 3) " +
            "BEGIN " +
            "DELETE tipo WHERE tipo_nombre = 'ROL' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM tipo WHERE tipo_id=6 AND tipo_nombre = 'PERSONA' AND grupo_tipo_id = 3) " +
            "BEGIN " +
            "DELETE tipo WHERE tipo_nombre= 'PERSONA' " +
            "END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM grupo_estado WHERE grupo_estado_id=2 AND grupo_estado_nombre = 'Estados de Seguimiento de Cambios') " +
            "BEGIN " +
            "DELETE grupo_estado WHERE grupo_estado_nombre = 'Estados de Seguimiento de Cambios' " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM estado WHERE estado_id=5 AND estado_nombre = 'Confirmado' AND grupo_estado_id=2) " +
            "BEGIN " +
            "DELETE estado WHERE tipo_nombre = 'Confirmado' " +
            "END ");

            migrationBuilder.Sql("IF NOT EXISTS(SELECT * FROM estado WHERE estado_id=6 AND estado_nombre = 'Cancelado' AND grupo_estado_id=2) " +
            "BEGIN " +
            "DELETE estado WHERE tipo_nombre = 'Cancelado' " +
            "END ");
        }
    }
}
