using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatSys.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NombreUsuario",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "IdPersona", "Apellido", "Email", "FechaNacimiento", "Nombre", "Telefono" },
                values: new object[] { 1, "Sistema", "admin@natsys.local", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "" });

            migrationBuilder.InsertData(
                table: "Entrenadores",
                columns: new[] { "IdPersona", "Especialidad", "Estado" },
                values: new object[] { 1, "Administrador", "activo" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "Clave", "Estado", "IdPersona", "IntentosFallidos", "NombreUsuario", "PreguntaSeguridad", "RespuestaSeguridadHash", "UltimoAcceso" },
                values: new object[] { 1, "100000.4uvRpU0wa/QMbwBk3j9KVw==.3R4HkrQp2yVzE/gVZ1Ah24Pcd2IWeBNbwkOimRtx6aU=", "activo", 1, 0, "admin", "¿Cuál es tu comida favorita?", "100000.kESgVpXzptVDIk65q4lo0w==.9zFXADSlOndC/igsrxTKhiGfWubSxlAKtqA4z52luZ0=", null });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_NombreUsuario",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Entrenadores",
                keyColumn: "IdPersona",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "IdPersona",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "NombreUsuario",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
