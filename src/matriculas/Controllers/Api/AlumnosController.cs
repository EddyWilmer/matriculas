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
	public class AlumnosController : Controller
	{
		private ILogger<AlumnosController> _logger;
		private IAppRepository _repository;
		private UserManager<ApplicationUser> _userManager;

		public AlumnosController(IAppRepository repository, ILogger<AlumnosController> logger, UserManager<ApplicationUser> userManager)
		{
			_repository = repository;
			_logger = logger;
			_userManager = userManager;
		}

		[HttpGet()]
		public IActionResult GetAllAlumnos()
		{
			try
			{
				_logger.LogInformation("Recuperando la lista de alumnos.");
				var result = _repository.Alumnos.GetAll();
				return Ok(Mapper.Map<IEnumerable<AlumnoViewModel>>(result));
			}
			catch (Exception ex)
			{
				_logger.LogError($"No se pudo recuperar los alumnos: {ex}");
				return BadRequest("No se pudo recuperar la información.");
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetAlumno(int id)
		{
			try
			{
				_logger.LogInformation("Recuperando la información del alumno.");
				var result = _repository.Alumnos.Get(id);
				return Ok(Mapper.Map<AlumnoViewModel>(result));
			}
			catch (Exception ex)
			{
				_logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
				return BadRequest("No se pudo recuperar la información del alumno.");
			}
		}

		[HttpPost()]
		public async Task<IActionResult> PostAlumno([FromBody] AlumnoViewModel alumnoDetails)
		{
			_logger.LogInformation("Agregando alumno.");

			var alumno = Mapper.Map<Alumno>(alumnoDetails);

            if (!_repository.Alumnos.HasDniUnique(alumno))
                ModelState.AddModelError("Alumno.Dni", "Dni no disponible.");

            if (ModelState.IsValid)
			{
				_repository.Alumnos.Add(alumno);
				if(await _repository.Complete())
				{
					return Created($"api/alumno/{alumno.Id}", Mapper.Map<AlumnoViewModel>(alumno));
				}
			}

			_logger.LogError("No se pudo agregar el alumno.");
			return BadRequest(ModelState);
        }

		[HttpPut()]
		public async Task<IActionResult> PutAlumno([FromBody] AlumnoViewModel alumnoDetails)
		{
			_logger.LogInformation("Actualizando alumno.");

			var alumno = Mapper.Map<Alumno>(alumnoDetails);

            if (!_repository.Alumnos.HasDniUnique(alumno))
                ModelState.AddModelError("Alumno.Dni", "Dni no disponible.");
            
            if (ModelState.IsValid)
			{
				_repository.Alumnos.Update(alumno);
				if (await _repository.Complete())
				{
					return Created($"api/alumnos/{alumno.Id}", Mapper.Map<AlumnoViewModel>(alumno));
				}
			}

			_logger.LogError("No se pudo actualizar los datos del alumno.");
			return BadRequest(ModelState);
		}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumno(int id)
        {
            _logger.LogInformation("Eliminando al alumno.");

            _repository.Alumnos.Delete(id);
            if (await _repository.Complete())
            {
                return Created($"api/alumnos/{id}", Mapper.Map<AlumnoViewModel>(_repository.Alumnos.Get(id)));
            }

            _logger.LogError("No se pudo eliminar al alumno.");
            return BadRequest("No se pudo eliminar este alumno.");
        }





        [HttpGet("dni/{dni}")]
        public IActionResult GetAlumnoByDni(string dni)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.Alumnos.GetByDni(dni);
                return Ok(Mapper.Map<AlumnoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}/grado")]
        public IActionResult GetGrado(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.Alumnos.GetGrado(id);
                return Ok(Mapper.Map<GradoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        [HttpGet("{id}/nextGrado")]
        public IActionResult GetNextGrado(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.Alumnos.GetNextGrado(id);
                return Ok(Mapper.Map<GradoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }
    }
}
