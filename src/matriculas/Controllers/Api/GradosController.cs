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
    /// Clase que permite la interacción de las vistas con la entidad Grados.
    /// </summary>
    public class GradosController : Controller
    {
        private ILogger<GradosController> _logger;
        private IMatriculasRepositorys _repository;

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Constructor de la clase GradosController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public GradosController(IMatriculasRepositorys repository, ILogger<GradosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para recuperar la lista de Grados activos.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/grados")]
        public IActionResult GetGrados()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de grados.");
                var results = _repository.GetAllGrados();
                return Ok(Mapper.Map<IEnumerable<GradoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los grados: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para recuperar un Grado Específico a través de su Id.
        /// </summary>
        /// <param name="idGrado">Id del Grado.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/grados/{idGrado}")]
        public IActionResult GetGradoEspecifico(int idGrado)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del grado.");
                var result = _repository.GetGradoById(idGrado);
                return Ok(Mapper.Map<GradoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del grado: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los cursos que se dicta en un Grado específico.
        /// </summary>
        /// <param name="idGrado">Id del Grado.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/grados/cursos/{idGrado}")]
        public IActionResult GetCursosGradoEspecifico(int idGrado)
        {
            try
            {
                _logger.LogInformation("Recuperando los cursos del grado.");
                var result = _repository.GetCursosGradoById(idGrado);
                return Ok(Mapper.Map<IEnumerable<CursoViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del grado: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para agregar un Grado en la base de datos.
        /// </summary>
        /// <param name="thisGrado">Grado.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/grados/crear")]
        public async Task<IActionResult> PostCrearGrado([FromBody] GradoViewModel thisGrado)
        {
            _logger.LogInformation("Agregando el grado.");

            if (!_repository.IsNombreValido(Mapper.Map<Grado>(thisGrado)))
                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

            if (ModelState.IsValid)
            {
                var newGrado = Mapper.Map<Grado>(thisGrado);

                _repository.AddGrado(newGrado);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/grados/{thisGrado.Id}", Mapper.Map<GradoViewModel>(newGrado));
                }
            }

            _logger.LogError("No se pudo agregar el grado.");
            return BadRequest(ModelState);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para actualizar un Grado en la base de datos.
        /// </summary>
        /// <param name="thisGrado">Grado con los datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/grados/editar")]
        public async Task<IActionResult> PostEditarGrado([FromBody] GradoViewModel thisGrado)
        {
            _logger.LogInformation("Actualizando los datos del grado.");

            if (!_repository.IsNombreValido(Mapper.Map<Grado>(thisGrado)))
                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

            if (ModelState.IsValid)
            {
                var gradoToUpdate = Mapper.Map<Grado>(thisGrado);

                var updatedGrado = _repository.UpdateGrado(gradoToUpdate);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/grados/{updatedGrado.Id}", Mapper.Map<GradoViewModel>(updatedGrado));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del grado.");
            return BadRequest(ModelState);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para eliminar lógicamente un Grado en la base de datos.
        /// </summary>
        /// <param name="thisGrado">Grado.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/grados/eliminar")]
        public async Task<IActionResult> PostEliminarGrado([FromBody] GradoViewModel thisGrado)
        {
            _logger.LogInformation("Eliminando el grado.");

            ModelState.Clear();

            if (!_repository.IsEliminable(Mapper.Map<Grado>(thisGrado))) 
                ModelState.AddModelError("eliminacionMessageValidation", "Este grado tiene secciones o cursos asociadas.");

            if (ModelState.IsValid)
            {
                var deletedGrado = Mapper.Map<Grado>(_repository.GetGradoById(thisGrado.Id));

                _repository.DeleteGrado(deletedGrado);
                if (await _repository.SaveChangesAsync())
                {
                    return Ok("Se eliminó el colaborador correctamente.");
                }
            }

            _logger.LogError("No se pudo eliminar el grado.");
            return BadRequest(ModelState);
        }
    }
}
