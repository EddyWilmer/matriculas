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

        [HttpGet("matricula/{id}")]
        public IActionResult GetLastMatricula(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.Alumnos.GetLastMatricula(id);
                return Ok(Mapper.Map<MatriculaViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        ///// <summary>
        ///// Método para recuperar el siguiente Grado que debe cursar un Alumno.
        ///// </summary>
        ///// <param name="idAlumno">Id del Alumno.</param>
        ///// <returns>Acción con la respuesta.</returns>
        //[HttpGet("api/alumnos/nextGrado/{idAlumno}")]
        //public IActionResult GetNextGradoAlumnoEspecifico(int idAlumno)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Recuperando el grado siguiente del alumno");
        //        var result = _repository.GetNextGrado(idAlumno);
        //        return Ok(Mapper.Map<GradoViewModel>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
        //        return BadRequest(ModelState);
        //    }
        //}

        ///// <author>Eddy Wilmer Canaza Tito</author>
        ///// <summary>
        ///// Método para recuperar las notas de la última matrícula del Alumno.
        ///// </summary>
        ///// <param name="idAlumno">Id del Alumno</param>
        ///// <returns>Acción con la respuesta.</returns>
        //[HttpGet("api/alumnos/notas/{idAlumno}")]
        //public IActionResult GetNotasAlumnoEspecifico(int idAlumno)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Recuperando las notas del alumno");
        //        var result = _repository.GetNotasLastMatricula(idAlumno);
        //        return Ok(Mapper.Map<IEnumerable<NotaViewModel>>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
        //        return BadRequest(ModelState);
        //    }
        //}

        ///// <author>Eddy Wilmer Canaza Tito</author>
        ///// <summary>
        ///// Método para recuperar las deudas de la última matrícula del Alumno.
        ///// </summary>
        ///// <param name="idAlumno">Id del Alumno</param>
        ///// <returns>Acción con la respuesta.</returns>
        //[HttpGet("api/alumnos/deudas/{idAlumno}")]
        //public IActionResult GetDeudasAlumnoEspecifico(int idAlumno)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Recuperando las deudas del alumno");
        //        var result = _repository.GetDeudasLastMatricula(idAlumno);
        //        return Ok(Mapper.Map<IEnumerable<DeudaViewModel>>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
        //        return BadRequest(ModelState);
        //    }
        //}

        ///// <author>Eddy Wilmer Canaza Tito</author>
        ///// <summary>
        ///// Método para procesar la matrícula de un Alumno.
        ///// </summary>
        ///// <param name="idAlumno">Id del Alumno</param>
        ///// <returns>Acción con la respuesta.</returns>
        //[HttpGet("api/alumnos/matriculas/{idAlumno}")]
        //public IActionResult MatricularAlumno(int idAlumno)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Matriculando al alumno");
        //        var colaboradorId = _userManager.FindByNameAsync(User.Identity.Name).Result.ColaboradorId;
        //        var result = _repository.RegistrarMatricula(idAlumno, colaboradorId);
        //        switch (result)
        //        {
        //            case 0:
        //                ModelState.AddModelError("CronogramaMessage", "No hay un cronograma de matrículas establecido.");
        //                return BadRequest(ModelState);
        //            case 1:
        //                return Ok("Se registro la matrícula correctamente.");
        //            case 2:
        //                ModelState.AddModelError("SeccionesDisponiblesMessage", "No hay secciones disponibles.");
        //                return BadRequest(ModelState);
        //            case 3:
        //                ModelState.AddModelError("AlumnoMatriculadoMessage", "Este alumno ya fue matriculado.");
        //                return BadRequest(ModelState);
        //        }
        //        ModelState.AddModelError("ErrorMessage", "No se pudo registrar la matrícula.");
        //        return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
        //        return BadRequest(ModelState);
        //    }
        //}
    }
}
