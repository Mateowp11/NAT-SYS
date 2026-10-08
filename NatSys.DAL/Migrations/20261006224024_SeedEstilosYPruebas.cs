using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NatSys.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedEstilosYPruebas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Estilos",
                columns: new[] { "IdEstilo", "CoefFatigaFinal", "CoefFatigaInicial", "CoefFatigaMedio", "Nombre" },
                values: new object[,]
                {
                    { 1, 1.00m, 0.97m, 1.05m, "Crol" },
                    { 2, 1.01m, 0.97m, 1.07m, "Espalda" },
                    { 3, 1.02m, 0.96m, 1.09m, "Pecho" },
                    { 4, 1.04m, 0.95m, 1.10m, "Mariposa" },
                    { 5, 1.00m, 1.00m, 1.00m, "Combinado" }
                });

            migrationBuilder.InsertData(
                table: "Pruebas",
                columns: new[] { "IdPrueba", "Distancia", "IdEstilo", "Nombre" },
                values: new object[,]
                {
                    { 1, 50, 1, "50 Crol" },
                    { 2, 100, 1, "100 Crol" },
                    { 3, 200, 1, "200 Crol" },
                    { 4, 400, 1, "400 Crol" },
                    { 5, 800, 1, "800 Crol" },
                    { 6, 1500, 1, "1500 Crol" },
                    { 7, 50, 2, "50 Espalda" },
                    { 8, 100, 2, "100 Espalda" },
                    { 9, 200, 2, "200 Espalda" },
                    { 10, 50, 3, "50 Pecho" },
                    { 11, 100, 3, "100 Pecho" },
                    { 12, 200, 3, "200 Pecho" },
                    { 13, 50, 4, "50 Mariposa" },
                    { 14, 100, 4, "100 Mariposa" },
                    { 15, 200, 4, "200 Mariposa" },
                    { 16, 100, 5, "100 Combinado" },
                    { 17, 200, 5, "200 Combinado" },
                    { 18, 400, 5, "400 Combinado" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Pruebas",
                keyColumn: "IdPrueba",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Estilos",
                keyColumn: "IdEstilo",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estilos",
                keyColumn: "IdEstilo",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Estilos",
                keyColumn: "IdEstilo",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Estilos",
                keyColumn: "IdEstilo",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Estilos",
                keyColumn: "IdEstilo",
                keyValue: 5);
        }
    }
}
