using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que siembra data en la base de datos
    /// </summary>
    public class MatriculasContextSeedData
    {
        private MatriculasContext _context;
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor de la clase MatriculasContextSeedData
        /// </summary>
        /// <param name="context">Contexto de la aplicación.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        /// <param name="roleManager">Administrador de roles.</param>
        public MatriculasContextSeedData(
            MatriculasContext context, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// Clase para insertar data en tiempo asíncrono.
        /// </summary>
        /// <returns>Hilo de procesamiento.</returns>
        public async Task EnsureSeedData()
        {
            if (!_context.Roles.Any())
            {
                var roleAdministrador = new IdentityRole { Name = "Administrador" };
                await _roleManager.CreateAsync(roleAdministrador);

                var roleDirector = new IdentityRole { Name = "Director" };
                await _roleManager.CreateAsync(roleDirector);

                var roleSecretaria = new IdentityRole { Name = "Secretaria" };
                await _roleManager.CreateAsync(roleSecretaria);
            }
            
            if (!_context.Cargos.Any())
            {    
                var rolAdministrador = new Cargo()
                {
                    Id = 1,
                    Nombre = "Administrador"
                };
                _context.Cargos.Add(rolAdministrador);
                await _context.SaveChangesAsync();

                var rolDirector = new Cargo()
                {
                    Id = 2,
                    Nombre = "Director"
                };
                _context.Cargos.Add(rolDirector);
                await _context.SaveChangesAsync();

                var rolSecretaria = new Cargo()
                {
                    Id = 3,
                    Nombre = "Secretaria"
                };
                _context.Cargos.Add(rolSecretaria);
                await _context.SaveChangesAsync();
            }

            if (!_context.Colaboradores.Any())
            {
                var admin = new Colaborador()
                {
                    Nombres = "Eddy Wilmer",
                    ApellidoPaterno = "Canaza",
                    ApellidoMaterno = "Tito",
                    Dni = "71079379",
                    Email = "ecanaza",
                    Rol = _context.Cargos.Where(t => t.Id == 1).FirstOrDefault()
                };
                _context.Colaboradores.Add(admin);
                await _context.SaveChangesAsync();

                var adminSec = new Colaborador()
                {
                    Nombres = "Luis Fernando",
                    ApellidoPaterno = "Yana",
                    ApellidoMaterno = "Espinoza",
                    Dni = "12312312",
                    Email = "lyana",
                    Rol = _context.Cargos.Where(t => t.Id == 1).FirstOrDefault()
                };
                _context.Colaboradores.Add(adminSec);
                await _context.SaveChangesAsync();
            }

            if (await _userManager.FindByEmailAsync("ecanaza@trilce.edu.pe") == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "ecanaza",
                    Email = "ecanaza@trilce.edu.pe",
                    ColaboradorId = 1
                };

                await _userManager.CreateAsync(user, "123123123");
                await _userManager.AddToRoleAsync(user, "Administrador");
            }

            if (await _userManager.FindByEmailAsync("lyana@trilce.edu.pe") == null)
            {
                var userSec = new ApplicationUser()
                {
                    UserName = "lyana",
                    Email = "lyana@trilce.edu.pe",
                    ColaboradorId = 2
                };

                await _userManager.CreateAsync(userSec, "123123123");
                await _userManager.AddToRoleAsync(userSec, "Administrador");
            }

            if (!_context.Niveles.Any())
            {
                var primariaNivel = new Nivel()
                {
                    Id = 1,
                    Nombre = "Primaria",
                    NroHoras = 27
                };
                _context.Niveles.Add(primariaNivel);
                await _context.SaveChangesAsync();

                var secundariaNivel = new Nivel()
                {
                    Id = 2,
                    Nombre = "Secundaria",
                    NroHoras = 34
                };

                _context.Niveles.Add(secundariaNivel);
                await _context.SaveChangesAsync();

                string[] files = Directory.GetFiles(@"DbScripts", "*.sql");
                foreach (string file in files)
                {
                    _context.Database.ExecuteSqlCommand(File.ReadAllText(file));
                }
                _context.Database.ExecuteSqlCommand(File.ReadAllText(@"DataDemo/data.sql"));
                _context.Database.ExecuteSqlCommand(File.ReadAllText(@"DataDemo/TR_CreateNotasDeudas.sql"));
            }         
        }
    }
}
