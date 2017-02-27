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
	public class CronogramasRepository : IRepository<Cronograma>, ICronogramasRepository
	{
		private MatriculasContext _context;

		public CronogramasRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(Cronograma entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(int id)
        {
            var cronograma = Get(id);
            _context.Entry(cronograma).State = EntityState.Modified;
            cronograma.Estado = "0";
        }

        public Cronograma Get(int id)
        {
            return _context.Cronogramas
                .Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Cronograma> GetAll()
        {
            return _context.Cronogramas
                .ToList();
        }

        public void Update(Cronograma entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
