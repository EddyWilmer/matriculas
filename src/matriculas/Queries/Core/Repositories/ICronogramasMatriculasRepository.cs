using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface ICronogramasMatriculasRepository : IRepository<CronogramaMatricula>
    {
        CronogramaMatricula GetCronograma(int idAnioAcademico, string nombre);
	}
}
