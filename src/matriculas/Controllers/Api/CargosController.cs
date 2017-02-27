using AutoMapper;
using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
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
    [Route("api/v2/[controller]")]
    public class CargosController : Controller
    {
        private ILogger<ColaboradoresController> _logger;
        private IAppRepository _repository;

        public CargosController(IAppRepository repository, ILogger<ColaboradoresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Get()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de roles.");
                var results = _repository.Cargos.GetAll();
                return Ok(Mapper.Map<IEnumerable<CargoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los roles: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }           
        }
    }
}
