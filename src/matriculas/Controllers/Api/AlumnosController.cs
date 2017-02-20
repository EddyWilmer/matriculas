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
																_logger.LogInformation("Recuperando la lista de alumnos");
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
																_logger.LogInformation("Recuperando la información del alumno");
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
												_logger.LogInformation("Agregando al alumno");

												var alumno = Mapper.Map<Alumno>(alumnoDetails);

												//if (!_repository.IsDniValido(alumno))
												//				ModelState.AddModelError("otros", "El Dni del alumno ya está registrado, verifique duplicidad.");

												if (ModelState.IsValid)
												{
																_repository.Alumnos.Add(alumno);
																if(await _repository.Complete())
																{
																				return Created($"api/alumno/{alumno.Id}", Mapper.Map<AlumnoViewModel>(alumno));
																}
												}

												_logger.LogError("No se pudo agregar al alumno");
												return BadRequest(ModelState);
								}

								///// <author>Julissa Zaida Huaman Hilari</author>
								///// <summary>
								///// Método para actualizar un Alumno en la base de datos.
								///// </summary>
								///// <param name="alumnoDetails">Alumno con datos actualizados.</param>
								///// <returns>Acción con la respuesta.</returns>
								//[HttpPut()]
								//public async Task<IActionResult> PutAlumno([FromBody] AlumnoViewModel alumnoDetails)
								//{
								//				_logger.LogInformation("Actualizando al alumno");

								//				var alumno = Mapper.Map<Alumno>(alumnoDetails);

								//				if (!_repository.IsDniValido(alumno))
								//								ModelState.AddModelError("otros", "El Dni del alumno ya está registrado, verifique duplicidad.");

								//				if (ModelState.IsValid)
								//				{
								//								_repository.UpdateAlumno(alumno);
								//								_repository.UpdateApoderado(alumno.Apoderado);
								//								if (await _repository.SaveChangesAsync())
								//								{
								//												return Created($"api/alumnos/{alumno.Id}", Mapper.Map<AlumnoViewModel>(alumno));
								//								}
								//				}

								//				_logger.LogError("No se pudo actualizar los datos del alumno.");
								//				return BadRequest(ModelState);
								//}

								///// <author>Julissa Zaida Huaman Hilari</author>
								///// <summary>
								///// Método para eliminar lógicamente un Alumno en la base de datos.
								///// </summary>
								///// <param name="id">Alumno.</param>
								///// <returns>Acción con la respuesta</returns>
								//[HttpPost("{id}")]
								//public async Task<IActionResult> PostEliminarAlumno(int id)
								//{
								//				_logger.LogInformation("Eliminando al alumno.");
								//				var result = _repository.GetAlumnoById(id);

								//				var alumno = Mapper.Map<Alumno>(result);

								//				_repository.DeleteAlumno(alumno);
								//				if (await _repository.SaveChangesAsync())
								//				{
								//								return Created($"api/alumnos/{alumno.Id}", Mapper.Map<AlumnoViewModel>(alumno));
								//				}

								//				_logger.LogError("No se pudo eliminar al alumno.");
								//				return BadRequest("No se pudo eliminar este colaborador.");
								//}


								///// <author>Eddy Wilmer Canaza Tito</author>
								///// <summary>
								///// Método para recuperar un Alumno específico a través del Dni.
								///// </summary>
								///// <param name="thisAlumno">Alumno.</param>
								///// <returns>Acción con la respuesta.</returns>
								//[HttpGet("api/alumnos/dni/{dni}")]
								//public IActionResult GetAlumnoEspecificoByDni(AlumnoViewModel thisAlumno)
								//{
								//        try
								//        {
								//        _logger.LogInformation("Recuperando la información del alumno");
								//        var result = _repository.GetAlumnoByDni(thisAlumno.Dni);
								//            return Ok(Mapper.Map<AlumnoViewModel>(result));
								//        }
								//        catch (Exception ex)
								//        {
								//            _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
								//            return BadRequest(ModelState);
								//        }    
								//}

								///// <author>Eddy Wilmer Canaza Tito</author>
								///// <summary>
								///// Método para recuperar la última matrícula de un Alumno específico.
								///// </summary>
								///// <param name="idAlumno">Id del Alumno.</param>
								///// <returns>Acción con la respuesta.</returns>
								//[HttpGet("api/alumnos/matricula/{idAlumno}")]
								//public IActionResult GetMatriculaAlumnoEspecifico(int idAlumno)
								//{
								//    try
								//    {
								//        _logger.LogInformation("Recuperando la información del alumno");
								//        var result = _repository.GetLastMatricula(idAlumno);
								//        return Ok(Mapper.Map<MatriculaViewModel>(result));
								//    }
								//    catch (Exception ex)
								//    {
								//        _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
								//        return BadRequest(ModelState);
								//    }
								//}

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
