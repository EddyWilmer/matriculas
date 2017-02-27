using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Matriculas.Models;

namespace matriculas.Migrations
{
    [DbContext(typeof(MatriculasContext))]
    [Migration("20170227161150_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Matriculas.Models.Alumno", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApellidoMaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("ApellidoPaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<int?>("ApoderadoId");

                    b.Property<string>("Direccion")
                        .HasAnnotation("MaxLength", 70);

                    b.Property<string>("Dni")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("DATE");

                    b.Property<string>("Nombres")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Sexo")
                        .HasAnnotation("MaxLength", 1);

                    b.HasKey("Id");

                    b.HasIndex("ApoderadoId");

                    b.ToTable("Alumnos");
                });

            modelBuilder.Entity("Matriculas.Models.AnioAcademico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("DATE");

                    b.Property<DateTime?>("FechaInicio")
                        .HasColumnType("DATE");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 20);

                    b.HasKey("Id");

                    b.ToTable("AniosAcademicos");
                });

            modelBuilder.Entity("Matriculas.Models.Apoderado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApellidoMaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("ApellidoPaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Dni")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<string>("EstadoCivil")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<string>("Nombres")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Sexo")
                        .HasAnnotation("MaxLength", 1);

                    b.HasKey("Id");

                    b.ToTable("Apoderados");
                });

            modelBuilder.Entity("Matriculas.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("ColaboradorId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("ColaboradorId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Matriculas.Models.Cargo", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("Matriculas.Models.Colaborador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApellidoMaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("ApellidoPaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Celular")
                        .HasAnnotation("MaxLength", 9);

                    b.Property<string>("Dni")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<string>("Nombres")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<int?>("RolId");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Colaboradores");
                });

            modelBuilder.Entity("Matriculas.Models.Cronograma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AnioAcademicoId");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<DateTime?>("FechaFin")
                        .HasColumnType("DATE");

                    b.Property<DateTime?>("FechaInicio")
                        .HasColumnType("DATE");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 30);

                    b.HasKey("Id");

                    b.HasIndex("AnioAcademicoId");

                    b.ToTable("Cronogramas");
                });

            modelBuilder.Entity("Matriculas.Models.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<int?>("GradoId");

                    b.Property<int>("HorasAcademicas");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");

                    b.HasIndex("GradoId");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Matriculas.Models.CursoAnioAcademico", b =>
                {
                    b.Property<int>("AnioAcademicoId");

                    b.Property<int>("CursoId");

                    b.Property<int>("GradoId");

                    b.Property<int?>("ProfesorId");

                    b.HasKey("AnioAcademicoId", "CursoId", "GradoId");

                    b.HasIndex("AnioAcademicoId");

                    b.HasIndex("CursoId");

                    b.HasIndex("GradoId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("CursosAniosAcademicos");
                });

            modelBuilder.Entity("Matriculas.Models.Deuda", b =>
                {
                    b.Property<int>("MatriculaId");

                    b.Property<int>("Mes");

                    b.Property<double>("Monto");

                    b.HasKey("MatriculaId", "Mes");

                    b.HasIndex("MatriculaId");

                    b.ToTable("Deudas");
                });

            modelBuilder.Entity("Matriculas.Models.Grado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacidad");

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<int?>("GradoRequisitoId");

                    b.Property<int?>("NivelId");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");

                    b.HasIndex("GradoRequisitoId");

                    b.HasIndex("NivelId");

                    b.ToTable("Grados");
                });

            modelBuilder.Entity("Matriculas.Models.HistorialAlumno", b =>
                {
                    b.Property<int>("AlumnoId");

                    b.Property<DateTime?>("FechaIngreso");

                    b.Property<DateTime?>("FechaRetiro");

                    b.HasKey("AlumnoId", "FechaIngreso");

                    b.HasIndex("AlumnoId");

                    b.ToTable("HistorialAlumnos");
                });

            modelBuilder.Entity("Matriculas.Models.HistorialApoderado", b =>
                {
                    b.Property<int>("Id");

                    b.Property<DateTime?>("FechaInicio");

                    b.Property<int?>("AlumnoId");

                    b.Property<string>("ApellidoMaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("ApellidoPaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Dni")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<string>("EstadoCivil")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<DateTime?>("FechaFin");

                    b.Property<string>("Nombres")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Sexo")
                        .HasAnnotation("MaxLength", 1);

                    b.HasKey("Id", "FechaInicio");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("Id");

                    b.ToTable("HistorialApoderados");
                });

            modelBuilder.Entity("Matriculas.Models.Matricula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlumnoId");

                    b.Property<int>("AnioAcademicoId");

                    b.Property<DateTime?>("Fecha");

                    b.Property<int>("RegistradorId");

                    b.Property<int>("SeccionId");

                    b.HasKey("Id");

                    b.HasIndex("AlumnoId");

                    b.HasIndex("AnioAcademicoId");

                    b.HasIndex("RegistradorId");

                    b.HasIndex("SeccionId");

                    b.ToTable("Matriculas");
                });

            modelBuilder.Entity("Matriculas.Models.Nivel", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 25);

                    b.HasKey("Id");

                    b.ToTable("Niveles");
                });

            modelBuilder.Entity("Matriculas.Models.Nota", b =>
                {
                    b.Property<int>("MatriculaId");

                    b.Property<int>("CursoId");

                    b.Property<int?>("Calificacion");

                    b.HasKey("MatriculaId", "CursoId");

                    b.HasIndex("CursoId");

                    b.HasIndex("MatriculaId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("Matriculas.Models.Profesor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApellidoMaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("ApellidoPaterno")
                        .HasAnnotation("MaxLength", 25);

                    b.Property<string>("Celular")
                        .HasAnnotation("MaxLength", 9);

                    b.Property<string>("Dni")
                        .HasAnnotation("MaxLength", 8);

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<string>("Nombres")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Sexo")
                        .HasAnnotation("MaxLength", 1);

                    b.HasKey("Id");

                    b.ToTable("Profesores");
                });

            modelBuilder.Entity("Matriculas.Models.ProfesorCurso", b =>
                {
                    b.Property<int>("ProfesorId");

                    b.Property<int>("CursoId");

                    b.HasKey("ProfesorId", "CursoId");

                    b.HasIndex("CursoId");

                    b.HasIndex("ProfesorId");

                    b.ToTable("ProfesoresCursos");
                });

            modelBuilder.Entity("Matriculas.Models.Seccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Estado")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue("1")
                        .HasAnnotation("MaxLength", 1);

                    b.Property<int?>("GradoId");

                    b.Property<string>("Nombre")
                        .HasAnnotation("MaxLength", 1);

                    b.HasKey("Id");

                    b.HasIndex("GradoId");

                    b.ToTable("Secciones");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Matriculas.Models.Alumno", b =>
                {
                    b.HasOne("Matriculas.Models.Apoderado", "Apoderado")
                        .WithMany()
                        .HasForeignKey("ApoderadoId");
                });

            modelBuilder.Entity("Matriculas.Models.ApplicationUser", b =>
                {
                    b.HasOne("Matriculas.Models.Colaborador", "Colaborador")
                        .WithMany()
                        .HasForeignKey("ColaboradorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.Colaborador", b =>
                {
                    b.HasOne("Matriculas.Models.Cargo", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId");
                });

            modelBuilder.Entity("Matriculas.Models.Cronograma", b =>
                {
                    b.HasOne("Matriculas.Models.AnioAcademico", "AnioAcademico")
                        .WithMany()
                        .HasForeignKey("AnioAcademicoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.Curso", b =>
                {
                    b.HasOne("Matriculas.Models.Grado", "Grado")
                        .WithMany()
                        .HasForeignKey("GradoId");
                });

            modelBuilder.Entity("Matriculas.Models.CursoAnioAcademico", b =>
                {
                    b.HasOne("Matriculas.Models.AnioAcademico", "AnioAcademico")
                        .WithMany()
                        .HasForeignKey("AnioAcademicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Grado", "Grado")
                        .WithMany()
                        .HasForeignKey("GradoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Profesor", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorId");
                });

            modelBuilder.Entity("Matriculas.Models.Deuda", b =>
                {
                    b.HasOne("Matriculas.Models.Matricula", "Matricula")
                        .WithMany()
                        .HasForeignKey("MatriculaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.Grado", b =>
                {
                    b.HasOne("Matriculas.Models.Grado", "GradoRequisito")
                        .WithMany()
                        .HasForeignKey("GradoRequisitoId");

                    b.HasOne("Matriculas.Models.Nivel", "Nivel")
                        .WithMany()
                        .HasForeignKey("NivelId");
                });

            modelBuilder.Entity("Matriculas.Models.HistorialAlumno", b =>
                {
                    b.HasOne("Matriculas.Models.Alumno", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.HistorialApoderado", b =>
                {
                    b.HasOne("Matriculas.Models.Alumno", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId");

                    b.HasOne("Matriculas.Models.Apoderado", "Apoderado")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.Matricula", b =>
                {
                    b.HasOne("Matriculas.Models.Alumno", "Alumno")
                        .WithMany()
                        .HasForeignKey("AlumnoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.AnioAcademico", "AnioAcademico")
                        .WithMany()
                        .HasForeignKey("AnioAcademicoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Colaborador", "Registrador")
                        .WithMany()
                        .HasForeignKey("RegistradorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Seccion", "Seccion")
                        .WithMany()
                        .HasForeignKey("SeccionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.Nota", b =>
                {
                    b.HasOne("Matriculas.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Matricula", "Matricula")
                        .WithMany()
                        .HasForeignKey("MatriculaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.ProfesorCurso", b =>
                {
                    b.HasOne("Matriculas.Models.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.Profesor", "Profesor")
                        .WithMany("ProfesorCurso")
                        .HasForeignKey("ProfesorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Matriculas.Models.Seccion", b =>
                {
                    b.HasOne("Matriculas.Models.Grado", "Grado")
                        .WithMany()
                        .HasForeignKey("GradoId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Matriculas.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Matriculas.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Matriculas.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
