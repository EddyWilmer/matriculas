using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace matriculas.Migrations
{
    public partial class Matriculas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AniosAcademicos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    FechaFin = table.Column<DateTime>(type: "DATE", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "DATE", nullable: true),
                    Nombre = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AniosAcademicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apoderados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApellidoMaterno = table.Column<string>(maxLength: 25, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 25, nullable: true),
                    Dni = table.Column<string>(maxLength: 8, nullable: true),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    EstadoCivil = table.Column<string>(maxLength: 1, nullable: true),
                    Nombres = table.Column<string>(maxLength: 50, nullable: true),
                    Sexo = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apoderados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cargos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Niveles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 25, nullable: true),
                    NroHoras = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApellidoMaterno = table.Column<string>(maxLength: 25, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 25, nullable: true),
                    Celular = table.Column<string>(maxLength: 9, nullable: true),
                    Dni = table.Column<string>(maxLength: 8, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    Nombres = table.Column<string>(maxLength: 50, nullable: true),
                    Sexo = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Cronogramas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnioAcademicoId = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    FechaFin = table.Column<DateTime>(type: "DATE", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "DATE", nullable: true),
                    Nombre = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cronogramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cronogramas_AniosAcademicos_AnioAcademicoId",
                        column: x => x.AnioAcademicoId,
                        principalTable: "AniosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Alumnos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApellidoMaterno = table.Column<string>(maxLength: 25, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 25, nullable: true),
                    ApoderadoId = table.Column<int>(nullable: true),
                    Direccion = table.Column<string>(maxLength: 70, nullable: true),
                    Dni = table.Column<string>(maxLength: 8, nullable: true),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    FechaNacimiento = table.Column<DateTime>(type: "DATE", nullable: false),
                    Nombres = table.Column<string>(maxLength: 50, nullable: true),
                    Sexo = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alumnos_Apoderados_ApoderadoId",
                        column: x => x.ApoderadoId,
                        principalTable: "Apoderados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Colaboradores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApellidoMaterno = table.Column<string>(maxLength: 25, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 25, nullable: true),
                    Celular = table.Column<string>(maxLength: 9, nullable: true),
                    Dni = table.Column<string>(maxLength: 8, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    Nombres = table.Column<string>(maxLength: 50, nullable: true),
                    RolId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colaboradores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colaboradores_Cargos_RolId",
                        column: x => x.RolId,
                        principalTable: "Cargos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Capacidad = table.Column<int>(nullable: false),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    GradoRequisitoId = table.Column<int>(nullable: true),
                    NivelId = table.Column<int>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grados_Grados_GradoRequisitoId",
                        column: x => x.GradoRequisitoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grados_Niveles_NivelId",
                        column: x => x.NivelId,
                        principalTable: "Niveles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialAlumnos",
                columns: table => new
                {
                    AlumnoId = table.Column<int>(nullable: false),
                    FechaIngreso = table.Column<DateTime>(nullable: false),
                    FechaRetiro = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAlumnos", x => new { x.AlumnoId, x.FechaIngreso });
                    table.ForeignKey(
                        name: "FK_HistorialAlumnos_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialApoderados",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FechaInicio = table.Column<DateTime>(nullable: false),
                    AlumnoId = table.Column<int>(nullable: true),
                    ApellidoMaterno = table.Column<string>(maxLength: 25, nullable: true),
                    ApellidoPaterno = table.Column<string>(maxLength: 25, nullable: true),
                    Dni = table.Column<string>(maxLength: 8, nullable: true),
                    EstadoCivil = table.Column<string>(maxLength: 1, nullable: true),
                    FechaFin = table.Column<DateTime>(nullable: true),
                    Nombres = table.Column<string>(maxLength: 50, nullable: true),
                    Sexo = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialApoderados", x => new { x.Id, x.FechaInicio });
                    table.ForeignKey(
                        name: "FK_HistorialApoderados_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialApoderados_Apoderados_Id",
                        column: x => x.Id,
                        principalTable: "Apoderados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ColaboradorId = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Colaboradores_ColaboradorId",
                        column: x => x.ColaboradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    GradoId = table.Column<int>(nullable: true),
                    HorasAcademicas = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cursos_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Secciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Estado = table.Column<string>(maxLength: 1, nullable: true, defaultValue: "1"),
                    GradoId = table.Column<int>(nullable: true),
                    Nombre = table.Column<string>(maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Secciones_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursosAniosAcademicos",
                columns: table => new
                {
                    AnioAcademicoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    GradoId = table.Column<int>(nullable: false),
                    ProfesorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursosAniosAcademicos", x => new { x.AnioAcademicoId, x.CursoId, x.GradoId });
                    table.ForeignKey(
                        name: "FK_CursosAniosAcademicos_AniosAcademicos_AnioAcademicoId",
                        column: x => x.AnioAcademicoId,
                        principalTable: "AniosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursosAniosAcademicos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursosAniosAcademicos_Grados_GradoId",
                        column: x => x.GradoId,
                        principalTable: "Grados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursosAniosAcademicos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfesoresCursos",
                columns: table => new
                {
                    ProfesorId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfesoresCursos", x => new { x.ProfesorId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_ProfesoresCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfesoresCursos_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matriculas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AlumnoId = table.Column<int>(nullable: false),
                    AnioAcademicoId = table.Column<int>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: true),
                    RegistradorId = table.Column<int>(nullable: false),
                    SeccionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matriculas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matriculas_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_AniosAcademicos_AnioAcademicoId",
                        column: x => x.AnioAcademicoId,
                        principalTable: "AniosAcademicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Colaboradores_RegistradorId",
                        column: x => x.RegistradorId,
                        principalTable: "Colaboradores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matriculas_Secciones_SeccionId",
                        column: x => x.SeccionId,
                        principalTable: "Secciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deudas",
                columns: table => new
                {
                    MatriculaId = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Monto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deudas", x => new { x.MatriculaId, x.Mes });
                    table.ForeignKey(
                        name: "FK_Deudas_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    MatriculaId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    Calificacion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => new { x.MatriculaId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_Notas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notas_Matriculas_MatriculaId",
                        column: x => x.MatriculaId,
                        principalTable: "Matriculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alumnos_ApoderadoId",
                table: "Alumnos",
                column: "ApoderadoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ColaboradorId",
                table: "AspNetUsers",
                column: "ColaboradorId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colaboradores_RolId",
                table: "Colaboradores",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Cronogramas_AnioAcademicoId",
                table: "Cronogramas",
                column: "AnioAcademicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_GradoId",
                table: "Cursos",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAniosAcademicos_AnioAcademicoId",
                table: "CursosAniosAcademicos",
                column: "AnioAcademicoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAniosAcademicos_CursoId",
                table: "CursosAniosAcademicos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAniosAcademicos_GradoId",
                table: "CursosAniosAcademicos",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursosAniosAcademicos_ProfesorId",
                table: "CursosAniosAcademicos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Deudas_MatriculaId",
                table: "Deudas",
                column: "MatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Grados_GradoRequisitoId",
                table: "Grados",
                column: "GradoRequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Grados_NivelId",
                table: "Grados",
                column: "NivelId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAlumnos_AlumnoId",
                table: "HistorialAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialApoderados_AlumnoId",
                table: "HistorialApoderados",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialApoderados_Id",
                table: "HistorialApoderados",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AlumnoId",
                table: "Matriculas",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_AnioAcademicoId",
                table: "Matriculas",
                column: "AnioAcademicoId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_RegistradorId",
                table: "Matriculas",
                column: "RegistradorId");

            migrationBuilder.CreateIndex(
                name: "IX_Matriculas_SeccionId",
                table: "Matriculas",
                column: "SeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_CursoId",
                table: "Notas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_MatriculaId",
                table: "Notas",
                column: "MatriculaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresCursos_CursoId",
                table: "ProfesoresCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfesoresCursos_ProfesorId",
                table: "ProfesoresCursos",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Secciones_GradoId",
                table: "Secciones",
                column: "GradoId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cronogramas");

            migrationBuilder.DropTable(
                name: "CursosAniosAcademicos");

            migrationBuilder.DropTable(
                name: "Deudas");

            migrationBuilder.DropTable(
                name: "HistorialAlumnos");

            migrationBuilder.DropTable(
                name: "HistorialApoderados");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "ProfesoresCursos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Matriculas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Profesores");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Alumnos");

            migrationBuilder.DropTable(
                name: "AniosAcademicos");

            migrationBuilder.DropTable(
                name: "Secciones");

            migrationBuilder.DropTable(
                name: "Colaboradores");

            migrationBuilder.DropTable(
                name: "Apoderados");

            migrationBuilder.DropTable(
                name: "Grados");

            migrationBuilder.DropTable(
                name: "Cargos");

            migrationBuilder.DropTable(
                name: "Niveles");
        }
    }
}
