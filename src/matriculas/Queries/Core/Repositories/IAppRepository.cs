using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IAppRepository : IDisposable
    {
        IAlumnosRepository Alumnos { get; }

        IColaboradoresRepository Colaboradores { get; }

        ICargosRepository Cargos { get; }

        IGradosRepository Grados { get; }

        ISeccionesRepository Secciones { get; }

        INivelesRepository Niveles { get; }

        ICursosRepository Cursos { get; }

        IProfesoresRepository Profesores { get; }

        IAniosAcademicosRepository AniosAcademicos { get; }

        ICronogramasMatriculasRepository CronogramasMatriculas { get; }

        Task<bool> Complete();
	}
}
