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
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(int id)
        {
            var anioAcademico = Get(id);
            _context.Entry(anioAcademico).State = EntityState.Modified;
            anioAcademico.Estado = "0";
        }

        public AnioAcademico Get(int id)
        {
            return GetAll()
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<AnioAcademico> GetAll()
        {
            return _context.AniosAcademicos
                .Where(t => t.Estado == "1")
                .AsNoTracking()
                .ToList();
        }

        public AnioAcademico GetAnioAcademico(int anio)
        {
            return GetAll()
                .Where(t => t.Nombre == anio)
                .FirstOrDefault();
        }

        public AnioAcademico GetByName(int name)
        {
            return GetAll()
                .Where(t => t.Nombre == name)
                .FirstOrDefault();
        }

        public IEnumerable<Cronograma> GetCronogramas(int id)
        {
            return _context.Cronogramas
                .Where(t => t.AnioAcademicoId == id)
                .Where(t => t.Estado == "1")
                .ToList();
        }

        public bool HasNombreUnique(AnioAcademico entity)
        {
            if (GetByName(entity.Nombre) == null)
                return true;

            var aux = Get(entity.Id);
            if (Get(entity.Id) != null)
                return (entity.Nombre == aux.Nombre) ? true : false;

            return false;
        }

        public void Update(AnioAcademico entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
