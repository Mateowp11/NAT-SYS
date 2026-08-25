// Proyecto: NatSys.DAL
// Instalar antes via NuGet: Microsoft.EntityFrameworkCore.SqlServer
//                           Microsoft.EntityFrameworkCore.Tools

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
                .HasMany(u => u.Grupos)
                .WithMany(g => g.Usuarios);

            modelBuilder.Entity<Grupo>()
                .HasMany(g => g.Permisos)
                .WithMany(p => p.Grupos);

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
                .HasForeignKey(m => m.IdAtleta);

            modelBuilder.Entity<Marca>()
                .HasOne(m => m.Prueba)
                .WithMany()
                .HasForeignKey(m => m.IdPrueba);

            modelBuilder.Entity<Marca>()
                .HasOne(m => m.TorneoRegistro)
                .WithMany(t => t.Marcas)
                .HasForeignKey(m => m.IdTorneo)
                .IsRequired(false); // una marca "test" o "SV" no tiene torneo

            modelBuilder.Entity<Prueba>()
                .HasOne(p => p.Estilo)
                .WithMany()
                .HasForeignKey(p => p.IdEstilo);

            modelBuilder.Entity<PlanPasaje>()
                .HasOne(p => p.Atleta)
                .WithMany()
                .HasForeignKey(p => p.IdAtleta);

            modelBuilder.Entity<PlanPasaje>()
                .HasOne(p => p.Entrenador)
                .WithMany(e => e.Planes)
                .HasForeignKey(p => p.IdEntrenador);

            modelBuilder.Entity<Pasaje>()
                .HasOne(pa => pa.Plan)
                .WithMany(pl => pl.Pasajes)
                .HasForeignKey(pa => pa.IdPlanPasaje);
        }
    }
}