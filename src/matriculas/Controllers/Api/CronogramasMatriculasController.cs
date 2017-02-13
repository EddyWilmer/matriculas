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
    /// <author>Luis Fernando Yana Espinoza</author>
    /// <summary>
    /// Clase que permite la interacción de las vistas con la entidad CronogramasMatriculas (Cronogramas de Matrícula).
    /// </summary>
    public class CronogramasMatriculasController : Controller
    {
        private ILogger<CronogramasMatriculasController> _logger;
        private IMatriculasRepository _repository;

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Constructor de la clase CronogramasMatriculasController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public CronogramasMatriculasController(IMatriculasRepository repository, ILogger<CronogramasMatriculasController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para recuperar la lista de Cronogramas de Matrícula.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cronogramasMatriculas")]
        public IActionResult GetCronogramasMatriculas()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de cronogramas de matrícula.");
                var results = _repository.GetAllCronogramasMatriculas();
                return Ok(Mapper.Map<IEnumerable<CronogramaMatriculaViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cronogramas de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para recuperar los Cronogramas de Matrícula de un Año Académico específico.
        /// </summary>
        /// <param name="idAnioAcademico">Id del Año Académico.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cronogramasMatriculas/anioAcademico/{idAnioAcademico}")]
        public IActionResult GetCronogramasMatriculasByAnioAcademico(int idAnioAcademico)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del año académico.");
                var results = _repository.GetAllCronogramasMatriculasByAnioAcademicoId(idAnioAcademico).Where(t => t.Estado == "1");
                return Ok(Mapper.Map<IEnumerable<CronogramaMatriculaViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cronogramas de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para recuperar un Cronograma Matrícula específico.
        /// </summary>
        /// <param name="idAnioAcademico">Id del Año Académico.</param>
        /// <param name="nombre">Nombre del Cronograma de Matrícula.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cronogramasMatriculas/{idAnioAcademico}/{nombre}")]
        public IActionResult GetCronogramaMatriculaEspecifico(int idAnioAcademico, string nombre)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del cronograma de matrícula.");
                var result = _repository.GetCronogramaMatriculaById(idAnioAcademico, nombre);
                return Ok(Mapper.Map<CronogramaMatriculaViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del cronograma de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para agregar un Cronograma de Matrícula en la base de datos.
        /// </summary>
        /// <param name="thisCronogramaMatricula">Cronograma de Matrícula.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cronogramasMatriculas/crear")]
        public async Task<IActionResult> PostCrearCronogramaMatricula([FromBody] CronogramaMatriculaViewModel thisCronogramaMatricula)
        {
            _logger.LogInformation("Agregando el cronograma de matrícula.");

            if (!_repository.IsNombreValido(Mapper.Map<CronogramaMatricula>(thisCronogramaMatricula)))
                ModelState.AddModelError("nombreMessageValidation", "Este Nombre ya fue registrado.");

            if (!_repository.IsFechasValidas(Mapper.Map<CronogramaMatricula>(thisCronogramaMatricula)))
                ModelState.AddModelError("fechasMessageValidation", "Las fechas no son válidas.");

            if (ModelState.IsValid)
            {
                var newCronogramaMatricula = Mapper.Map<CronogramaMatricula>(thisCronogramaMatricula);

                _repository.AddCronogramaMatricula(newCronogramaMatricula);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/cronogramasMatriculas/{thisCronogramaMatricula.AnioAcademicoId}", Mapper.Map<CronogramaMatriculaViewModel>(newCronogramaMatricula));
                }
            }

            _logger.LogError("No se pudo agregar el cronograma de matrícula.");
            return BadRequest(ModelState);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para actualizar un Cronograma de Matrícula en la base de datos.
        /// </summary>
        /// <param name="thisCronogramaMatricula">Cronograma de Matrícula con los datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cronogramasMatriculas/editar")]
        public async Task<IActionResult> PostEditarCronogramaMatricula([FromBody] CronogramaMatriculaViewModel thisCronogramaMatricula)
        {
            _logger.LogInformation("Actualizando la información del cronograma de matrícula.");

            if (!_repository.IsNombreValido(Mapper.Map<CronogramaMatricula>(thisCronogramaMatricula)))
                ModelState.AddModelError("nombreMessageValidation", "Este Nombre ya fue registrado.");

            if (!_repository.IsFechasValidas(Mapper.Map<CronogramaMatricula>(thisCronogramaMatricula)))
                ModelState.AddModelError("fechasMessageValidation", "Las fechas no son válidas.");

            if (ModelState.IsValid)
            {
                var cronogramaMatriculaToUpdate = Mapper.Map<CronogramaMatricula>(thisCronogramaMatricula);

                var updatedCronogramaMatricula = _repository.UpdateCronogramaMatricula(cronogramaMatriculaToUpdate);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/cronogramasMatriculas/{updatedCronogramaMatricula.AnioAcademicoId}", Mapper.Map<CronogramaMatriculaViewModel>(updatedCronogramaMatricula));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del cronograma de matrícula.");
            return BadRequest(ModelState);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para eliminar lógicamente un Cronograma de Matrícula en la base de datos.
        /// </summary>
        /// <param name="thisCronogramaMatricula">Cronograma de Matrícula.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cronogramasMatriculas/eliminar")]
        public async Task<IActionResult> PostEliminarCronogramaMatricula([FromBody] CronogramaMatriculaViewModel thisCronogramaMatricula)
        {
            _logger.LogInformation("Eliminando el cronograma de matrícula.");
            
            ModelState.Clear();

            if (ModelState.IsValid)
            {
                var anioAcademicoToDelete = Mapper.Map<CronogramaMatricula>(_repository.GetCronogramaMatriculaById(thisCronogramaMatricula.AnioAcademicoId, thisCronogramaMatricula.Nombre));

                _repository.DeleteCronogramaMatricula(anioAcademicoToDelete);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Se eliminó el colaborador correctamente.");
                }
            }

            _logger.LogError("No se pudo eliminar el cronograma de matrícula.");
            return BadRequest(ModelState);
        }
    }
}
