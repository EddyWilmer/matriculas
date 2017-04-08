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
    [Route("api/v2/[controller]")]
    public class GradosController : Controller
    {
        private ILogger<GradosController> _logger;
        private IAppRepository _repository;

        public GradosController(IAppRepository repository, ILogger<GradosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetGrados()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de grados.");
                var results = _repository.Grados.GetAll();
                return Ok(Mapper.Map<IEnumerable<GradoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los grados: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetGrado(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del grado.");
                var result = _repository.Grados.Get(id);
                return Ok(Mapper.Map<GradoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del grado: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}/cursos")]
        public IActionResult GetCursos(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando los cursos del grado.");
                var result = _repository.Grados.GetCursos(id);
                return Ok(Mapper.Map<IEnumerable<CursoViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del grado: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostGrado([FromBody] GradoViewModel gradoDetails)
        {
            _logger.LogInformation("Agregando el grado.");

            var grado = Mapper.Map<Grado>(gradoDetails);

            if (!_repository.Grados.HasNombreUnique(grado))
                ModelState.AddModelError("Nombre", "Nombre no disponible.");

            if (ModelState.IsValid)
            {
                _repository.Grados.Add(grado);
                if (await _repository.Complete())
                {
                    return Created($"api/grados/{grado.Id}/grados", Mapper.Map<GradoViewModel>(grado));
                }
            }

            _logger.LogError("No se pudo agregar el grado.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutGrado([FromBody] GradoViewModel gradoDetails)
        {
            var grado = Mapper.Map<Grado>(gradoDetails);

            _logger.LogInformation("Actualizando los datos del grado.");

            if (!_repository.Grados.HasNombreUnique(grado))
                ModelState.AddModelError("Nombre", "Nombre no disponible.");

            if (ModelState.IsValid)
            {
                _repository.Grados.Update(grado);
                if (await _repository.Complete())
                {
                    return Created($"api/grados/{grado.Id}", Mapper.Map<GradoViewModel>(grado));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del grado.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrado(int id)
        {
            _logger.LogInformation("Eliminando el grado.");

            if (_repository.Grados.HasSecciones(_repository.Grados.Get(id)))
                ModelState.AddModelError("noEliminable", "Tiene secciones asociadas.");

            if (ModelState.IsValid)
            {
                _repository.Grados.Delete(id);

                if (await _repository.Complete())
                {
                    return Created($"api/grados/{id}", Mapper.Map<GradoViewModel>(_repository.Grados.Get(id)));
                }
            }

            _logger.LogError("No se pudo eliminar el grado.");
            return BadRequest(ModelState);
        }
    }
}
