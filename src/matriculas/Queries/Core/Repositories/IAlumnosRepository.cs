using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IAlumnosRepository : IRepository<Alumno>
    {
		Alumno GetByDni(string dni);

        Grado GetGrado(int id);

        Grado GetNextGrado(int id);

        bool HasDniUnique(Alumno entity);
	}
}
