using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IProfesoresRepository : IRepository<Profesor>
    {
        IEnumerable<Curso> GetCursos(int id);

        void DeleteCursos(int id);

        void AddCursos(int id, IEnumerable<Curso> cursos);
	}
}
