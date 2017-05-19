using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Matriculas.ViewModels;
using Matriculas.Services;
using Microsoft.Extensions.Configuration;
using Matriculas.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Hosting;
using iTextSharp.text.html;
using Matriculas.Queries.Core.Repositories;

namespace Matriculas.Controllers
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que define el controlador de la aplicación.
    /// </summary>
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private IAppRepository _repository;
        private ILogger<AppController> _logger;
        private IHostingEnvironment _env;

        public AppController(IConfigurationRoot config,
			IAppRepository repository,
            ILogger<AppController> logger,
            IHostingEnvironment env )
        {
            _config = config;
            _repository = repository;
            _logger = logger;
            _env = env;
        }

        [Authorize(Roles = "Director, Secretaria, Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Colaboradores()
        {
            return View();
        }

        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Grados()
        {
            return View();
        }

        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Secciones()
        {
            return View();
        }

        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Cursos()
        {
            return View();
        }

        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Profesores()
        {
            return View();
        }

        [Authorize(Roles = "Director, Secretaria, Administrador")]
        public IActionResult Alumnos()
        {
            return View();
        }

        [Authorize(Roles = "Director, Administrador")]
        public IActionResult AniosAcademicos()
        {
            return View();
        }

        [Authorize(Roles = "Director, Administrador")]
        [Route("App/AniosAcademicos/{id?}/Cronogramas")]
        public IActionResult Cronogramas(int id)
        {
            ViewBag.IdAnioAcademico = id;
            return View();
        }

        [Authorize(Roles = "Secretaria, Administrador")]
        public IActionResult Matriculas()
        {
            return View();
        }

        [Authorize(Roles = "Secretaria, Administrador")]
        [Route("App/Matriculas/ReporteMatricula/{dni?}")]
        public IActionResult ReporteMatricula(string dni)
        {
            ReporteConstanciaMatricula newReporte = new ReporteConstanciaMatricula(_repository, _env);
            return newReporte.GenerarReporte(dni);
        }

        [Authorize(Roles = "Secretaria, Administrador")]
        [Route("App/Secciones/ReporteLista/{idSeccion?}")]
        public IActionResult ReporteLista(int idSeccion)
        {
            ReporteLista newReporte = new ReporteLista(_repository, _env);
            return newReporte.GenerarReporte(idSeccion);
        }
    }
}
    