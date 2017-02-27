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
	public class AniosAcademicosRepository : IRepository<AnioAcademico>, IAniosAcademicosRepository
	{
		private MatriculasContext _context;

		public AniosAcademicosRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(AnioAcademico entity)
        {
            _context.AniosAcademicos.Add(entity);
        }

        public void Delete(int id)
        {
            //var thisAnioAcademico = GetAnioAcademicoById(anioAcademicoToDelete.Id);
            //thisAnioAcademico.Estado = "0";

            //_context.Update(thisAnioAcademico);

            //return (thisAnioAcademico);
        }

        public AnioAcademico Get(int id)
        {
            return _context.AniosAcademicos
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<AnioAcademico> GetAll()
        {
            return _context.AniosAcademicos
                .Where(t => t.Estado == "1")
                .ToList();
        }

        public AnioAcademico GetAnioAcademico(int anio)
        {
            return _context.AniosAcademicos
                .Where(t => t.FechaInicio.Value.Year == anio)
                .FirstOrDefault();
        }

        public void Update(AnioAcademico entity)
        {
            //var thisAnioAcademico = GetAnioAcademicoById(anioAcademicoToUpdate.Id);
            //thisAnioAcademico.Nombre = anioAcademicoToUpdate.Nombre;
            //thisAnioAcademico.FechaInicio = anioAcademicoToUpdate.FechaInicio;
            //thisAnioAcademico.FechaFin = anioAcademicoToUpdate.FechaFin;

            //_context.Update(thisAnioAcademico);

            //return (thisAnioAcademico);
        }
    }
}
