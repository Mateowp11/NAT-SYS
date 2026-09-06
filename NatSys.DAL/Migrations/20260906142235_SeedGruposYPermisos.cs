using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NatSys.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedGruposYPermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Grupos",
                columns: new[] { "IdGrupo", "Descripcion", "Estado", "Nombre" },
                values: new object[,]
                {
                    { 1, "Acceso total al sistema", "activo", "Administradores" },
                    { 2, "Gestion operativa del equipo", "activo", "Entrenadores" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "Accion", "Modulo", "Nombre" },
                values: new object[,]
                {
                    { 1, "Gestionar", "Atletas", "GESTIONAR_ATLETAS" },
                    { 2, "Gestionar", "Torneos", "GESTIONAR_TORNEOS" },
                    { 3, "Gestionar", "Marcas", "GESTIONAR_MARCAS" },
                    { 4, "Calcular", "Pasajes", "CALCULAR_PASAJES" },
                    { 5, "Ver", "Reportes", "VER_REPORTES" },
                    { 6, "Gestionar", "Usuarios", "GESTIONAR_USUARIOS" },
                    { 7, "Gestionar", "Grupos", "GESTIONAR_GRUPOS" }
                });

            migrationBuilder.InsertData(
                table: "GrupoPermiso",
                columns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 1, 5 },
                    { 1, 6 },
                    { 1, 7 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 3 },
                    { 2, 4 },
                    { 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "GrupoUsuario",
                columns: new[] { "GruposIdGrupo", "UsuariosIdUsuario" },
                values: new object[] { 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "GrupoPermiso",
                keyColumns: new[] { "GruposIdGrupo", "PermisosIdPermiso" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "GrupoUsuario",
                keyColumns: new[] { "GruposIdGrupo", "UsuariosIdUsuario" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Grupos",
                keyColumn: "IdGrupo",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Grupos",
                keyColumn: "IdGrupo",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 7);
        }
    }
}
