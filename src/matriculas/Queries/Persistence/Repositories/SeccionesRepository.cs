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
	public class SeccionesRepository : IRepository<Seccion>, ISeccionesRepository
	{
		private MatriculasContext _context;

		public SeccionesRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(Seccion entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(int id)
        {
            var seccion = Get(id);
            _context.Entry(seccion).State = EntityState.Modified;
            seccion.Estado = "0";
        }

        public Seccion Get(int id)
        {
            return GetAll()
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Seccion> GetAll()
        {
            return _context.Secciones
                .Include(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .OrderBy(t => t.Grado.Nivel.Nombre)
                .ThenBy(t => t.Grado.Nombre)
                .ThenBy(t => t.Nombre)
                .Where(t => t.Estado == "1")
                .AsNoTracking()
                .ToList();
        }

        public Seccion GetByName(string name, int idGrado)
        {
            return GetAll()
                .Where(t => t.Nombre == name)
                .Where(t => t.Grado.Id == idGrado)
                .FirstOrDefault();
        }

        public IEnumerable<Alumno> GetLista(int id)
        {
            DateTime fechaActual = new DateTime(2017, 3, 10);

            var anioAcademico = _context.AniosAcademicos
                .Where(t => t.Estado == "1")
                .ToList()
                .Where(t => t.FechaInicio.Value.Year == fechaActual.Year)
                .FirstOrDefault();

            var lista = _context.Alumnos.FromSql(String.Format("EXEC SP_ListaAlumnosPorSeccion @idAnioAcademico={0}, @idSeccion={1}", anioAcademico.Id, id)).ToList();

            return lista;
        }

        public bool HasNombreUnique(Seccion entity)
        {
            if (GetByName(entity.Nombre, entity.Grado.Id) == null)
                return true;

            var aux = Get(entity.Id);
            if (Get(entity.Id) != null)
                return (entity.Nombre == aux.Nombre && entity.Grado.Id == aux.Grado.Id) ? true : false;

            return false;
        }

        public void Update(Seccion entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
