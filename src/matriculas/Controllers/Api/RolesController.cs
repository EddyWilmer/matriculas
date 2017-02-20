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
    /// Clase que permite la interacción de las vistas con la entidad Roles.
    /// </summary>
    [Route("api/roles")]
    public class RolesController : Controller
    {
        private ILogger<ColaboradoresController> _logger;
        private IMatriculasRepositorys _repository;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase RolesController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public RolesController(IMatriculasRepositorys repository, ILogger<ColaboradoresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar la lista de roles.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de roles.");
                var results = _repository.GetAllRoles();
                return Ok(Mapper.Map<IEnumerable<RolViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los roles: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
            
        }
    }
}
