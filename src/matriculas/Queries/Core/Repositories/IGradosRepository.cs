using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IGradosRepository : IRepository<Grado>
    {
        IEnumerable<Curso> GetCursos(int id);

        Grado GetByName(string name, int idNivel);

        bool HasNombreUnique(Grado entity);

        bool HasSecciones(Grado entity);

        int GetNroHoras(Grado entity);
	}
}
