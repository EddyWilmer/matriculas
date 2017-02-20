using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matriculas.Queries.Persistence.Repositories
{
    public class AppRepository : IAppRepository
    {
								private MatriculasContext _context;
								private ILogger<MatriculasRepositorys> _logger;
								private UserManager<ApplicationUser> _userManager;

								public AppRepository(MatriculasContext context, ILogger<MatriculasRepositorys> logger, UserManager<ApplicationUser> userManager)
								{
												_context = context;
												_logger = logger;
												_userManager = userManager;
												Alumnos = new AlumnoRepository(_context, _logger, _userManager);
								}

								public IAlumnoRepository Alumnos { get; private set; }

								public async Task<bool> Complete()
								{
												return (await _context.SaveChangesAsync()) > 0;
								}

								public void Dispose()
								{
												_context.Dispose();
								}
				}
}
