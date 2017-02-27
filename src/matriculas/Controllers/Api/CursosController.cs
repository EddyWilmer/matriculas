using AutoMapper;
using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
using Matriculas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Controllers.Api
{
    [Route("/api/v2/[controller]")]
    public class CursosController : Controller
    {
        private ILogger<CursosController> _logger;
        private IAppRepository _repository;

        public CursosController(IAppRepository repository, ILogger<CursosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet()]
        public IActionResult GetAllCursos()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de cursos.");
                var results = _repository.Cursos.GetAll();
                return Ok(Mapper.Map<IEnumerable<CursoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cursos: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCurso(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del curso.");
                var result = _repository.Cursos.Get(id);
                return Ok(Mapper.Map<CursoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del curso: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostCurso([FromBody] CursoViewModel cursoDetails)
        {
            _logger.LogInformation("Agregando el curso.");

            var curso = Mapper.Map<Curso>(cursoDetails);

            if (ModelState.IsValid)
            {
                _repository.Cursos.Add(curso);
                if (await _repository.Complete())
                {
                    return Created($"api/cursos/{curso.Id}", Mapper.Map<CursoViewModel>(curso));
                }
            }

            _logger.LogError("No se pudo agregar el curso.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutCurso([FromBody] CursoViewModel cursoDetails)
        {
            _logger.LogInformation("Actualizando los datos del curso.");

            var curso = Mapper.Map<Curso>(cursoDetails);

            if (ModelState.IsValid)
            {
                _repository.Cursos.Update(curso);
                if (await _repository.Complete())
                {
                    return Created($"api/cursos/{curso.Id}", Mapper.Map<CursoViewModel>(curso));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del curso.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            _logger.LogInformation("Elimando el curso.");

            _repository.Cursos.Delete(id);
            if (await _repository.Complete())
            {
                return Created($"api/cursos/{id}", Mapper.Map<CursoViewModel>(_repository.Cursos.Get(id)));
            }

            _logger.LogError("No se pudo eliminar el curso.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }






        [HttpGet("{id}/searchProfesores")]
        public IActionResult SearchProfesores(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de profesores que dictan el curso.");
                var result = _repository.Cursos.SearchProfesores(id);
                return Ok(Mapper.Map<IEnumerable<ProfesorViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información de los profesores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpGet("{id}/profesor")]
        public IActionResult GetProfesor(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de profesores que dictan el curso.");
                var result = _repository.Cursos.GetProfesor(id);
                return Ok(Mapper.Map<ProfesorViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información de los profesores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost("{id}/assignProfesor/{idProfesor}")]
        public async Task<IActionResult> AssignProfesor(int id, int idProfesor)
        {
            _logger.LogInformation("Asignando profesor a curso.");

            if (ModelState.IsValid)
            {
                _repository.Cursos.AssignProfesor(id, idProfesor);
                if(await _repository.Complete())
                {
                    return Ok();
                }
            }

            _logger.LogError("No se pudo asignar profesor al curso.");
            return BadRequest(ModelState);
        }
    }
}
