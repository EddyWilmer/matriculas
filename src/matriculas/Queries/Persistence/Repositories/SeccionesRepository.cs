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
            return _context.Secciones
                .Include(t => t.Grado)
                .ThenInclude(t => t.Nivel)
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
                .ToList();
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

        public void Update(Seccion entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
