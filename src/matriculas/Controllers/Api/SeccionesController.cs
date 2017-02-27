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
    public class SeccionesController : Controller
    {
        private ILogger<SeccionesController> _logger;
        private IAppRepository _repository;
  
        public SeccionesController(IAppRepository repository, ILogger<SeccionesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAllSecciones()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de secciones.");
                var results = _repository.Secciones.GetAll();
                return Ok(Mapper.Map<IEnumerable<SeccionViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los secciones: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetSeccion(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información de la sección.");
                var result = _repository.Secciones.Get(id);
                return Ok(Mapper.Map<SeccionViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información de la sección: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostSeccion([FromBody] SeccionViewModel seccionDetails)
        {
            _logger.LogInformation("Agregando la sección.");

            var seccion = Mapper.Map<Seccion>(seccionDetails);

            if (ModelState.IsValid)
            {
                _repository.Secciones.Add(seccion);

                if (await _repository.Complete())
                {
                    return Created($"api/secciones/{seccion.Id}", Mapper.Map<SeccionViewModel>(seccion));
                }
            }

            _logger.LogError("No se pudo agregar la sección.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutSeccion([FromBody] SeccionViewModel seccionDetails)
        {
            _logger.LogInformation("Actualizando los datos de la sección.");

            var seccion = Mapper.Map<Seccion>(seccionDetails);

            if (ModelState.IsValid)
            {
                _repository.Secciones.Update(seccion);
                
                if (await _repository.Complete())
                {
                    return Created($"api/secciones/{seccion.Id}", Mapper.Map<SeccionViewModel>(seccion));
                }
            }

            _logger.LogError("No se pudo actualizar los datos de la sección.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeccion(int id)
        {
            _logger.LogInformation("Eliminando la sección.");

            _repository.Secciones.Delete(id);
            if (await _repository.Complete())
            {
                return Created($"api/secciones/{id}", Mapper.Map<SeccionViewModel>(_repository.Secciones.Get(id)));
            }

            _logger.LogError("No se pudo eliminar la sección.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }
    }
}
