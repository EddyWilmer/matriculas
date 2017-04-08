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
	public class NivelesRepository : IRepository<Nivel>, INivelesRepository
	{
		private MatriculasContext _context;

		public NivelesRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(Nivel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Nivel Get(int id)
        {
            return _context.Niveles
                .Where(t => t.Id == id)
                .AsNoTracking()
                .FirstOrDefault();
        }

        public IEnumerable<Nivel> GetAll()
        {
            return _context.Niveles.ToList();
        }

        public void Update(Nivel entity)
        {
            throw new NotImplementedException();
        }
    }
}
