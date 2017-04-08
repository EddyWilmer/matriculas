using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Matriculas.Queries.Core.Repositories;
using Matriculas.ViewModels;
using Matriculas.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Matriculas.Controllers.Validator
{
    public class ValidatorController : Controller
    {
        private IAppRepository _repository;

        public ValidatorController(IAppRepository repository)
        {
            _repository = repository;
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsDniAlumnoUnique([Bind(Prefix = "Alumno.Id")] int id, [Bind(Prefix = "Alumno.Dni")] string dni)
        {
            if (_repository.Alumnos.HasDniUnique(new Models.Alumno { Id = id, Dni = dni}))
                return Json(data: " ");
            return Json(data: "Dni no válido.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsDniColaboradorUnique(int id, string dni)
        {
            if (_repository.Colaboradores.HasDniUnique(new Colaborador { Id = id, Dni = dni}))
                return Json(data: " ");
            return Json(data: "Dni no válido.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNombreGradoUnique(int id, string nombre, int nivel)
        {
            if (_repository.Grados.HasNombreUnique(new Grado { Id = id, Nombre = nombre, Nivel = _repository.Niveles.Get(nivel) }))
                return Json(data: " ");
            return Json(data: "Nombre no válido.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNombreSeccionUnique(int id, string nombre, int grado)
        {
            if (_repository.Secciones.HasNombreUnique(new Seccion { Id = id, Nombre = nombre, Grado = _repository.Grados.Get(grado) }))
                return Json(data: " ");
            return Json(data: "Nombre no válido.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNombreCursoUnique([Bind(Prefix = "Curso.Id")] int id, [Bind(Prefix = "Curso.Nombre")] string nombre, [Bind(Prefix = "Curso.Grado")] int grado)
        {
            if (_repository.Cursos.HasNombreUnique(new Curso { Id = id, Nombre = nombre, Grado = _repository.Grados.Get(grado) }))
                return Json(data: " ");
            return Json(data: "Nombre no válido.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult FitScheduleCurso([Bind(Prefix = "Curso.Id")] int id, [Bind(Prefix = "Curso.HorasAcademicas")] int horasAcademicas, [Bind(Prefix = "Curso.Grado")] int grado)
        {
            if (_repository.Cursos.FitSchedule(new Curso { Id = id, HorasAcademicas = horasAcademicas, Grado = _repository.Grados.Get(grado) }))
                return Json(data: " ");
            return Json(data: "Sin espacio en horario.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsDniProfesorUnique(int id, string dni)
        {
            if (_repository.Profesores.HasDniUnique(new Profesor { Id = id, Dni = dni }))
                return Json(data: " ");
            return Json(data: "Dni no disponible.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNombreAnioAcademicoUnique(int id, int nombre)
        {
            if (_repository.AniosAcademicos.HasNombreUnique(new AnioAcademico { Id = id, Nombre = nombre }))
                return Json(data: " ");
            return Json(data: "Nombre no válido.");
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult IsNombreCronogramaUnique(int id, string nombre)
        {
            if (_repository.Cronogramas.HasNombreUnique(new Cronograma { Id = id, Nombre = nombre }))
                return Json(data: " ");
            return Json(data: "Nombre no válido.");
        }
    }
}
