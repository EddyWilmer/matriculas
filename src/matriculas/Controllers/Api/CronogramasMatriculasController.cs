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
    public class CronogramasMatriculasController : Controller
    {
        private ILogger<CronogramasMatriculasController> _logger;
        private IAppRepository _repository;

        public CronogramasMatriculasController(IAppRepository repository, ILogger<CronogramasMatriculasController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetCronogramas()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de cronogramas de matrícula.");
                var results = _repository.CronogramasMatriculas.GetAll();
                return Ok(Mapper.Map<IEnumerable<CronogramaMatriculaViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cronogramas de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        //[HttpGet("{idAnioAcademico}")]
        //public IActionResult GetCronogramas(int idAnioAcademico)
        //{
        //    try
        //    {
        //        _logger.LogInformation("Recuperando la información del año académico.");
        //        var results = _repository.CronogramasMatriculas.Get(idAnioAcademico);
        //        return Ok(Mapper.Map<IEnumerable<CronogramaMatriculaViewModel>>(results));
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"No se pudo recuperar los cronogramas de matrícula: {ex}");
        //        return BadRequest("No se pudo recuperar la información.");
        //    }
        //}

        [HttpGet("{idAnioAcademico}/{nombre}")]
        public IActionResult GetCronogramaMatricula(int idAnioAcademico, string nombre)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del cronograma de matrícula.");
                var result = _repository.CronogramasMatriculas.GetCronograma(idAnioAcademico, nombre);
                return Ok(Mapper.Map<CronogramaMatriculaViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del cronograma de matrícula: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostCronogramaMatricula([FromBody] CronogramaMatriculaViewModel cronogramaDetails)
        {
            _logger.LogInformation("Agregando el cronograma de matrícula.");

            var cronogramaMatricula = Mapper.Map<CronogramaMatricula>(cronogramaDetails);

            if (ModelState.IsValid)
            {
                _repository.CronogramasMatriculas.Add(cronogramaMatricula);
                if (await _repository.Complete())
                {
                    return Created($"api/cronogramasMatriculas/{cronogramaDetails.AnioAcademicoId}", Mapper.Map<CronogramaMatriculaViewModel>(cronogramaMatricula));
                }
            }

            _logger.LogError("No se pudo agregar el cronograma de matrícula.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutCronogramaMatricula([FromBody] CronogramaMatriculaViewModel cronogramaDetails)
        {
            _logger.LogInformation("Actualizando la información del cronograma de matrícula.");

            var cronogramaMatricula = Mapper.Map<CronogramaMatricula>(cronogramaDetails);

            if (ModelState.IsValid)
            {
                var cronogramaMatriculaToUpdate = Mapper.Map<CronogramaMatricula>(cronogramaDetails);

                _repository.CronogramasMatriculas.Update(cronogramaMatriculaToUpdate);
                if (await _repository.Complete())
                {
                    return Created($"api/cronogramasMatriculas/{cronogramaMatricula.AnioAcademicoId}", Mapper.Map<CronogramaMatriculaViewModel>(cronogramaMatricula));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del cronograma de matrícula.");
            return BadRequest(ModelState);
        }

        //[HttpDelete("{idAnioAcademico}/{nombre}")]
        //public async Task<IActionResult> PostEliminarCronogramaMatricula(int idAnioAcademico, string nombre)
        //{
        //    _logger.LogInformation("Eliminando el cronograma de matrícula.");

        //    ModelState.Clear();

        //    if (ModelState.IsValid)
        //    {
        //        var anioAcademicoToDelete = Mapper.Map<CronogramaMatricula>(_repository.GetCronogramaMatriculaById(thisCronogramaMatricula.AnioAcademicoId, thisCronogramaMatricula.Nombre));

        //        _repository.DeleteCronogramaMatricula(anioAcademicoToDelete);
        //        if (await _repository.SaveChangesAsync())
        //        {
        //            return Ok("Se eliminó el colaborador correctamente.");
        //        }
        //    }

        //    _logger.LogError("No se pudo eliminar el cronograma de matrícula.");
        //    return BadRequest(ModelState);
        //}
    }
}
