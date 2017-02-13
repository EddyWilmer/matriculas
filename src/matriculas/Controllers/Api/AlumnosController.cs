using AutoMapper;
using Matriculas.Models;
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
    /// <author>Julissa Zaida Huaman Hilari</author>
    /// <summary>
    /// Clase que permite la interacción de las vistas con la entidad Alumno.
    /// </summary>
    public class AlumnosController : Controller
    {
        private ILogger<AlumnosController> _logger;
        private IMatriculasRepository _repository;
        private UserManager<ApplicationUser> _userManager;

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Constructor de la clase AlumnosController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        public AlumnosController(IMatriculasRepository repository, ILogger<AlumnosController> logger, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para recuperar la lista de Alumnos activos.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos")]
        public IActionResult GetAlumnos()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de alumnos");
                var result = _repository.GetAllAlumnos();
                return Ok(Mapper.Map<IEnumerable<AlumnoViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los alumnos: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }          
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para recuperar un Alumno específico a través del Id.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/{idAlumno}")]
        public IActionResult GetAlumnoEspecifico(int idAlumno)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.GetAlumnoById(idAlumno);
                return Ok(Mapper.Map<AlumnoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest("No se pudo recuperar la información del alumno.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar un Alumno específico a través del Dni.
        /// </summary>
        /// <param name="thisAlumno">Alumno.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/dni/{dni}")]
        public IActionResult GetAlumnoEspecificoByDni(AlumnoViewModel thisAlumno)
        {
                try
                {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.GetAlumnoByDni(thisAlumno.Dni);
                    return Ok(Mapper.Map<AlumnoViewModel>(result));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                    return BadRequest(ModelState);
                }    
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar la última matrícula de un Alumno específico.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/matricula/{idAlumno}")]
        public IActionResult GetMatriculaAlumnoEspecifico(int idAlumno)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del alumno");
                var result = _repository.GetLastMatricula(idAlumno);
                return Ok(Mapper.Map<MatriculaViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Método para recuperar el siguiente Grado que debe cursar un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/nextGrado/{idAlumno}")]
        public IActionResult GetNextGradoAlumnoEspecifico(int idAlumno)
        {
            try
            {
                _logger.LogInformation("Recuperando el grado siguiente del alumno");
                var result = _repository.GetNextGrado(idAlumno);
                return Ok(Mapper.Map<GradoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar las notas de la última matrícula del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/notas/{idAlumno}")]
        public IActionResult GetNotasAlumnoEspecifico(int idAlumno)
        {
            try
            {
                _logger.LogInformation("Recuperando las notas del alumno");
                var result = _repository.GetNotasLastMatricula(idAlumno);
                return Ok(Mapper.Map<IEnumerable<NotaViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar las deudas de la última matrícula del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/deudas/{idAlumno}")]
        public IActionResult GetDeudasAlumnoEspecifico(int idAlumno)
        {
            try
            {
                _logger.LogInformation("Recuperando las deudas del alumno");
                var result = _repository.GetDeudasLastMatricula(idAlumno);
                return Ok(Mapper.Map<IEnumerable<DeudaViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para procesar la matrícula de un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/alumnos/matriculas/{idAlumno}")]
        public IActionResult MatricularAlumno(int idAlumno)
        {
            try
            {
                _logger.LogInformation("Matriculando al alumno");
                var colaboradorId = _userManager.FindByNameAsync(User.Identity.Name).Result.ColaboradorId;
                var result = _repository.RegistrarMatricula(idAlumno, colaboradorId);
                switch (result)
                {
                    case 0:
                        ModelState.AddModelError("CronogramaMessage", "No hay un cronograma de matrículas establecido.");
                        return BadRequest(ModelState);
                    case 1:
                        return Ok("Se registro la matrícula correctamente.");
                    case 2:
                        ModelState.AddModelError("SeccionesDisponiblesMessage", "No hay secciones disponibles.");
                        return BadRequest(ModelState);
                    case 3:
                        ModelState.AddModelError("AlumnoMatriculadoMessage", "Este alumno ya fue matriculado.");
                        return BadRequest(ModelState);
                }
                ModelState.AddModelError("ErrorMessage", "No se pudo registrar la matrícula.");
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del alumno: {ex}");
                return BadRequest(ModelState);
            }
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para agregar un Alumno en la base de datos.
        /// </summary>
        /// <param name="thisAlumno">Alumno</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/alumnos/crear")]
        public async Task<IActionResult> PostCrearAlumno([FromBody] AlumnoViewModel thisAlumno)
        {
            _logger.LogInformation("Agregando al alumno");
            if (!_repository.IsDniValido(Mapper.Map<Alumno>(thisAlumno)))
                ModelState.AddModelError("dniMessageValidation", "Este DNI ya fue registrado.");

            var thisApoderado = Mapper.Map<ApoderadoViewModel>(thisAlumno.Apoderado);
            var validationContext = new ValidationContext(thisApoderado, null, null);
            var validationResults = new List<ValidationResult>();
            var ModelStateApoderado = Validator.TryValidateObject(thisApoderado, validationContext, validationResults, true);

            if (ModelState.IsValid && validationResults.Count == 0)
            {
                var newAlumno = Mapper.Map<Alumno>(thisAlumno);

                _repository.AddAlumno(newAlumno);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/alumno/{thisAlumno.Id}", Mapper.Map<AlumnoViewModel>(newAlumno));
                }
            }

            _logger.LogError("No se pudo agregar al alumno");
            return BadRequest(ModelState);
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para actualizar un Alumno en la base de datos.
        /// </summary>
        /// <param name="thisAlumno">Alumno con datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/alumnos/editar")]
        public async Task<IActionResult> PostEditarAlumno([FromBody] AlumnoViewModel thisAlumno)
        {
            _logger.LogInformation("Actualizando al alumno");
            if (!_repository.IsDniValido(Mapper.Map<Alumno>(thisAlumno)))
                ModelState.AddModelError("dniMessageValidation", "Este DNI ya fue registrado.");

            var thisApoderado = Mapper.Map<ApoderadoViewModel>(thisAlumno.Apoderado);
            var validationContext = new ValidationContext(thisApoderado, null, null);
            var validationResults = new List<ValidationResult>();
            var ModelStateApoderado = Validator.TryValidateObject(thisApoderado, validationContext, validationResults, true);

            if (ModelState.IsValid && validationResults.Count == 0)
            {
                var alumnoToUpdate = Mapper.Map<Alumno>(thisAlumno);

                var updatedAlumno = _repository.UpdateAlumno(alumnoToUpdate);
                var updatedApoderado = _repository.UpdateApoderado(alumnoToUpdate.Apoderado);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/alumnos/{updatedAlumno.Id}", Mapper.Map<AlumnoViewModel>(updatedAlumno));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del alumno.");
            return BadRequest(ModelState);
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para eliminar lógicamente un Alumno en la base de datos.
        /// </summary>
        /// <param name="thisAlumno">Alumno.</param>
        /// <returns>Acción con la respuesta</returns>
        [HttpPost("api/alumnos/eliminar")]
        public async Task<IActionResult> PostEliminarAlumno([FromBody] AlumnoViewModel thisAlumno)
        {
            _logger.LogInformation("Eliminando al alumno.");
            var result = _repository.GetAlumnoById(thisAlumno.Id);

            var alumnoToDelete = Mapper.Map<Alumno>(result);

            _repository.DeleteAlumno(alumnoToDelete);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("Se eliminó el colaborador correctamente.");
            }

            _logger.LogError("No se pudo eliminar al alumno.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }
    }
}
