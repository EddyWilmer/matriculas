using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Matriculas.Models
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que proporciona el acceso a la base de datos.
    /// Esta clase es usada para crear las entidades a través de Entity Framework.
    /// </summary>
    public class MatriculasContext : IdentityDbContext<ApplicationUser>
    {
        private IConfigurationRoot _config;

        /// <summary>
        /// Constructor de la clase MatriculasContext.
        /// </summary>
        /// <param name="config">Ruta de acceso a la base de datos.</param>
        /// <param name="options">Configuración del contexto.</param>
        public MatriculasContext(IConfigurationRoot config, DbContextOptions options) 
            : base(options)
        {
            _config = config;
        }

        // Entidades en base de datos
        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Rol> Cargos { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<ProfesorCurso> ProfesorCursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Apoderado> Apoderados { get; set; }
        public DbSet<HistorialAlumno> HistorialAlumnos{ get; set; }
        public DbSet<HistorialApoderado> HistorialApoderados { get; set; }
        public DbSet<AnioAcademico> AniosAcademicos { get; set; }
        public DbSet<CronogramaMatricula> CronogramasMatricula { get; set; }
        public DbSet<CursoAnioAcademico> CursosAnioAcademico { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<Deuda> Deudas { get; set; }

        /// <summary>
        /// Metódo para configurar el contexto de la base de datos.
        /// </summary>
        /// <param name="optionsBuilder">Opciones de configuración.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MatriculasContextConnection"]);
        }

        /// <summary>
        /// Método para crear los modelos en la base de datos.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define valores por defecto 
            modelBuilder.Entity<Colaborador>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<Grado>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<Seccion>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<Curso>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<Profesor>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<Alumno>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<Apoderado>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<AnioAcademico>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            modelBuilder.Entity<CronogramaMatricula>()
                .Property(t => t.Estado)
                .HasDefaultValue("1");

            // Define las claves
            modelBuilder.Entity<ProfesorCurso>()
                .HasKey(t => new { t.ProfesorId, t.CursoId });

            modelBuilder.Entity<ProfesorCurso>()
                .HasOne(pc => pc.Profesor)
                .WithMany(p => p.ProfesorCurso)
                .HasForeignKey(pc => pc.ProfesorId);

            modelBuilder.Entity<ProfesorCurso>()
                .HasOne(pc => pc.Curso)
                .WithMany(c => c.ProfesorCurso)
                .HasForeignKey(pc => pc.CursoId);

            modelBuilder.Entity<HistorialAlumno>()
                .HasKey(t => new { t.AlumnoId, t.FechaIngreso });

            modelBuilder.Entity<HistorialApoderado>()
                .HasKey(t => new { t.Id, t.FechaInicio });

            modelBuilder.Entity<CronogramaMatricula>()
                .HasKey(t => new { t.AnioAcademicoId, t.Nombre });

            modelBuilder.Entity<CursoAnioAcademico>()
                .HasKey(t => new { t.AnioAcademicoId, t.CursoId, t.GradoId });

            modelBuilder.Entity<Nota>()
                .HasKey(t => new { t.MatriculaId, t.CursoId });

            modelBuilder.Entity<Deuda>()
                .HasKey(t => new { t.MatriculaId, t.Mes });
        }    
    }
}
