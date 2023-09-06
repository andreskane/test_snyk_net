using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace ABI.API.Structure.Infrastructure.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class Update_Responsable_ModoAtencionRol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=1 AND rol_id is null) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 1 AND rol_id is null " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(1, null, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=2 AND rol_id is null) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 2 AND rol_id is null " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(2, null, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=3 AND rol_id =1) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 3 AND rol_id = 1 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(3, 1, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=3 AND rol_id=3) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 3 AND rol_id =3 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(3, 3, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=3 AND rol_id =4) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 3 AND rol_id =4 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(3, 4, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=4 AND rol_id is null) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 4 AND rol_id is null " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(4, null, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=5 AND rol_id =9) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 5 AND rol_id =9 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(5, 9, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=6 AND rol_id =2) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 0 WHERE modo_atencion_id = 6 AND rol_id =2 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(6, 2, 0) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=6 AND rol_id =12) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 6 AND rol_id =12 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(6, 12, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=7 AND rol_id is null) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 7 AND rol_id is null " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(7, null, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=8 AND rol_id =25) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 8 AND rol_id = 25 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(8, 25, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=8 AND rol_id =26) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 8 AND rol_id = 26 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(8, 26, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=9 AND rol_id =14) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 9 AND rol_id = 14 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(9, 14, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=10 AND rol_id is null) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 10 AND rol_id is null " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(10, null, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=11 AND rol_id is null) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 11 AND rol_id is null " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(11, null, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=12 AND rol_id =20) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 12 AND rol_id = 20 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(12, 20, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=12 AND rol_id =21) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 12 AND rol_id = 21 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(12, 21, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=12 AND rol_id =22) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 12 AND rol_id = 22 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(12, 22, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=12 AND rol_id =23) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 1 WHERE modo_atencion_id = 12 AND rol_id = 23 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(12, 23, 1) END ");

            migrationBuilder.Sql("IF EXISTS(SELECT * FROM Modo_Atencion_Rol WHERE modo_atencion_id=13 AND rol_id =19) " +
                "BEGIN " +
                "UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 0 WHERE modo_atencion_id = 13 AND rol_id = 19 " +
                "END ELSE BEGIN " +
                "INSERT INTO Modo_Atencion_Rol(modo_atencion_id, rol_id, modo_atencion_rol_responsable) " +
                "VALUES(13, 19, 0) END ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Modo_Atencion_Rol SET modo_atencion_rol_responsable = 0 WHERE modo_atencion_id = 13 AND rol_id = 19");
        }
    }
}
