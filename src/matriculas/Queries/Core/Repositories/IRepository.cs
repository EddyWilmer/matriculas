using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
								IEnumerable<TEntity> GetAll();
								TEntity Get(int id);
								void Add(TEntity entity);
								void Update(TEntity entity);
								void Delete(int id);
				}
}
