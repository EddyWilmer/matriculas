using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Persistence.Repositories
{
    public class ColaboradoresRepository : IRepository<Colaborador>, IColaboradoresRepository
    {
        private MatriculasContext _context;

        public ColaboradoresRepository(MatriculasContext context)
        {
            _context = context;
        }

        public void Add(Colaborador entity)
        {
            _context.Entry(entity).State = EntityState.Added;
        }

        public void Delete(int id)
        {
            var colaborador = Get(id);
            _context.Entry(colaborador).State = EntityState.Modified;
            colaborador.Estado = "2"; ;
        }

        public Colaborador Get(int id)
        {
            return _context.Colaboradores
                .Where(t => t.Id == id)
                .Include(t => t.Rol)
                .FirstOrDefault();
        }

        public IEnumerable<Colaborador> GetAll()
        {
            return _context.Colaboradores
                .Include(t => t.Rol)
                .OrderBy(t => t.ApellidoPaterno)
                .ThenBy(t => t.ApellidoMaterno)
                .ThenBy(t => t.Nombres)
                .Where(t => t.Estado != "2")
                .ToList();
        }

        public void ResetPassword(int id, UserManager<ApplicationUser> userManager)
        {
            var usuario = userManager.Users.Where(t => t.ColaboradorId == id).FirstOrDefault();
            var colaborador = Get(id);

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            usuario.PasswordHash = hasher.HashPassword(usuario, colaborador.Dni);

            _context.Update(usuario);
        }

        public void ToggleEstado(int id)
        {
            var colaborador = Get(id);
            _context.Colaboradores.Attach(colaborador).State = EntityState.Modified;

            var estado = colaborador.Estado;

            colaborador.Estado = estado == "1" ?  "3" : estado == "3" ? "1" : estado;
        }

        public void Update(Colaborador entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
