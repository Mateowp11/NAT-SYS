using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NatSys.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Iteracion2Entrenamiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClaveProvisoria",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "GruposEntrenamiento",
                columns: table => new
                {
                    IdGrupoEntrenamiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdEntrenador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GruposEntrenamiento", x => x.IdGrupoEntrenamiento);
                    table.ForeignKey(
                        name: "FK_GruposEntrenamiento_Entrenadores_IdEntrenador",
                        column: x => x.IdEntrenador,
                        principalTable: "Entrenadores",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtletaGrupoEntrenamiento",
                columns: table => new
                {
                    AtletasIdPersona = table.Column<int>(type: "int", nullable: false),
                    GruposIdGrupoEntrenamiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtletaGrupoEntrenamiento", x => new { x.AtletasIdPersona, x.GruposIdGrupoEntrenamiento });
                    table.ForeignKey(
                        name: "FK_AtletaGrupoEntrenamiento_Atletas_AtletasIdPersona",
                        column: x => x.AtletasIdPersona,
                        principalTable: "Atletas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtletaGrupoEntrenamiento_GruposEntrenamiento_GruposIdGrupoEntrenamiento",
                        column: x => x.GruposIdGrupoEntrenamiento,
                        principalTable: "GruposEntrenamiento",
                        principalColumn: "IdGrupoEntrenamiento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrabajosDiarios",
                columns: table => new
                {
                    IdTrabajo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArchivoNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Archivo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IdGrupoEntrenamiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajosDiarios", x => x.IdTrabajo);
                    table.ForeignKey(
                        name: "FK_TrabajosDiarios_GruposEntrenamiento_IdGrupoEntrenamiento",
                        column: x => x.IdGrupoEntrenamiento,
                        principalTable: "GruposEntrenamiento",
                        principalColumn: "IdGrupoEntrenamiento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bloques",
                columns: table => new
                {
                    IdBloque = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distancia = table.Column<int>(type: "int", nullable: false),
                    Repeticiones = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    IdTrabajo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bloques", x => x.IdBloque);
                    table.ForeignKey(
                        name: "FK_Bloques_TrabajosDiarios_IdTrabajo",
                        column: x => x.IdTrabajo,
                        principalTable: "TrabajosDiarios",
                        principalColumn: "IdTrabajo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resultados",
                columns: table => new
                {
                    IdResultado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCarga = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdBloque = table.Column<int>(type: "int", nullable: false),
                    IdAtleta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resultados", x => x.IdResultado);
                    table.ForeignKey(
                        name: "FK_Resultados_Atletas_IdAtleta",
                        column: x => x.IdAtleta,
                        principalTable: "Atletas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resultados_Bloques_IdBloque",
                        column: x => x.IdBloque,
                        principalTable: "Bloques",
                        principalColumn: "IdBloque",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiemposRepeticion",
                columns: table => new
                {
                    IdTiempo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroRepeticion = table.Column<int>(type: "int", nullable: false),
                    Tiempo = table.Column<decimal>(type: "decimal(7,2)", precision: 7, scale: 2, nullable: false),
                    IdResultado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiemposRepeticion", x => x.IdTiempo);
                    table.ForeignKey(
                        name: "FK_TiemposRepeticion_Resultados_IdResultado",
                        column: x => x.IdResultado,
                        principalTable: "Resultados",
                        principalColumn: "IdResultado",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Grupos",
                columns: new[] { "IdGrupo", "Descripcion", "Estado", "Nombre" },
                values: new object[] { 3, "Carga de resultados desde la web", "activo", "Atletas" });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "Accion", "Modulo", "Nombre" },
                values: new object[,]
                {
                    { 8, "Gestionar", "Entrenamiento", "GESTIONAR_GRUPOS_ENTRENAMIENTO" },
                    { 9, "Cargar", "Entrenamiento", "CARGAR_TRABAJOS" },
                    { 10, "Cargar resultados", "Entrenamiento", "CARGAR_RESULTADOS" }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "IdUsuario",
                keyValue: 1,
                column: "ClaveProvisoria",
                value: false);

            migrationBuilder.InsertData(
                table: "GrupoPermiso",
                columns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                values: new object[,]
                {
                    { 1, 8 },
                    { 1, 9 },
                    { 1, 10 },
                    { 2, 8 },
                    { 2, 9 },
                    { 3, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtletaGrupoEntrenamiento_GruposIdGrupoEntrenamiento",
                table: "AtletaGrupoEntrenamiento",
                column: "GruposIdGrupoEntrenamiento");

            migrationBuilder.CreateIndex(
                name: "IX_Bloques_IdTrabajo",
                table: "Bloques",
                column: "IdTrabajo");

            migrationBuilder.CreateIndex(
                name: "IX_GruposEntrenamiento_IdEntrenador",
                table: "GruposEntrenamiento",
                column: "IdEntrenador");

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_IdAtleta",
                table: "Resultados",
                column: "IdAtleta");

            migrationBuilder.CreateIndex(
                name: "IX_Resultados_IdBloque_IdAtleta",
                table: "Resultados",
                columns: new[] { "IdBloque", "IdAtleta" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiemposRepeticion_IdResultado_NroRepeticion",
                table: "TiemposRepeticion",
                columns: new[] { "IdResultado", "NroRepeticion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrabajosDiarios_IdGrupoEntrenamiento",
                table: "TrabajosDiarios",
                column: "IdGrupoEntrenamiento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtletaGrupoEntrenamiento");

            migrationBuilder.DropTable(
                name: "TiemposRepeticion");

            migrationBuilder.DropTable(
                name: "Resultados");

            migrationBuilder.DropTable(
                name: "Bloques");

            migrationBuilder.DropTable(
                name: "TrabajosDiarios");

            migrationBuilder.DropTable(
                name: "GruposEntrenamiento");

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 3, 10 });

            migrationBuilder.DeleteData(
                table: "Grupos",
                keyColumn: "IdGrupo",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "ClaveProvisoria",
                table: "Usuarios");
        }
    }
}
