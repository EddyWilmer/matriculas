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
			return GetAll()
			    .Where(t => t.Id == id)           
			    .FirstOrDefault();
		}

		public IEnumerable<Alumno> GetAll()
		{
            return _context.Alumnos
                .Include(t => t.Apoderado)
                .Where(t => t.Estado == "1")
                .AsNoTracking()
			    .ToList();
		}

		public Alumno GetByDni(string dni)
		{
			return GetAll()             
				.Where(t => t.Dni == dni)
                .FirstOrDefault();
		}

        public Grado GetGrado(int id)
        {
            var grado = _context.Matriculas
                .Where(t => t.AlumnoId == id)
                .Select(t => t.Seccion.Grado)
                .Last();

            return new GradosRepository(_context).Get(grado.Id);
        }

        public Grado GetNextGrado(int id)
        {
            var gradoRequisito = GetGrado(id);

            return _context.Grados
                .Where(t => t.GradoRequisito.Id == gradoRequisito.Id)
                .FirstOrDefault();
        }

        public bool HasDniUnique(Alumno entity)
        {
            if (GetByDni(entity.Dni) == null)
                return true;

            if (Get(entity.Id) != null)
                return (entity.Dni == Get(entity.Id).Dni) ? true : false;

            return false;
        }

        public bool IsMatriculado(Alumno entity)
        {
            var matriculaAnioAcademico = _context.Matriculas
                .Where(t => t.AlumnoId == entity.Id)
                .Where(t => t.AnioAcademicoId == new AniosAcademicosRepository(_context).GetByName(DateTime.Now.Year).Id)
                .FirstOrDefault();

            if (matriculaAnioAcademico != null)
                return true;
            else
                return false;
        }

        public void Update(Alumno entity)
		{
            _context.Entry(entity.Apoderado).State = EntityState.Modified;
			_context.Entry(entity).State = EntityState.Modified;
		}
	}
}
