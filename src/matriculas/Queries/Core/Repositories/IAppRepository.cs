using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IAppRepository : IDisposable
    {
								IAlumnoRepository Alumnos { get; }
								Task<bool> Complete();
				}
}
