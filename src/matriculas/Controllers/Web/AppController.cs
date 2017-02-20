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

namespace Matriculas.Controllers
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que define el controlador de la aplicación.
    /// </summary>
    public class AppController : Controller
    {
        private IConfigurationRoot _config;
        private IMailService _mailService;
        private IMatriculasRepositorys _repository;
        private ILogger<AppController> _logger;
        private IHostingEnvironment _env;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la calse AppController
        /// </summary>
        /// <param name="mailService">Servicio de Email.</param>
        /// <param name="config">Configuración de la aplicación.</param>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        /// <param name="env">Hosting.</param>
        public AppController(IMailService mailService,
            IConfigurationRoot config,
            IMatriculasRepositorys repository,
            ILogger<AppController> logger,
            IHostingEnvironment env )
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
            _env = env;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al Index de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Secretaria, Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al módulo de Colaboradores de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Administrador")]
        public IActionResult Colaboradores()
        {
            return View();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para redirigir al módulo de Grados de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Grados()
        {
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al módulo de Secciones de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Secciones()
        {
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al módulo de Cursos de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Cursos()
        {
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al módulo de Profesores de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Administrador")]
        public IActionResult Profesores()
        {
            return View();
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para redirigir al módulo de Alumnos de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Secretaria, Administrador")]
        public IActionResult Alumnos()
        {
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al módulo de Años Académicos de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Administrador")]
        public IActionResult AniosAcademicos()
        {
            return View();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para redirigir al módulo de Cronogramas de Mátrícula de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Director, Administrador")]
        [Route("App/AniosAcademicos/CronogramaMatriculas/{id?}")]
        public IActionResult CronogramaMatriculas(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redirigir al módulo de Matrículas de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Secretaria, Administrador")]
        public IActionResult Matriculas()
        {
            return View();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para generar el reporte de Matrícula de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Secretaria, Administrador")]
        [Route("App/Matriculas/ReporteMatricula/{dni?}")]
        public IActionResult ReporteMatricula(string dni)
        {
            ReporteConstanciaMatricula newReporte = new ReporteConstanciaMatricula(_repository, _env);
            return newReporte.GenerarReporte(dni);          
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para generar el reporte de Lista de alumnos de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [Authorize(Roles = "Secretaria, Administrador")]
        [Route("App/Secciones/ReporteLista/{idSeccion?}")]
        public IActionResult ReporteLista(int idSeccion)
        {         
            ReporteLista newReporte = new ReporteLista(_repository, _env);
            return newReporte.GenerarReporte(idSeccion);
        }
    }
}
    