using AutoMapper;
using Matriculas.Controllers;
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
using System.Web.Http;

namespace Matriculas.Controllers.Api
{
    [Route("api/v2/[controller]")]
    public class AniosAcademicosController : Controller
    {
        private ILogger<AniosAcademicosController> _logger;
        private IAppRepository _repository;

        public AniosAcademicosController(IAppRepository repository, ILogger<AniosAcademicosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAniosAcademicos()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de años académicos.");
                var results = _repository.AniosAcademicos.GetAll();
                return Ok(Mapper.Map<IEnumerable<AnioAcademicoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los años académicos: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetAnioAcademico(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del año académico.");
                var result = _repository.AniosAcademicos.Get(id);
                return Ok(Mapper.Map<AnioAcademicoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del año académico: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostAnioAcademico([FromBody] AnioAcademicoViewModel anioAcademicoDetails)
        {
            _logger.LogInformation("Agregando el año académico.");

            var anioAcademico = Mapper.Map<AnioAcademico>(anioAcademicoDetails);

            if (ModelState.IsValid)
            {
                _repository.AniosAcademicos.Add(anioAcademico);
                if (await _repository.Complete())
                {
                    return Created($"api/aniosAcademicos/{anioAcademicoDetails.Id}", Mapper.Map<AnioAcademicoViewModel>(anioAcademico));
                }
            }
            _logger.LogError("No se pudo agregar el año académico.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutAnioAcademico([FromBody] AnioAcademicoViewModel anioAcademicoDetails)
        {
            _logger.LogInformation("Actualizando el año académico.");

            var anioAcademico = Mapper.Map<AnioAcademico>(anioAcademicoDetails);

            if (ModelState.IsValid)
            {
                _repository.AniosAcademicos.Update(anioAcademico);
                if (await _repository.Complete())
                {
                    return Created($"api/aniosAcademicos/{anioAcademico.Id}", Mapper.Map<AnioAcademicoViewModel>(anioAcademico));
                }
            }

            _logger.LogError("No se pudo actualizar el año académico.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnioAcademico(int id)
        {
            _logger.LogInformation("Eliminando año académico.");

            _repository.AniosAcademicos.Delete(id);
            if (await _repository.Complete())
            {
                return Created($"api/aniosAcademicos/{id}", Mapper.Map<AnioAcademicoViewModel>(_repository.AniosAcademicos.Get(id)));
            }

            _logger.LogError("No se pudo eliminar el año académico.");
            return BadRequest("No se pudo eliminar este año académico.");
        }




        [HttpGet("{id}/cronogramas")]
        public IActionResult GetCronogramas(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del año académico.");
                var results = _repository.AniosAcademicos.GetCronogramas(id);
                return Ok(Mapper.Map<IEnumerable<CronogramaViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cronogramas de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }
    }
}
