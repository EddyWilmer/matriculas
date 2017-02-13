using AutoMapper;
using Matriculas.Models;
using Matriculas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Matriculas.Controllers.Api
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que permite la interacción de las vistas con la entidad Profesores.
    /// </summary>
    public class ProfesoresController : Controller
    {
        private ILogger<ProfesoresController> _logger;
        private IMatriculasRepository _repository;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase ProfesoresController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public ProfesoresController(IMatriculasRepository repository, ILogger<ProfesoresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar la lista de Profesores.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/profesores")]
        public IActionResult GetProfesores()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de profesores.");
                var results = _repository.GetAllProfesores();
                return Ok(Mapper.Map<IEnumerable<ProfesorViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los profesores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }          
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar un Profesor específico a través del Id.
        /// </summary>
        /// <param name="idProfesor">Id del Profesor.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/profesores/{idProfesor}")]
        public IActionResult GetProfesorEspecifico(int idProfesor)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del profesor.");
                var result = _repository.GetProfesorById(idProfesor);              
                return Ok(Mapper.Map<ProfesorViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del profesor: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un Profesor en la base de datos.
        /// </summary>
        /// <param name="thisProfesor">Profesor.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/profesores/crear")]
        public async Task<IActionResult> PostCrearProfesor([FromBody] ProfesorViewModel thisProfesor)
        {
            _logger.LogInformation("Agregando el profesor.");

            if (!_repository.IsDniValido(Mapper.Map<Profesor>(thisProfesor)))
                ModelState.AddModelError("dniMessageValidation", "Este DNI ya fue registrado.");

            if (ModelState.IsValid)
            {
                var newProfesorCursos = thisProfesor.Cursos;
                var newProfesor = Mapper.Map<Profesor>(thisProfesor);

                _repository.AddProfesor(newProfesor, newProfesorCursos);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/profesores/{thisProfesor.Id}", Mapper.Map<ProfesorViewModel>(newProfesor));
                }
            }

            _logger.LogError("No se pudo agregar el profesor.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un Profesor en la base de datos.
        /// </summary>
        /// <param name="thisProfesor">Profesor con los datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/profesores/editar")]
        public async Task<IActionResult> PostEditarProfesor([FromBody] ProfesorViewModel thisProfesor)
        {
            _logger.LogInformation("Actualizando los datos del profesor.");

            if (!_repository.IsDniValido(Mapper.Map<Profesor>(thisProfesor)))
                ModelState.AddModelError("dniMessageValidation", "Este DNI ya fue registrado.");

            if (ModelState.IsValid)
            {
                var profesorToUpdateCursos = thisProfesor.Cursos;
                var profesorToUpdate = Mapper.Map<Profesor>(thisProfesor);

                var updatedProfesor = _repository.UpdateProfesor(profesorToUpdate, profesorToUpdateCursos);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/profesores/{updatedProfesor.Id}", Mapper.Map<ProfesorViewModel>(updatedProfesor));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del profesor.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Profesor en la base de datos.
        /// </summary>
        /// <param name="thisProfesor">Profesor.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/profesores/eliminar")]
        public async Task<IActionResult> PostEliminarProfesor([FromBody] ProfesorViewModel thisProfesor)
        {
            _logger.LogInformation("Eliminando el profesor.");

            var result = _repository.GetProfesorById(thisProfesor.Id);

            var profesorToDelete = Mapper.Map<Profesor>(result);

            _repository.DeleteProfesor(profesorToDelete);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("Se eliminó el profesor correctamente.");
            }

            _logger.LogError("No se pudo eliminar el profesor.");
            return BadRequest("No se pudo eliminar este profesor.");
        }
    }
}
