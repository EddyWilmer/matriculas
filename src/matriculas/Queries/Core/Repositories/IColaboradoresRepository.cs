using Matriculas.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Core.Repositories
{
    public interface IColaboradoresRepository : IRepository<Colaborador>
    {
        void ToggleEstado(int id);

        void ResetPassword(int id, UserManager<ApplicationUser> userManager);

        bool HasDniUnique(Colaborador entity);

        Colaborador GetByDni(string dni);
    }
}
