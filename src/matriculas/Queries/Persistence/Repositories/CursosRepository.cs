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
    public class CursosRepository : IRepository<Curso>, ICursosRepository
    {
        private MatriculasContext _context;

        public CursosRepository(MatriculasContext context)
        {
            _context = context;
        }

        public void Add(Curso entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void AssignProfesor(int id, int idProfesor)
        {
            var anioAcademico = new AniosAcademicosRepository(_context).GetAnioAcademico(DateTime.Now.Year);

            if (anioAcademico != null)
            {
                var cursoAnioAcademico = _context.CursosAnioAcademico
                    .Where(t => t.AnioAcademicoId == anioAcademico.Id)
                    .Where(t => t.CursoId == id)
                    .FirstOrDefault();

                _context.Entry(cursoAnioAcademico).State = EntityState.Modified;

                if (idProfesor != 0)
                    cursoAnioAcademico.ProfesorId = idProfesor;
                else
                    cursoAnioAcademico.ProfesorId = null;
            }
        }

        public void Delete(int id)
        {
            var curso = Get(id);
            _context.Entry(curso).State = EntityState.Modified;
            curso.Estado = "0";
        }

        public Curso Get(int id)
        {
            return GetAll()
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Curso> GetAll()
        {
            return _context.Cursos
                .Include(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .OrderBy(t => t.Grado.Nivel.Nombre)
                .ThenBy(t => t.Grado.Nombre)
                .ThenBy(t => t.Nombre)
                .Where(t => t.Estado == "1")
                .ToList();
        }

        public Profesor GetProfesor(int id)
        {
            var curso = new CursosRepository(_context).Get(id);

            var anioAcademico = new AniosAcademicosRepository(_context).GetAnioAcademico(DateTime.Now.Year);

            if (anioAcademico != null)
            {
                return _context.CursosAnioAcademico
                    .Include(t => t.Profesor)
                    .Where(t => t.AnioAcademicoId == anioAcademico.Id)
                    .Where(t => t.CursoId == curso.Id)
                    .Select(t => t.Profesor)
                    .FirstOrDefault();
            }
            return null;
        }

        public IEnumerable<Profesor> SearchProfesores(int id)
        {
            return _context.ProfesorCursos
               .Include(t => t.Profesor)
               .Where(t => t.CursoId == id)
               .Select(t => t.Profesor)
               .ToList();
        }

        public void Update(Curso entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
