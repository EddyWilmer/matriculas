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
using System.Web.Http;

namespace Matriculas.Controllers.Api
{
    [Route("api/v2/[Controller]")]
    public class CronogramasController : Controller
    {
        private ILogger<CronogramasController> _logger;
        private IAppRepository _repository;

        public CronogramasController(IAppRepository repository, ILogger<CronogramasController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult GetCronograma(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del cronograma de matrícula.");
                var result = _repository.Cronogramas.Get(id);
                return Ok(Mapper.Map<CronogramaViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del cronograma de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostCronograma([FromBody] CronogramaViewModel cronogramaDetails)
        {
            _logger.LogInformation("Agregando el cronograma de matrícula.");

            var cronograma = Mapper.Map<Cronograma>(cronogramaDetails);

            if (ModelState.IsValid)
            {
                _repository.Cronogramas.Add(cronograma);
                if (await _repository.Complete())
                {
                    return Created($"api/cronogramasMatriculas/{cronogramaDetails.AnioAcademicoId}", Mapper.Map<CronogramaViewModel>(cronograma));
                }
            }

            _logger.LogError("No se pudo agregar el cronograma de matrícula.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutCronograma([FromBody] CronogramaViewModel cronogramaDetails)
        {
            _logger.LogInformation("Actualizando la información del cronograma de matrícula.");

            var cronograma = Mapper.Map<Cronograma>(cronogramaDetails);

            if (ModelState.IsValid)
            {
                var cronogramaMatriculaToUpdate = Mapper.Map<Cronograma>(cronogramaDetails);

                _repository.Cronogramas.Update(cronogramaMatriculaToUpdate);
                if (await _repository.Complete())
                {
                    return Created($"api/cronogramasMatriculas/{cronograma.AnioAcademicoId}", Mapper.Map<CronogramaViewModel>(cronograma));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del cronograma de matrícula.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCronograma(int id)
        {
            _logger.LogInformation("Eliminando el cronograma.");

            _repository.Cronogramas.Delete(id);
            if (await _repository.Complete())
            {
                return Created($"api/cronogramas/{id}", Mapper.Map<CronogramaViewModel>(_repository.Cronogramas.Get(id)));
            }

            _logger.LogError("No se pudo eliminar el curso.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }
    }
}
