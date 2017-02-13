using AutoMapper;
using Matriculas.Models;
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
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que permite la interacción de las vistas con la entidad Cursos.
    /// </summary>
    public class CursosController : Controller
    {
        private ILogger<CursosController> _logger;
        private IMatriculasRepository _repository;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase CursosController.
        /// </summary>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        public CursosController(IMatriculasRepository repository, ILogger<CursosController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar la lista de Cursos activos.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cursos")]
        public IActionResult GetCursos()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de cursos.");
                var results = _repository.GetAllCursos();
                return Ok(Mapper.Map<IEnumerable<CursoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cursos: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar los Cursos que puede dictar un profesor.
        /// </summary>
        /// <param name="idProfesor">Id del Profesor.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cursosProfesor/{idProfesor}")]
        public IActionResult GetCursosDisponibles(int idProfesor)
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de cursos que dicta el profesor.");
                var results = _repository.GetCursosByIdProfesor(idProfesor);
                return Ok(Mapper.Map<IEnumerable<CursoViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los cursos: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar un Curso específico.
        /// </summary>
        /// <param name="idCurso">Id del Alumno.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cursos/{idCurso}")]
        public IActionResult GetCursoEspecifico(int idCurso)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del curso.");
                var result = _repository.GetCursoById(idCurso);
                return Ok(Mapper.Map<CursoViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del curso: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar el profesor de un Curso específico.
        /// </summary>
        /// <param name="idCurso">Id del Curso.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cursos/profesor/{idCurso}")]
        public IActionResult GetCursoProfesorEspecifico(int idCurso)
        {
            try
            {
                _logger.LogInformation("Obteniendo la información del profesor que dicta un curso.");
                var thisProfesor = _repository.GetProfesorByIdCurso(idCurso);
                var thisCurso = _repository.GetCursoById(idCurso);
                // Crear objeto para transferencia
                CursoProfesorViewModel aux = new CursoProfesorViewModel
                {
                    Curso = Mapper.Map<CursoViewModel>(thisCurso),
                    Profesor = Mapper.Map<ProfesorViewModel>(thisProfesor)
                };

                return Ok(aux);
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del curso: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar los profesores que pueden dictar un Curso específico.
        /// </summary>
        /// <param name="idCurso">Id del Alumno</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/cursos/profesores/{idCurso}")]
        public IActionResult GetProfesoresCursoEspecifico(int idCurso)
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de profesores que dictan el curso.");
                var result = _repository.GetProfesoresCursoById(idCurso);
                return Ok(Mapper.Map<IEnumerable<ProfesorViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información de los profesores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un Profesor en la base de datos.
        /// </summary>
        /// <param name="thisCurso">Curso.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cursos/crear")]
        public async Task<IActionResult> PostCrearCurso([FromBody] CursoViewModel thisCurso)
        {
            _logger.LogInformation("Agregando el curso.");

            if (!_repository.IsNombreValido(Mapper.Map<Curso>(thisCurso)))
                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

            if (_repository.ExcedeMaximoHoras(Mapper.Map<Curso>(thisCurso)))
                ModelState.AddModelError("excesoHorasMessageValidation", "Este grado ha superado el máximo de horas permitido.");

            if (ModelState.IsValid)
            {
                var newCurso = Mapper.Map<Curso>(thisCurso);

                _repository.AddCurso(newCurso);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/cursos/{thisCurso.Id}", Mapper.Map<CursoViewModel>(newCurso));
                }
            }

            _logger.LogError("No se pudo agregar el curso.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un Profesor en la base de datos.
        /// </summary>
        /// <param name="thisCurso">Curso con los datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cursos/editar")]
        public async Task<IActionResult> PostEditarCurso([FromBody] CursoViewModel thisCurso)
        {
            _logger.LogInformation("Actualizando los datos del curso.");

            if (!_repository.IsNombreValido(Mapper.Map<Curso>(thisCurso)))
                ModelState.AddModelError("nombreMessageValidation", "Este nombre ya fue registrado.");

            if (_repository.ExcedeMaximoHoras(Mapper.Map<Curso>(thisCurso)))
                ModelState.AddModelError("excesoHorasMessageValidation", "Este grado ha superado el máximo de horas permitido.");

            if (ModelState.IsValid)
            {
                var cursoToUpdate = Mapper.Map<Curso>(thisCurso);

                var updatedCurso = _repository.UpdateCurso(cursoToUpdate);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/cursos/{updatedCurso.Id}", Mapper.Map<CursoViewModel>(updatedCurso));
                }
            }

            _logger.LogError("No se pudo actualizar los datos del curso.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar el curso que dicta un profesor en la base de datos.
        /// </summary>
        /// <param name="thisCursoProfesor">Profesor con su Curso actualizado.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cursos/profesor/editar")]
        public async Task<IActionResult> PostEditarCursoProfesor([FromBody] CursoProfesorViewModel thisCursoProfesor)
        {
            _logger.LogInformation("Actualizando los cursos que dictan el profesor.");

            if (ModelState.IsValid)
            {
                var thisProfesor = Mapper.Map<Profesor>(thisCursoProfesor.Profesor);
                var cursoToUpdate = Mapper.Map<Curso>(thisCursoProfesor.Curso);

                if(_repository.UpdateCursoProfesor(cursoToUpdate, thisProfesor))
                {
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok("Se actualizó el profesor correctamente");
                    }
                }   
                else
                {
                    ModelState.AddModelError("anioAcademicoMessageValidation", "No tiene un Año Académico creado.");
                    return BadRequest(ModelState);
                }
            }

            _logger.LogError("No se pudo actualizar el curso que dicta el profesor.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Curso en la base de datos.
        /// </summary>
        /// <param name="thisCurso">Curso.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/cursos/eliminar")]
        public async Task<IActionResult> PostEliminarCurso([FromBody] CursoViewModel thisCurso)
        {
            _logger.LogInformation("Elimando el curso.");

            var result = _repository.GetCursoById(thisCurso.Id);

            var cursoToDelete = Mapper.Map<Curso>(result);

            _repository.DeleteCurso(cursoToDelete);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("Se eliminó el colaborador correctamente.");
            }

            _logger.LogError("No se pudo eliminar el curso.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }
    }
}
