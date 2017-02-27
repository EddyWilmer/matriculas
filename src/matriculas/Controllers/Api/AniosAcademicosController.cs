//using AutoMapper;
//using Matriculas.Models;
//using Matriculas.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace Matriculas.Controllers.Api
//{

//    /// <author>Eddy Wilmer Canaza Tito</author>
//    /// <summary>
//    /// Clase que permite la interacción de las vistas con la entidad AniosAcademicos (Años Académicos).
//    /// </summary>
//    public class AniosAcademicosController : Controller
//    {
//        private ILogger<AniosAcademicosController> _logger;
//        private IMatriculasRepositorys _repository;

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Constructor de la clase AniosAcademicosController.
//        /// </summary>
//        /// <param name="repository">Instancia del respositorio.</param>
//        /// <param name="logger">Administrador de logging.</param>
//        public AniosAcademicosController(IMatriculasRepositorys repository, ILogger<AniosAcademicosController> logger)
//        {
//            _repository = repository;
//            _logger = logger;
//        }

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Método para recuperar los Años Académicos activos.
//        /// </summary>
//        /// <returns>Acción con la respuesta.</returns>
//        [HttpGet("api/aniosAcademicos")]
//        public IActionResult GetAniosAcademicos()
//        {
//            try
//            {
//                _logger.LogInformation("Recuperando la lista de años académicos.");
//                var results = _repository.GetAllAniosAcademicos();
//                return Ok(Mapper.Map<IEnumerable<AnioAcademicoViewModel>>(results));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"No se pudo recuperar los años académicos: {ex}");
//                return BadRequest("No se pudo recuperar la información.");
//            }
//        }

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Método para recuperar un Año Académico específico.
//        /// </summary>
//        /// <param name="idAnioAcademico">Id del Año Académico.</param>
//        /// <returns>Acción con la respuesta.</returns>
//        [HttpGet("api/aniosAcademicos/{idAnioAcademico}")]
//        public IActionResult GetAnioAcademicoEspecifico(int idAnioAcademico)
//        {
//            try
//            {
//                _logger.LogInformation("Recuperando la información del año académico.");
//                var result = _repository.GetAnioAcademicoById(idAnioAcademico);
//                return Ok(Mapper.Map<AnioAcademicoViewModel>(result));
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"No se pudo recuperar la información del año académico: {ex}");
//                return BadRequest("No se pudo recuperar la información.");
//            }
//        }

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Método para agregar un Año Académico en la base de datos.
//        /// </summary>
//        /// <param name="thisAnioAcademico">Año Académico.</param>
//        /// <returns>Acción con la respuesta.</returns>
//        [HttpPost("api/aniosAcademicos/crear")]
//        public async Task<IActionResult> PostCrearAnioAcademico([FromBody] AnioAcademicoViewModel thisAnioAcademico)
//        {
//            _logger.LogInformation("Agregando el año académico.");

//            if (!_repository.IsNombreValido(Mapper.Map<AnioAcademico>(thisAnioAcademico)))
//                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

//            if (!_repository.IsFechasValidas(Mapper.Map<AnioAcademico>(thisAnioAcademico)))
//                ModelState.AddModelError("fechasMessageValidation", "Las fechas no son válidas.");

//            if (ModelState.IsValid)
//            {
//                var newAnioAcademico = Mapper.Map<AnioAcademico>(thisAnioAcademico);

//                _repository.AddAnioAcademico(newAnioAcademico);
//                if (await _repository.SaveChangesAsync())
//                {
//                    return Created($"api/aniosAcademicos/{thisAnioAcademico.Id}", Mapper.Map<AnioAcademicoViewModel>(newAnioAcademico));
//                }
//            }
//            _logger.LogError("No se pudo agregar el año académico.");
//            return BadRequest(ModelState);
//        }

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Método para actualizar un Año Académico en la base de datos.
//        /// </summary>
//        /// <param name="thisAnioAcademico">Año Académico con los datos actualizados.</param>
//        /// <returns>Acción con la respuesta.</returns>
//        [HttpPost("api/aniosAcademicos/editar")]
//        public async Task<IActionResult> PostEditarAnioAcademico([FromBody] AnioAcademicoViewModel thisAnioAcademico)
//        {
//            _logger.LogInformation("Actualizando el año académico.");

//            if (!_repository.IsNombreValido(Mapper.Map<AnioAcademico>(thisAnioAcademico)))
//                ModelState.AddModelError("nombreMessageValidation", "Este Nombre ya fue registrado.");

//            if (!_repository.IsFechasValidas(Mapper.Map<AnioAcademico>(thisAnioAcademico)))
//                ModelState.AddModelError("fechasMessageValidation", "Las fechas no son válidas.");

//            if (ModelState.IsValid)
//            {
//                var anioAcademicoToUpdate = Mapper.Map<AnioAcademico>(thisAnioAcademico);

//                var updatedAnioAcademico = _repository.UpdateAnioAcademico(anioAcademicoToUpdate);
//                if (await _repository.SaveChangesAsync())
//                {
//                    return Created($"api/aniosAcademicos/{updatedAnioAcademico.Id}", Mapper.Map<AnioAcademicoViewModel>(updatedAnioAcademico));
//                }
//            }

//            _logger.LogError("No se pudo actualizar el año académico.");
//            return BadRequest(ModelState);
//        }

//        /// <author>Eddy Wilmer Canaza Tito</author>
//        /// <summary>
//        /// Método para eliminar lógicamente un Año Académico en la base de datos.
//        /// </summary>
//        /// <param name="thisAnioAcademico">Año Académico</param>
//        /// <returns>Acción con la respuesta.</returns>
//        [HttpPost("api/aniosAcademicos/eliminar")]
//        public async Task<IActionResult> PostEliminarAnioAcademico([FromBody] AnioAcademicoViewModel thisAnioAcademico)
//        {
//            _logger.LogInformation("Eliminando el año académico.");

//            ModelState.Clear();

//            if (ModelState.IsValid)
//            {
//                var anioAcademicoToDelete = Mapper.Map<AnioAcademico>(_repository.GetAnioAcademicoById(thisAnioAcademico.Id));

//                _repository.DeleteAnioAcademico(anioAcademicoToDelete);
//                if (await _repository.SaveChangesAsync())
//                {
//                    return Ok("Se eliminó el colaborador correctamente.");
//                }
//            }

//            _logger.LogInformation("No se pudo eliminar el año académico.");
//            return BadRequest(ModelState);
//        }
//    }
//}
