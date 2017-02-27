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
	public class CronogramasMatriculasRepository : IRepository<CronogramaMatricula>, ICronogramasMatriculasRepository
	{
		private MatriculasContext _context;

		public CronogramasMatriculasRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(CronogramaMatricula entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(int id)
        {
            var cronogramaMatricula = Get(id);
            _context.Entry(cronogramaMatricula).State = EntityState.Modified;
            cronogramaMatricula.Estado = "0";
        }

        public CronogramaMatricula Get(int id)
        {
            return _context.CronogramasMatricula
                .Where(t => t.AnioAcademicoId == id)
                .FirstOrDefault();
        }

        public IEnumerable<CronogramaMatricula> GetAll()
        {
            return _context.CronogramasMatricula
                .ToList();
        }

        public CronogramaMatricula GetCronograma(int idAnioAcademico, string nombre)
        {
            return _context.CronogramasMatricula
                .Where(t => t.AnioAcademicoId == idAnioAcademico)
                .Where(t => t.Nombre == nombre)
                .FirstOrDefault();
        }

        public void Update(CronogramaMatricula entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
