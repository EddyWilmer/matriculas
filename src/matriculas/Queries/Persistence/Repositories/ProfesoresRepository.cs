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
	public class ProfesoresRepository : IRepository<Profesor>, IProfesoresRepository
	{
		private MatriculasContext _context;

		public ProfesoresRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(Profesor entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            AddCursos(entity.Id, entity.Cursos);
        }

        public void AddCursos(int id, IEnumerable<Curso> cursos)
        {
            foreach (Curso item in cursos)
            {
                var aux = new ProfesorCurso();
                aux.ProfesorId = id;
                aux.CursoId = item.Id;

                _context.ProfesorCursos
                    .Add(aux);
            }
        }

        public void Delete(int id)
        {
            DeleteCursos(id);
            var profesor = Get(id);
            _context.Entry(profesor).State = EntityState.Modified;
            profesor.Estado = "0";
        }

        public void DeleteCursos(int id)
        {
            var cursos = _context.ProfesorCursos
                .Where(t => t.ProfesorId == id)
                .ToList();

            _context.ProfesorCursos.RemoveRange(cursos);
            _context.SaveChanges();
        }

        public Profesor Get(int id)
        {
            return _context.Profesores
                .Where(t => t.Id == id)
                .Include(t => t.ProfesorCurso)
                .ThenInclude(t => t.Curso)
                .FirstOrDefault();
        }

        public IEnumerable<Profesor> GetAll()
        {
            return _context.Profesores
                .Where(t => t.Estado == "1")
                .ToList();
        }

        public IEnumerable<Curso> GetCursos(int id)
        {
            var cursosProfesor = _context.ProfesorCursos
                .Where(t => t.ProfesorId == id)
                .Include(t => t.Curso)
                .ToList();

            List<Curso> cursos = new List<Curso>();
            foreach (ProfesorCurso curso in cursosProfesor)
            {
                cursos.Add(new CursosRepository(_context).Get(curso.CursoId));
            }

            return cursos.AsEnumerable();
        }

        public void Update(Profesor entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            DeleteCursos(entity.Id);
            AddCursos(entity.Id, entity.Cursos);         
        }
    }
}
