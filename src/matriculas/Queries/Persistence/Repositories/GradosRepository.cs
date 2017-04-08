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
	public class GradosRepository : IRepository<Grado>, IGradosRepository
	{
		private MatriculasContext _context;

        public GradosRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(Grado entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(int id)
        {
            var grado = Get(id);
            _context.Entry(grado).State = EntityState.Modified;
            grado.Estado = "0";
        }

        public Grado Get(int id)
        {
            return GetAll()
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Grado> GetAll()
        {
            return _context.Grados
                .Include(t => t.GradoRequisito)
                .Include(t => t.Nivel)
                .OrderBy(t => t.Nivel.Nombre)
                .ThenBy(t => t.Nombre)
                .Where(t => t.Estado == "1")
                .AsNoTracking()
                .ToList();
        }

        public Grado GetByName(string name, int idNivel)
        {
            return GetAll()
                .Where(t => t.Nombre == name)
                .Where(t => t.Nivel.Id == idNivel)
                .FirstOrDefault();
        }

        public IEnumerable<Curso> GetCursos(int id)
        {
            var a = new CursosRepository(_context).GetAll();
            return a
                .Where(t => t.Estado == "1")
                .Where(t => t.Grado.Id == id)
                .ToList();
        }

        public int GetNroHoras(Grado entity)
        {
            return _context.Cursos
                .Where(t => t.Grado.Id == entity.Id)
                .Sum(t => t.HorasAcademicas);
        }

        public bool HasNombreUnique(Grado entity)
        {
            if (GetByName(entity.Nombre, entity.Nivel.Id) == null)
                return true;

            var aux = Get(entity.Id);
            if (Get(entity.Id) != null)
                return (entity.Nombre == aux.Nombre && entity.Nivel.Id == aux.Nivel.Id) ? true : false;

            return false;
        }

        public bool HasSecciones(Grado entity)
        {
            var aux = new SeccionesRepository(_context).GetAll()
                .Where(t => t.Grado.Id == entity.Id)
                .ToList();

            return (aux.Count > 0) ? true : false;
        }

        public void Update(Grado entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
