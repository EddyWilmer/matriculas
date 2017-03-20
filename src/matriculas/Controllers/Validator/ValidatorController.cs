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

        [HttpPost]
        public IActionResult IsDniAlumnoUnique([Bind(Prefix = "Alumno")] Alumno entity)
        {
            if (_repository.Alumnos.HasDniUnique(entity))
                return Json(true);
            return Json("Dni no disponible.");
        }

    }
}
