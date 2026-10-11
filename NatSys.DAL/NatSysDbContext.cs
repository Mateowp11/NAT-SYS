
using System;
using Microsoft.EntityFrameworkCore;
using NatSys.Entidades;

namespace NatSys.DAL
{
    public class NatSysDbContext : DbContext
    {
        // Cadena de conexion recibida desde afuera (clave para el escenario
        // multi-cliente: cada instalacion apunta a un SQL Server distinto)
        private readonly string _connectionString;

        public NatSysDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<Atleta> Atletas { get; set; }
        public DbSet<Entrenador> Entrenadores { get; set; }
        public DbSet<Estilo> Estilos { get; set; }
        public DbSet<Prueba> Pruebas { get; set; }
        public DbSet<Torneo> Torneos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<PlanPasaje> PlanesPasaje { get; set; }
        public DbSet<Pasaje> Pasajes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPT (Table Per Type), no TPC: creamos una tabla Personas
            // compartida (con Nombre, Apellido, etc) + una tabla Atletas y
            // otra Entrenadores, cada una con SOLO sus campos propios y el
            // mismo Id que en Personas.
            //
            // Por que no TPC: con TPC no existe una tabla Personas fisica,
            // entonces Usuario no tendria a que tabla apuntar con su FK
            // "persona: Persona" (que en tu diagrama puede ser un Atleta O
            // un Entrenador indistintamente). TPT resuelve exactamente ese
            // problema: siempre hay una fila en Personas para identificar
            // a cualquiera de los dos.
            modelBuilder.Entity<Persona>().UseTptMappingStrategy();

            // IMPORTANTE: le decimos a EF explicitamente cual es la clave
            // primaria de cada entidad. Por que hace falta: EF busca solo
            // "Id" o "NombreClaseId" (por ejemplo "PersonaId"), y nuestras
            // clases usan el orden al reves ("IdPersona") - un nombre mas
            // claro para nosotros, pero que EF no reconoce sin que se lo
            // digamos aca.
            //
            // Atleta y Entrenador NO necesitan su propio HasKey: al ser TPT,
            // heredan la clave de Persona automaticamente.
            modelBuilder.Entity<Persona>().HasKey(p => p.IdPersona);
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Sesion>().HasKey(s => s.IdSesion);
            modelBuilder.Entity<Grupo>().HasKey(g => g.IdGrupo);
            modelBuilder.Entity<Permiso>().HasKey(p => p.IdPermiso);
            modelBuilder.Entity<Estilo>().HasKey(e => e.IdEstilo);
            modelBuilder.Entity<Prueba>().HasKey(p => p.IdPrueba);
            modelBuilder.Entity<Torneo>().HasKey(t => t.IdTorneo);
            modelBuilder.Entity<Marca>().HasKey(m => m.IdMarca);
            modelBuilder.Entity<PlanPasaje>().HasKey(p => p.IdPlanPasaje);
            modelBuilder.Entity<Pasaje>().HasKey(pa => pa.IdPasaje);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Persona)
                .WithMany()
                .HasForeignKey(u => u.IdPersona);

            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Grupos)
                .WithMany(g => g.Usuarios)
                .UsingEntity(j => j.HasData(
                    // Usuario admin (IdUsuario=1) queda en el grupo Administradores (IdGrupo=1)
                    new { GruposIdGrupo = 1, UsuariosIdUsuario = 1 }
                ));

            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Permisos)
                .WithMany(p => p.Grupos)
                .UsingEntity(j => j.HasData(
                    // Administradores (IdGrupo=1): todos los permisos
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 1 },
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 2 },
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 3 },
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 4 },
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 5 },
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 6 },
                    new { GruposIdGrupo = 1, PermisosIdPermiso = 7 },
                    // Entrenadores (IdGrupo=2): todo menos Usuarios y Grupos
                    new { GruposIdGrupo = 2, PermisosIdPermiso = 1 },
                    new { GruposIdGrupo = 2, PermisosIdPermiso = 2 },
                    new { GruposIdGrupo = 2, PermisosIdPermiso = 3 },
                    new { GruposIdGrupo = 2, PermisosIdPermiso = 4 },
                    new { GruposIdGrupo = 2, PermisosIdPermiso = 5 }
                ));

            modelBuilder.Entity<Sesion>()
                .HasOne(s => s.Usuario)
                .WithMany()
                .HasForeignKey(s => s.IdUsuario);

            modelBuilder.Entity<Atleta>()
                .HasMany(a => a.Pruebas)
                .WithMany();

            modelBuilder.Entity<Marca>()
                .HasOne(m => m.Atleta)
                .WithMany(a => a.Marcas)
                .HasForeignKey(m => m.IdAtleta)
                .OnDelete(DeleteBehavior.Restrict); // no borrar en cascada el historial de marcas

            modelBuilder.Entity<Marca>()
                .HasOne(m => m.Prueba)
                .WithMany()
                .HasForeignKey(m => m.IdPrueba)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Marca>()
                .HasOne(m => m.TorneoRegistro)
                .WithMany(t => t.Marcas)
                .HasForeignKey(m => m.IdTorneo)
                .IsRequired(false); // una marca "test" o "SV" no tiene torneo

            modelBuilder.Entity<Prueba>()
                .HasOne(p => p.Estilo)
                .WithMany()
                .HasForeignKey(p => p.IdEstilo);

            // Restrict en las 3 relaciones de PlanPasaje: es lo que resuelve
            // el error de SQL Server "may cause cycles or multiple cascade
            // paths". Como Atleta y Entrenador comparten el mismo ancestro
            // (Persona, por la herencia TPT), SQL Server no permite que las
            // dos rutas de borrado en cascada lleguen a la misma tabla
            // (PlanesPasaje). Ademas, tiene sentido para el negocio: no
            // queremos que borrar un atleta o entrenador borre en cascada,
            // en silencio, sus planes de pasajes historicos.
            modelBuilder.Entity<PlanPasaje>()
                .HasOne(p => p.Atleta)
                .WithMany()
                .HasForeignKey(p => p.IdAtleta)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanPasaje>()
                .HasOne(p => p.Entrenador)
                .WithMany(e => e.Planes)
                .HasForeignKey(p => p.IdEntrenador)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PlanPasaje>()
                .HasOne(p => p.Prueba)
                .WithMany()
                .HasForeignKey(p => p.IdPrueba)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pasaje>()
                .HasOne(pa => pa.Plan)
                .WithMany(pl => pl.Pasajes)
                .HasForeignKey(pa => pa.IdPlanPasaje);

            // ---------- Usuario de fabrica ----------
            //
            // Toda base de datos NUEVA (la tuya ahora, o la de un cliente el
            // dia de mañana) nace con este usuario ya cargado, como parte de
            // la migracion misma - sin editar codigo a mano en cada
            // instalacion.
            //
            // Usuario: admin / Contraseña: Admin1234
            // Pregunta de seguridad: ¿Cual es tu comida favorita? / pizza
            //
            // Los hashes de abajo son fijos (calculados una sola vez), no se
            // generan de nuevo en cada instalacion - por eso la contraseña
            // de fabrica es la MISMA en todas las instalaciones. Por
            // seguridad, el entrenador tiene que cambiarla apenas entra por
            // primera vez (con GestorSeguridad.CambiarContraseña).
            //
            // Con TPT, el HasData de Entrenador (con TODAS sus propiedades,
            // heredadas + propias) alcanza para sembrar tanto la fila de
            // Personas como la de Entrenadores - no hace falta (y de hecho
            // rompe) tener ademas un HasData separado para Persona con el
            // mismo Id.
            modelBuilder.Entity<Entrenador>().HasData(new
            {
                IdPersona = 1,
                Nombre = "Admin",
                Apellido = "Sistema",
                Email = "admin@natsys.local",
                Telefono = "",
                FechaNacimiento = new DateTime(2000, 1, 1),
                Estado = "activo",
                Especialidad = "Administrador"
            });

            modelBuilder.Entity<Usuario>().HasData(new
            {
                IdUsuario = 1,
                IdPersona = 1,
                NombreUsuario = "admin",
                Clave = "100000.4uvRpU0wa/QMbwBk3j9KVw==.3R4HkrQp2yVzE/gVZ1Ah24Pcd2IWeBNbwkOimRtx6aU=",
                Estado = "activo",
                IntentosFallidos = 0,
                PreguntaSeguridad = "¿Cuál es tu comida favorita?",
                RespuestaSeguridadHash = "100000.kESgVpXzptVDIk65q4lo0w==.9zFXADSlOndC/igsrxTKhiGfWubSxlAKtqA4z52luZ0="
            });

            // ---------- Permisos y Grupos de fabrica ----------
            //
            // Un permiso por cada pantalla de gestion. El nombre es lo que
            // usa GestorSeguridad.VerificarPermiso(usuario, "NOMBRE") para
            // decidir si habilitar o no un boton/pantalla.
            modelBuilder.Entity<Permiso>().HasData(
                new Permiso { IdPermiso = 1, Nombre = "GESTIONAR_ATLETAS", Modulo = "Atletas", Accion = "Gestionar" },
                new Permiso { IdPermiso = 2, Nombre = "GESTIONAR_TORNEOS", Modulo = "Torneos", Accion = "Gestionar" },
                new Permiso { IdPermiso = 3, Nombre = "GESTIONAR_MARCAS", Modulo = "Marcas", Accion = "Gestionar" },
                new Permiso { IdPermiso = 4, Nombre = "CALCULAR_PASAJES", Modulo = "Pasajes", Accion = "Calcular" },
                new Permiso { IdPermiso = 5, Nombre = "VER_REPORTES", Modulo = "Reportes", Accion = "Ver" },
                new Permiso { IdPermiso = 6, Nombre = "GESTIONAR_USUARIOS", Modulo = "Usuarios", Accion = "Gestionar" },
                new Permiso { IdPermiso = 7, Nombre = "GESTIONAR_GRUPOS", Modulo = "Grupos", Accion = "Gestionar" }
            );

            // Administradores: acceso total. Entrenadores: todo menos
            // Usuarios y Grupos (eso queda exclusivo del administrador).
            modelBuilder.Entity<Grupo>().HasData(
                new Grupo { IdGrupo = 1, Nombre = "Administradores", Descripcion = "Acceso total al sistema", Estado = "activo" },
                new Grupo { IdGrupo = 2, Nombre = "Entrenadores", Descripcion = "Gestion operativa del equipo", Estado = "activo" }
            );

            // ---------- Estilos y Pruebas oficiales ----------
            //
            // Son fijos por reglamento (World Aquatics), por eso se cargan
            // de fabrica en vez de tener una pantalla para gestionarlos.
            //
            // Los coeficientes de cada Estilo son el RESGUARDO: MotorPasajes
            // usa primero PerfilesRitmoElite (porcentajes reales de los
            // finalistas de Mundiales / JJOO para cada prueba y pileta), y
            // solo cae a estos si no hay perfil para esa combinacion. Los
            // nombres tienen que coincidir con las claves de
            // PerfilesRitmoElite.
            //
            // Combinado lleva 1/1/1 como valor neutro: todas sus distancias
            // validas (100/200/400) tienen perfil real.
            modelBuilder.Entity<Estilo>().HasData(
                new Estilo { IdEstilo = 1, Nombre = "Crol", CoefFatigaInicial = 0.97m, CoefFatigaMedio = 1.05m, CoefFatigaFinal = 1.00m },
                new Estilo { IdEstilo = 2, Nombre = "Espalda", CoefFatigaInicial = 0.97m, CoefFatigaMedio = 1.07m, CoefFatigaFinal = 1.01m },
                new Estilo { IdEstilo = 3, Nombre = "Pecho", CoefFatigaInicial = 0.96m, CoefFatigaMedio = 1.09m, CoefFatigaFinal = 1.02m },
                new Estilo { IdEstilo = 4, Nombre = "Mariposa", CoefFatigaInicial = 0.95m, CoefFatigaMedio = 1.10m, CoefFatigaFinal = 1.04m },
                new Estilo { IdEstilo = 5, Nombre = "Combinado", CoefFatigaInicial = 1.00m, CoefFatigaMedio = 1.00m, CoefFatigaFinal = 1.00m }
            );

            // Pruebas individuales oficiales. El 100 Combinado existe solo
            // en pileta corta (25m) - MotorPasajes lo valida al calcular.
            modelBuilder.Entity<Prueba>().HasData(
                // Crol
                new Prueba { IdPrueba = 1, Nombre = "50 Crol", Distancia = 50, IdEstilo = 1 },
                new Prueba { IdPrueba = 2, Nombre = "100 Crol", Distancia = 100, IdEstilo = 1 },
                new Prueba { IdPrueba = 3, Nombre = "200 Crol", Distancia = 200, IdEstilo = 1 },
                new Prueba { IdPrueba = 4, Nombre = "400 Crol", Distancia = 400, IdEstilo = 1 },
                new Prueba { IdPrueba = 5, Nombre = "800 Crol", Distancia = 800, IdEstilo = 1 },
                new Prueba { IdPrueba = 6, Nombre = "1500 Crol", Distancia = 1500, IdEstilo = 1 },
                // Espalda
                new Prueba { IdPrueba = 7, Nombre = "50 Espalda", Distancia = 50, IdEstilo = 2 },
                new Prueba { IdPrueba = 8, Nombre = "100 Espalda", Distancia = 100, IdEstilo = 2 },
                new Prueba { IdPrueba = 9, Nombre = "200 Espalda", Distancia = 200, IdEstilo = 2 },
                // Pecho
                new Prueba { IdPrueba = 10, Nombre = "50 Pecho", Distancia = 50, IdEstilo = 3 },
                new Prueba { IdPrueba = 11, Nombre = "100 Pecho", Distancia = 100, IdEstilo = 3 },
                new Prueba { IdPrueba = 12, Nombre = "200 Pecho", Distancia = 200, IdEstilo = 3 },
                // Mariposa
                new Prueba { IdPrueba = 13, Nombre = "50 Mariposa", Distancia = 50, IdEstilo = 4 },
                new Prueba { IdPrueba = 14, Nombre = "100 Mariposa", Distancia = 100, IdEstilo = 4 },
                new Prueba { IdPrueba = 15, Nombre = "200 Mariposa", Distancia = 200, IdEstilo = 4 },
                // Combinado
                new Prueba { IdPrueba = 16, Nombre = "100 Combinado", Distancia = 100, IdEstilo = 5 },
                new Prueba { IdPrueba = 17, Nombre = "200 Combinado", Distancia = 200, IdEstilo = 5 },
                new Prueba { IdPrueba = 18, Nombre = "400 Combinado", Distancia = 400, IdEstilo = 5 }
            );
        }
    }
}