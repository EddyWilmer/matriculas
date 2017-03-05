using Matriculas.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IMatriculasRepository : IRepository<Matricula>
    {
        Seccion ChooseSeccion(Grado grado);
	}
}
