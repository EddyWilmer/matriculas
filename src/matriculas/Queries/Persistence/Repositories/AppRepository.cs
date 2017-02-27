using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Persistence.Repositories
{
    public class AppRepository : IAppRepository
    {
		private MatriculasContext _context;
        private UserManager<ApplicationUser> _userManager;

        public AppRepository(MatriculasContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
            _userManager = userManager;
			Alumnos = new AlumnosRepository(_context);
            Colaboradores = new ColaboradoresRepository(_context);
            Cargos = new CargosRepository(_context);
            Grados = new GradosRepository(_context);
            Secciones = new SeccionesRepository(_context);
            Niveles = new NivelesRepository(_context);
            Cursos = new CursosRepository(_context);
            Profesores = new ProfesoresRepository(_context);
            AniosAcademicos = new AniosAcademicosRepository(_context);
            Cronogramas = new CronogramasRepository(_context);
        }

        public IAlumnosRepository Alumnos { get; private set; }
        public IColaboradoresRepository Colaboradores { get; private set; }
        public ICargosRepository Cargos { get; private set; }
        public IGradosRepository Grados { get; private set; }
        public ISeccionesRepository Secciones { get; private set; }
        public INivelesRepository Niveles { get; private set; }
        public ICursosRepository Cursos { get; private set; }
        public IProfesoresRepository Profesores { get; private set; }
        public IAniosAcademicosRepository AniosAcademicos { get; private set; }
        public ICronogramasRepository Cronogramas { get; private set; }

        public async Task<bool> Complete()
		{
		    return (await _context.SaveChangesAsync()) > 0;
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
