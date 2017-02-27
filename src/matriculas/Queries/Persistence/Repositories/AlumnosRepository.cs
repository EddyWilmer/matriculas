using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Matriculas.Queries.Core.Repositories;

namespace Matriculas.Queries.Persistence.Repositories
{
	public class AlumnosRepository : IRepository<Alumno>, IAlumnosRepository
	{
		private MatriculasContext _context;

		public AlumnosRepository(MatriculasContext context)
		{
			_context = context;
		}

		public void Add(Alumno entity)
		{
			_context.Alumnos.Add(entity);											
		}

		public void Delete(int id)
		{
            var alumno = Get(id);
            _context.Alumnos.Attach(alumno);
            alumno.Estado = "0";
        }

		public Alumno Get(int id)
		{
			return _context.Alumnos
			    .Include(t => t.Apoderado)
			    .Where(t => t.Id == id)
			    .FirstOrDefault();
		}

		public IEnumerable<Alumno> GetAll()
		{
			return _context.Alumnos
			    .Include(t => t.Apoderado)
			    .Where(t => t.Estado == "1")
			    .ToList();
		}

		public Alumno GetByDni(string dni)
		{
			return GetAll()
				.Where(t => t.Dni == dni)
				.First();
		}

        public Matricula GetLastMatricula(int id)
        {
            return _context.Matriculas
                .Include(t => t.Alumno)
                .Include(t => t.Seccion)
                .ThenInclude(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .Include(t => t.Registrador)
                .Include(t => t.AnioAcademico)
                .Where(t => t.AlumnoId == id)
                .Last();
        }

        public bool IsRegisteredDni(string dni)
		{
			var alumno = _context.Alumnos
			    .Where(t => t.Dni == dni)
			    .First();

			return alumno == null ? false : true;
		}

		public void Update(Alumno entity)
		{
			_context.Alumnos.Attach(entity);
			var entry = _context.Entry(entity).State = EntityState.Modified;
			_context.Entry(entity.Apoderado).State = EntityState.Modified;
		}
	}
}
