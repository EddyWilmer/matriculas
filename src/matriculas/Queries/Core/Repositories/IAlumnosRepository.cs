using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IAlumnosRepository : IRepository<Alumno>
    {
		bool IsRegisteredDni(string dni);

		Alumno GetByDni(string dni);

        Matricula GetLastMatricula(int id);
	}
}
