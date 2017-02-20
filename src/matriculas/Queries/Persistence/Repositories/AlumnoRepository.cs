using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Matriculas.Queries.Core.Repositories;

namespace Matriculas.Queries.Persistence.Repositories
{
				public class AlumnoRepository : IRepository<Alumno>, IAlumnoRepository
				{
								private MatriculasContext _context;
								private ILogger<MatriculasRepositorys> _logger;
								private UserManager<ApplicationUser> _userManager;

								public AlumnoRepository(MatriculasContext context, ILogger<MatriculasRepositorys> logger, UserManager<ApplicationUser> userManager)
								{
												_context = context;
												_logger = logger;
												_userManager = userManager;
								}

								public void Add(Alumno entity)
								{
												_context.Alumnos.Add(entity);
								}

								public void Delete(int id)
								{
												throw new NotImplementedException();
								}

								public Alumno Get(int id)
								{
												return _context.Alumnos
																.Include(t => t.Apoderado)
																.Where(t => t.Id == id)
																.FirstOrDefault();
								}

								public IEnumerable<Alumno> GetAll()
								{
												return _context.Alumnos
																.Include(t => t.Apoderado)
																.Where(t => t.Estado == "1")
																.ToList();
								}

								public bool HasDuplicatedDni(string dni)
								{
												var result = _context.Alumnos
																.Where(t => t.Dni == dni)
																.First();

												return result != null ? true : false;
								}

								public void Update(Alumno entity)
								{
												throw new NotImplementedException();
								}
				}
}
