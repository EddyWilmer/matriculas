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
    /// Clase que permite la interacción de las vistas con la entidad Secciones.
    /// </summary>
    public class SeccionesController : Controller
    {
        private ILogger<SeccionesController> _logger;
        private IMatriculasRepository _repository;
  
        /// <summary>
        /// Constructor de la clase SeccionesController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public SeccionesController(IMatriculasRepository repository, ILogger<SeccionesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Método para recuperar la lista de Secciones activas.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/secciones")]
        public IActionResult GetSecciones()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de secciones.");
                var results = _repository.GetAllSecciones();
                return Ok(Mapper.Map<IEnumerable<SeccionViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los secciones: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <summary>
        /// Método para recuperar un Sección específica a través de un Id.
        /// </summary>
        /// <param name="idSeccion">Id de la Sección.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/secciones/{idSeccion}")]
        public IActionResult GetSeccionEspecifica(int idSeccion)
        {
            try
            {
                _logger.LogInformation("Recuperando la información de la sección.");
                var result = _repository.GetSeccionById(idSeccion);
                return Ok(Mapper.Map<SeccionViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información de la sección: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <summary>
        /// Método para agregar una Sección en la base de datos.
        /// </summary>
        /// <param name="thisSeccion">Sección.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/secciones/crear")]
        public async Task<IActionResult> PostCrearSeccion([FromBody] SeccionViewModel thisSeccion)
        {
            _logger.LogInformation("Agregando la sección.");

            if (!_repository.IsNombreValido(Mapper.Map<Seccion>(thisSeccion)))
                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

            if (ModelState.IsValid)
            {
                var newSeccion = Mapper.Map<Seccion>(thisSeccion);

                _repository.AddSeccion(newSeccion);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/secciones/{thisSeccion.Id}", Mapper.Map<SeccionViewModel>(newSeccion));
                }
            }

            _logger.LogError("No se pudo agregar la sección.");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Método para actualizar una Sección en la base de datos.
        /// </summary>
        /// <param name="thisSeccion">Sección con los datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/secciones/editar")]
        public async Task<IActionResult> PostEditarSeccion([FromBody] SeccionViewModel thisSeccion)
        {
            _logger.LogInformation("Actualizando los datos de la sección.");

            if (!_repository.IsNombreValido(Mapper.Map<Seccion>(thisSeccion)))
                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

            if (ModelState.IsValid)
            {
                var seccionToUpdate = Mapper.Map<Seccion>(thisSeccion);

                var updatedSeccion = _repository.UpdateSeccion(seccionToUpdate);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/secciones/{updatedSeccion.Id}", Mapper.Map<SeccionViewModel>(updatedSeccion));
                }
            }

            _logger.LogError("No se pudo actualizar los datos de la sección.");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Método para eliminar lógicamente una Sección en la base de datos.
        /// </summary>
        /// <param name="thisSeccion">Sección.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/secciones/eliminar")]
        public async Task<IActionResult> PostEliminarSeccion([FromBody] SeccionViewModel thisSeccion)
        {
            _logger.LogInformation("Eliminando la sección.");

            var result = _repository.GetSeccionById(thisSeccion.Id);

            var deletedSeccion = Mapper.Map<Seccion>(result);

            _repository.DeleteSeccion(deletedSeccion);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("Se eliminó el colaborador correctamente.");
            }

            _logger.LogError("No se pudo eliminar la sección.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }
    }
}
