using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NatSys.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InicialNatSys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estilos",
                columns: table => new
                {
                    IdEstilo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoefFatigaInicial = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoefFatigaMedio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoefFatigaFinal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estilos", x => x.IdEstilo);
                });

            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    IdGrupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.IdGrupo);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    IdPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Accion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.IdPermiso);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.IdPersona);
                });

            migrationBuilder.CreateTable(
                name: "Torneos",
                columns: table => new
                {
                    IdTorneo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreTorneo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sede = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LargoPileta = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneos", x => x.IdTorneo);
                });

            migrationBuilder.CreateTable(
                name: "Pruebas",
                columns: table => new
                {
                    IdPrueba = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distancia = table.Column<int>(type: "int", nullable: false),
                    IdEstilo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pruebas", x => x.IdPrueba);
                    table.ForeignKey(
                        name: "FK_Pruebas_Estilos_IdEstilo",
                        column: x => x.IdEstilo,
                        principalTable: "Estilos",
                        principalColumn: "IdEstilo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoPermiso",
                columns: table => new
                {
                    GruposIdGrupo = table.Column<int>(type: "int", nullable: false),
                    PermisosIdPermiso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoPermiso", x => new { x.GruposIdGrupo, x.PermisosIdPermiso });
                    table.ForeignKey(
                        name: "FK_GrupoPermiso_Grupos_GruposIdGrupo",
                        column: x => x.GruposIdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoPermiso_Permisos_PermisosIdPermiso",
                        column: x => x.PermisosIdPermiso,
                        principalTable: "Permisos",
                        principalColumn: "IdPermiso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Atletas",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Categoria = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atletas", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Atletas_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entrenadores",
                columns: table => new
                {
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especialidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entrenadores", x => x.IdPersona);
                    table.ForeignKey(
                        name: "FK_Entrenadores_Personas_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntentosFallidos = table.Column<int>(type: "int", nullable: false),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PreguntaSeguridad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RespuestaSeguridadHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false),
                    PersonaIdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_PersonaIdPersona",
                        column: x => x.PersonaIdPersona,
                        principalTable: "Personas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AtletaPrueba",
                columns: table => new
                {
                    AtletaIdPersona = table.Column<int>(type: "int", nullable: false),
                    PruebasIdPrueba = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtletaPrueba", x => new { x.AtletaIdPersona, x.PruebasIdPrueba });
                    table.ForeignKey(
                        name: "FK_AtletaPrueba_Atletas_AtletaIdPersona",
                        column: x => x.AtletaIdPersona,
                        principalTable: "Atletas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AtletaPrueba_Pruebas_PruebasIdPrueba",
                        column: x => x.PruebasIdPrueba,
                        principalTable: "Pruebas",
                        principalColumn: "IdPrueba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Marcas",
                columns: table => new
                {
                    IdMarca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tiempo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EsRecordPersonal = table.Column<bool>(type: "bit", nullable: false),
                    LargoPileta = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdAtleta = table.Column<int>(type: "int", nullable: false),
                    IdPrueba = table.Column<int>(type: "int", nullable: false),
                    IdTorneo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.IdMarca);
                    table.ForeignKey(
                        name: "FK_Marcas_Atletas_IdAtleta",
                        column: x => x.IdAtleta,
                        principalTable: "Atletas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marcas_Pruebas_IdPrueba",
                        column: x => x.IdPrueba,
                        principalTable: "Pruebas",
                        principalColumn: "IdPrueba",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Marcas_Torneos_IdTorneo",
                        column: x => x.IdTorneo,
                        principalTable: "Torneos",
                        principalColumn: "IdTorneo");
                });

            migrationBuilder.CreateTable(
                name: "PlanesPasaje",
                columns: table => new
                {
                    IdPlanPasaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaObjetivo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LargoPileta = table.Column<int>(type: "int", nullable: false),
                    IdAtleta = table.Column<int>(type: "int", nullable: false),
                    IdPrueba = table.Column<int>(type: "int", nullable: false),
                    PruebaIdPrueba = table.Column<int>(type: "int", nullable: false),
                    IdEntrenador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanesPasaje", x => x.IdPlanPasaje);
                    table.ForeignKey(
                        name: "FK_PlanesPasaje_Atletas_IdAtleta",
                        column: x => x.IdAtleta,
                        principalTable: "Atletas",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanesPasaje_Entrenadores_IdEntrenador",
                        column: x => x.IdEntrenador,
                        principalTable: "Entrenadores",
                        principalColumn: "IdPersona",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanesPasaje_Pruebas_PruebaIdPrueba",
                        column: x => x.PruebaIdPrueba,
                        principalTable: "Pruebas",
                        principalColumn: "IdPrueba",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUsuario",
                columns: table => new
                {
                    GruposIdGrupo = table.Column<int>(type: "int", nullable: false),
                    UsuariosIdUsuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUsuario", x => new { x.GruposIdGrupo, x.UsuariosIdUsuario });
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Grupos_GruposIdGrupo",
                        column: x => x.GruposIdGrupo,
                        principalTable: "Grupos",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Usuarios_UsuariosIdUsuario",
                        column: x => x.UsuariosIdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    IdSesion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Fin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.IdSesion);
                    table.ForeignKey(
                        name: "FK_Sesiones_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pasajes",
                columns: table => new
                {
                    IdPasaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NroTramo = table.Column<int>(type: "int", nullable: false),
                    Tiempo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Distancia = table.Column<int>(type: "int", nullable: false),
                    IdPlanPasaje = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasajes", x => x.IdPasaje);
                    table.ForeignKey(
                        name: "FK_Pasajes_PlanesPasaje_IdPlanPasaje",
                        column: x => x.IdPlanPasaje,
                        principalTable: "PlanesPasaje",
                        principalColumn: "IdPlanPasaje",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtletaPrueba_PruebasIdPrueba",
                table: "AtletaPrueba",
                column: "PruebasIdPrueba");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoPermiso_PermisosIdPermiso",
                table: "GrupoPermiso",
                column: "PermisosIdPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_UsuariosIdUsuario",
                table: "GrupoUsuario",
                column: "UsuariosIdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_IdAtleta",
                table: "Marcas",
                column: "IdAtleta");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_IdPrueba",
                table: "Marcas",
                column: "IdPrueba");

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_IdTorneo",
                table: "Marcas",
                column: "IdTorneo");

            migrationBuilder.CreateIndex(
                name: "IX_Pasajes_IdPlanPasaje",
                table: "Pasajes",
                column: "IdPlanPasaje");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesPasaje_IdAtleta",
                table: "PlanesPasaje",
                column: "IdAtleta");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesPasaje_IdEntrenador",
                table: "PlanesPasaje",
                column: "IdEntrenador");

            migrationBuilder.CreateIndex(
                name: "IX_PlanesPasaje_PruebaIdPrueba",
                table: "PlanesPasaje",
                column: "PruebaIdPrueba");

            migrationBuilder.CreateIndex(
                name: "IX_Pruebas_IdEstilo",
                table: "Pruebas",
                column: "IdEstilo");

            migrationBuilder.CreateIndex(
                name: "IX_Sesiones_IdUsuario",
                table: "Sesiones",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaIdPersona",
                table: "Usuarios",
                column: "PersonaIdPersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtletaPrueba");

            migrationBuilder.DropTable(
                name: "GrupoPermiso");

            migrationBuilder.DropTable(
                name: "GrupoUsuario");

            migrationBuilder.DropTable(
                name: "Marcas");

            migrationBuilder.DropTable(
                name: "Pasajes");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Torneos");

            migrationBuilder.DropTable(
                name: "PlanesPasaje");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Atletas");

            migrationBuilder.DropTable(
                name: "Entrenadores");

            migrationBuilder.DropTable(
                name: "Pruebas");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Estilos");
        }
    }
}
