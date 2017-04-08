using AutoMapper;
using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
using Matriculas.Queries.Persistence.Repositories;
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
    [Route("/api/v2/[controller]")]
    public class ProfesoresController : Controller
    {
        private ILogger<ProfesoresController> _logger;
        private IAppRepository _repository;

        public ProfesoresController(IAppRepository repository, ILogger<ProfesoresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GeAllProfesores()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de profesores.");
                var result = _repository.Profesores.GetAll();
                return Ok(Mapper.Map<IEnumerable<ProfesorViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la lista de profesores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProfesor(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del profesor.");
                var result = _repository.Profesores.Get(id);
                return Ok(Mapper.Map<ProfesorViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del profesor: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostProfesor([FromBody] ProfesorViewModel profesorDetails)
        {
            _logger.LogInformation("Agregando el profesor.");

            var profesor = Mapper.Map<Profesor>(profesorDetails);

            if (!_repository.Profesores.HasDniUnique(profesor))
                ModelState.AddModelError("Dni", "Dni no disponible.");

            if (ModelState.IsValid)
            {
                _repository.Profesores.Add(profesor);
                if (await _repository.Complete())
                {
                    return Created($"api/profesores/{profesorDetails.Id}", Mapper.Map<ProfesorViewModel>(profesorDetails));
                }
            }

            _logger.LogError("No se pudo agregar el profesor.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutProfesor([FromBody] ProfesorViewModel profesorDetails)
        {
            _logger.LogInformation("Actualizando los datos del profesor.");

            var profesor = Mapper.Map<Profesor>(profesorDetails);

            if (!_repository.Profesores.HasDniUnique(profesor))
                ModelState.AddModelError("Dni", "Dni no disponible.");

            if (ModelState.IsValid)
            {
                _repository.Profesores.Update(profesor);
                if (await _repository.Complete())
                {
                    return Created($"api/profesores/{profesorDetails.Id}", Mapper.Map<ProfesorViewModel>(profesor));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del profesor.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            _logger.LogInformation("Eliminando el profesor.");

            if (_repository.Profesores.HasCursos(_repository.Profesores.Get(id)))
                ModelState.AddModelError("noEliminable", "Tiene cursos asociados.");

            if (ModelState.IsValid)
            {
                _repository.Profesores.Delete(id);

                if (await _repository.Complete())
                {
                    return Created($"api/profesores/{id}", Mapper.Map<ProfesorViewModel>(_repository.Profesores.Get(id)));
                }
            }

            _logger.LogError("No se pudo eliminar el profesor.");
            return BadRequest(ModelState);
        }





        [HttpGet("{id}/cursos")]
        public IActionResult GetCursosDisponibles(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de cursos que dicta el profesor.");
                var result = _repository.Profesores.GetCursos(id);
                return Ok(Mapper.Map<IEnumerable<CursoViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cursos: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }
    }
}
