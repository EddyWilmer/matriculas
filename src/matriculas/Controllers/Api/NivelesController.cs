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

namespace Matriculas.Controllers.Api
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que permite la interacción de las vistas con la entidad Niveles.
    /// </summary>
    [Route("api/niveles")]
    public class NivelesController : Controller
    {
        private ILogger<NivelesController> _logger;
        private IMatriculasRepositorys _repository;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase NivelesController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public NivelesController(IMatriculasRepositorys repository, ILogger<NivelesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar la lista de Niveles.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de niveles.");
                var results = _repository.GetAllNiveles();
                return Ok(Mapper.Map<IEnumerable<NivelViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los niveles: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
            
        }
    }
}
