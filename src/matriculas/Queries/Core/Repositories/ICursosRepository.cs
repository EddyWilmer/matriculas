using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface ICursosRepository : IRepository<Curso>
    {
        IEnumerable<Profesor> SearchProfesores(int id);

        void AssignProfesor(int id, int idProfesor);

        Profesor GetProfesor(int id);

        Curso GetByName(string name, int idGrado);

        bool HasNombreUnique(Curso entity);

        bool FitSchedule(Curso entity);
    }
}
