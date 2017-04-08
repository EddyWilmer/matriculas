using AutoMapper;
using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
using Matriculas.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Matriculas.Controllers.Api
{
	[Route("api/v2/[controller]")]
	public class MatriculasController : Controller
	{
		private ILogger<MatriculasController> _logger;
		private IAppRepository _repository;
        private UserManager<ApplicationUser> _userManager;

        public MatriculasController(IAppRepository repository, ILogger<MatriculasController> logger, UserManager<ApplicationUser> userManager)
		{
			_repository = repository;
			_logger = logger;
            _userManager = userManager;
		}

		[HttpGet()]
		public IActionResult GetAllMatriculas()
		{
			try
			{
				_logger.LogInformation("Recuperando la lista de alumnos.");
                var result = _repository.Matriculas.GetAll();
				return Ok(Mapper.Map<IEnumerable<MatriculaViewModel>>(result));
			}
			catch (Exception ex)
			{
				_logger.LogError($"No se pudo recuperar los alumnos: {ex}");
				return BadRequest("No se pudo recuperar la información.");
			}
		}





        [HttpPost()]
        public async Task<IActionResult> PostMatricula([FromBody] MatriculaViewModel matriculaDetails)
        {
            _logger.LogInformation("Agregando matrícula.");

            var matricula = Mapper.Map<Matricula>(matriculaDetails);
            matricula.RegistradorId = _userManager.FindByNameAsync(User.Identity.Name).Result.ColaboradorId;

            if (_repository.AniosAcademicos.GetAnioAcademico(DateTime.Now.Year) == null)
                ModelState.AddModelError("noDisponible", "No están disponibles las matrículas.");

            if (_repository.Alumnos.IsMatriculado(_repository.Alumnos.Get(matricula.AlumnoId)))
                ModelState.AddModelError("yaMatriculado", "Ya está matriculado en el periodo.");

            if (ModelState.IsValid)
            {
                _repository.Matriculas.Add(matricula);
                if (await _repository.Complete())
                {
                    return Created($"api/alumno/{matricula.Id}", Mapper.Map<MatriculaViewModel>(matricula));
                }
            }

            _logger.LogError("No se pudo agregar el alumno.");
            return BadRequest(ModelState);
        }
    }
}
