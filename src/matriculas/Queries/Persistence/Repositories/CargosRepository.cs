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
	public class CargosRepository : IRepository<Cargo>, ICargosRepository
	{
		private MatriculasContext _context;

        public CargosRepository(MatriculasContext context)
		{
			_context = context;
		}

        public void Add(Cargo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Cargo Get(int id)
        {
            return _context.Cargos.Where(t => t.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Cargo> GetAll()
        {
            return _context.Cargos
                .ToList();
        }

        public void Update(Cargo entity)
        {
            throw new NotImplementedException();
        }
    }
}
