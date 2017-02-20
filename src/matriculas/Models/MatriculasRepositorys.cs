using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que interactua con la base de datos.
    /// </summary>
    public class MatriculasRepositorys : IMatriculasRepositorys
    {
        private MatriculasContext _context;
        private ILogger<MatriculasRepositorys> _logger;
        private UserManager<ApplicationUser> _userManager;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase MatriculasRepository.
        /// </summary>
        /// <param name="context">Contexto de la aplicación.</param>
        /// <param name="logger">Administrador de logging.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        public MatriculasRepositorys(MatriculasContext context, ILogger<MatriculasRepositorys> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener todos los Colaboradores activos.
        /// </summary>
        /// <returns>Lista de Colaboradores activos.</returns>
        public IEnumerable<Colaborador> GetAllColaboradores()
        {
            _logger.LogInformation("Obteniendo los usuarios de la base de datos");

            var colaboradores = _context.Colaboradores
                .Include(t => t.Rol)
                .OrderBy(t => t.ApellidoPaterno)
                .ThenBy(t => t.ApellidoMaterno)
                .ThenBy(t => t.Nombres)
                .Where(t => t.Estado != "2")
                .ToList();

            return colaboradores;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener un Colaborador específico a través de su Id.
        /// </summary>
        /// <param name="idColaborador">Id del colaborador.</param>
        /// <returns>Colaborador específico.</returns>
        public Colaborador GetColaboradorById(int idColaborador)
        {
            return _context.Colaboradores.Where(t => t.Id == idColaborador)
                .Include(t => t.Rol)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un Dni ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisColaborador">Colaborador.</param>
        /// <returns>Estado del Dni.</returns>
        public bool IsDniValido(Colaborador thisColaborador)
        {
            try
            {
                var result = GetAllColaboradores()
               .Where(t => t.Dni == thisColaborador.Dni)
               .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (result != null && thisColaborador.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un Email ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisColaborador">Colaborador.</param>
        /// <returns>Estado del Email.</returns>
        public bool IsEmailValido(Colaborador thisColaborador)
        {
            try
            {
                var result = _context.Users
                    .Where(t => t.NormalizedUserName == thisColaborador.Email.ToUpper())
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisColaborador.Id == result.ColaboradorId)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un Colaborador en la base de datos.
        /// </summary>
        /// <param name="thisColaborador">Colaborador.</param>
        /// <returns>Colaborador agregado.</returns>
        public Colaborador AddColaborador(Colaborador thisColaborador)
        {
            var rol = GetRolById(thisColaborador.Rol.Id);
            _context.Cargos.Attach(rol);
            _context.Colaboradores.Add(thisColaborador);

            return thisColaborador;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un Colaborador en la base de datos.
        /// </summary>
        /// <param name="colaboradorToUpdate">Colaborador con datos actualizados.</param>
        /// <returns>Colaborador actualizado.</returns>
        public Colaborador UpdateColaborador(Colaborador colaboradorToUpdate)
        {
            var thisColaborador = _context.Colaboradores
                .Include(t => t.Rol)
                .Where(t => t.Id == colaboradorToUpdate.Id)
                .FirstOrDefault();

            thisColaborador.ApellidoPaterno = colaboradorToUpdate.ApellidoPaterno;
            thisColaborador.ApellidoMaterno = colaboradorToUpdate.ApellidoMaterno;
            thisColaborador.Nombres = colaboradorToUpdate.Nombres;
            thisColaborador.Dni = colaboradorToUpdate.Dni;
            var rol = GetRolById(colaboradorToUpdate.Rol.Id); _context.Cargos.Attach(rol); thisColaborador.Rol = rol;
            thisColaborador.Email = colaboradorToUpdate.Email;
            thisColaborador.Celular = colaboradorToUpdate.Celular;

            _context.Update(thisColaborador);

            return thisColaborador;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Colaborador en la base de datos.
        /// </summary>
        /// <param name="colaboradorToDelete">Colaborador.</param>
        public void DeleteColaborador(Colaborador colaboradorToDelete)
        {
            var thisColaborador = _context.Colaboradores
                .Include(t => t.Rol)
                .Where(t => t.Id == colaboradorToDelete.Id)
                .FirstOrDefault();

            thisColaborador.Estado = "2";

            _context.Update(thisColaborador);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para cambiar el estado del Usuario asociado al Colaborador.
        /// </summary>
        /// <param name="colaboradorToChangeEstado">Colaborador.</param>
        /// <returns>Colaborador actualizado.</returns>
        public Colaborador ChangeEstadoUsuario(Colaborador colaboradorToChangeEstado)
        {
            var thisColaborador = GetColaboradorById(colaboradorToChangeEstado.Id);

            if (thisColaborador.Estado == "1")
                thisColaborador.Estado = "3";
            else if (thisColaborador.Estado == "3")
                thisColaborador.Estado = "1";

            _context.Update(thisColaborador);

            return thisColaborador;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los Roles.
        /// </summary>
        /// <returns>Lista de Roles.</returns>
        public IEnumerable<Rol> GetAllRoles()
        {
            _logger.LogInformation("Obteniendo los roles de la base de datos");
            var roles = _context.Cargos
                .ToList();
            return roles;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener un Rol específico a través de su Id.
        /// </summary>
        /// <param name="idRol">Id del Rol.</param>
        /// <returns>Rol específico.</returns>
        public Rol GetRolById(int idRol)
        {
            return _context.Cargos.Where(t => t.Id == idRol)
                .FirstOrDefault();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para obtener los Grados activos.
        /// </summary>
        /// <returns>Lista de Grados.</returns>
        public IEnumerable<Grado> GetAllGrados()
        {
            _logger.LogInformation("Obteniendo los roles de la base de datos");
            return _context.Grados
                .Include(t => t.GradoRequisito)
                .Include(t => t.Nivel)
                .OrderBy(t => t.Nivel.Nombre)
                .ThenBy(t => t.Nombre)
                .Where(t => t.Estado == "1")
                .ToList();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para obtener un Grado Específico a través de su Id.
        /// </summary>
        /// <param name="idGrado">Id del Grado.</param>
        /// <returns>Grado específico.</returns>
        public Grado GetGradoById(int idGrado)
        {
            return _context.Grados
                .Include(t => t.GradoRequisito)
                .Include(t => t.Nivel)
                .Where(t => t.Id == idGrado)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los Cursos de un Grado específico a través de su Id.
        /// </summary>
        /// <param name="idGrado">Id del Grado.</param>
        /// <returns>Lista de Cursos del Grado.</returns>
        public IEnumerable<Curso> GetCursosGradoById(int idGrado)
        {
            return GetAllCursos()
                .Where(t => t.Grado.Id == idGrado)
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un nombre de Grado ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisGrado">Grado.</param>
        /// <returns>Estado del nombre del Grado.</returns>
        public bool IsNombreValido(Grado thisGrado)
        {
            try
            {
                var result = GetAllGrados()
                .Where(t => t.Nombre.ToLower() == thisGrado.Nombre.ToLower())
                .Where(t => t.Nivel.Nombre == thisGrado.Nivel.Nombre)
                .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (result != null && thisGrado.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un Grado no tiene secciones asociadas.
        /// </summary>
        /// <param name="thisGrado">Grado.</param>
        /// <returns>Estado del Grado.</returns>
        public bool IsEliminable(Grado thisGrado)
        {
            try
            {
                var resultSecciones = GetAllSecciones()
               .Where(t => t.Grado.Id == thisGrado.Id)
               .ToList();

                var resultCursos = GetAllCursos()
                   .Where(t => t.Grado.Id == thisGrado.Id)
                   .ToList();

                if (resultSecciones.Count == 0 && resultCursos.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para agregar un Grado en la base de datos.
        /// </summary>
        /// <param name="thisGrado">Grado.</param>
        public void AddGrado(Grado thisGrado)
        {
            var nivel = GetNivelById(thisGrado.Nivel.Id);
            _context.Niveles.Attach(nivel);

            _context.Grados.Add(thisGrado);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para actualizar un Grado en la base de datos.
        /// </summary>
        /// <param name="gradoToUpdate">Grado con datos actualizados.</param>
        /// <returns>Grado actualizado.</returns>
        public Grado UpdateGrado(Grado gradoToUpdate)
        {
            var thisGrado = _context.Grados
                .Include(t => t.Nivel)
                .Where(t => t.Id == gradoToUpdate.Id)
                .FirstOrDefault();

            thisGrado.Nombre = gradoToUpdate.Nombre;
            var nivel = GetNivelById(gradoToUpdate.Nivel.Id); _context.Niveles.Attach(nivel); thisGrado.Nivel = nivel;
            thisGrado.Capacidad = gradoToUpdate.Capacidad;
            var gradoRequisito = GetGradoById(gradoToUpdate.GradoRequisito.Id); _context.Grados.Attach(gradoRequisito); thisGrado.GradoRequisito = gradoRequisito;

            _context.Update(thisGrado);

            return thisGrado;
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para eliminar lógicamente un Grado en la base de datos.
        /// </summary>
        /// <param name="gradoToDelete">Grado.</param>
        public void DeleteGrado(Grado gradoToDelete)
        {
            var thisGrado = _context.Grados
                .Include(t => t.Nivel)
                .Where(t => t.Id == gradoToDelete.Id)
                .FirstOrDefault();

            thisGrado.Estado = "0";

            _context.Update(thisGrado);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los Niveles activos.
        /// </summary>
        /// <returns>Lista de Niveles.</returns>
        public IEnumerable<Nivel> GetAllNiveles()
        {
            return _context.Niveles.ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener un Nivel específico a través de su Id.
        /// </summary>
        /// <param name="idNivel">Id del Nivel.</param>
        /// <returns>Nivel específico.</returns>
        public Nivel GetNivelById(int idNivel)
        {
            return _context.Niveles
                .Where(t => t.Id == idNivel)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener las Secciones activas.
        /// </summary>
        /// <returns>Lista de Secciones activas.</returns>
        public IEnumerable<Seccion> GetAllSecciones()
        {
            _logger.LogInformation("Obteniendo las secciones de la base de datos");
            return _context.Secciones
                .Include(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .OrderBy(t => t.Grado.Nivel.Nombre)
                .ThenBy(t => t.Grado.Nombre)
                .ThenBy(t => t.Nombre)
                .Where(t => t.Estado == "1")
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener una Sección específica.
        /// </summary>
        /// <param name="idSeccion">Id de la Sección.</param>
        /// <returns>Sección específica.</returns>
        public Seccion GetSeccionById(int idSeccion)
        {
            return _context.Secciones
                .Include(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .Where(t => t.Id == idSeccion)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un nombre de Sección ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisSeccion">Sección.</param>
        /// <returns>Estado del nombre de la Sección.</returns>
        public bool IsNombreValido(Seccion thisSeccion)
        {
            try
            {
                var result = GetAllSecciones()
                    .Where(t => t.Nombre.ToLower() == thisSeccion.Nombre.ToLower())
                    .Where(t => t.Grado.Nombre == thisSeccion.Grado.Nombre && t.Grado.Nivel == thisSeccion.Grado.Nivel)
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisSeccion.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar una Sección en la base de datos.
        /// </summary>
        /// <param name="thisSeccion">Sección.</param>
        public void AddSeccion(Seccion thisSeccion)
        {
            var grado = GetGradoById(thisSeccion.Grado.Id);
            _context.Grados.Attach(grado);

            _context.Secciones.Add(thisSeccion);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar una Sección en la base de datos. 
        /// </summary>
        /// <param name="seccionToUpdate">Sécción con datos actualizados.</param>
        /// <returns>Sección actualizada.</returns>
        public Seccion UpdateSeccion(Seccion seccionToUpdate)
        {
            var thisSeccion = _context.Secciones
                .Include(t => t.Grado)
                .Where(t => t.Id == seccionToUpdate.Id)
                .FirstOrDefault();

            thisSeccion.Nombre = seccionToUpdate.Nombre;
            var grado = GetGradoById(seccionToUpdate.Grado.Id); _context.Grados.Attach(grado); thisSeccion.Grado = grado;

            _context.Update(thisSeccion);

            return (thisSeccion);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente una Sección en la base de datos.
        /// </summary>
        /// <param name="seccionToDelete">Sección.</param>
        public void DeleteSeccion(Seccion seccionToDelete)
        {
            var thisSeccion = _context.Secciones
                .Include(t => t.Grado)
                .Where(t => t.Id == seccionToDelete.Id)
                .FirstOrDefault();

            thisSeccion.Estado = "0";

            _context.Update(thisSeccion);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener la lista de Alumnos de una sección específica.
        /// </summary>
        /// <param name="idSeccion">Id de la Sección.</param>
        /// <returns>Lista de Alumnos.</returns>
        public IEnumerable<Alumno> GetListaAlumnosByIdSeccion(int idSeccion)
        {
            DateTime fechaActual = new DateTime(2017, 3, 10);

            var anioAcademico = GetAllAniosAcademicos()
                .Where(t => t.FechaInicio.Value.Year == fechaActual.Year)
                .FirstOrDefault();

            var lista = _context.Alumnos.FromSql(String.Format("EXEC SP_ListaAlumnosPorSeccion @idAnioAcademico={0}, @idSeccion={1}", anioAcademico.Id, idSeccion)).ToList();

            return lista;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener la lista de Cursos activos.
        /// </summary>
        /// <returns>Lista de Cursos activos.</returns>
        public IEnumerable<Curso> GetAllCursos()
        {
            return _context.Cursos
                .Include(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .OrderBy(t => t.Grado.Nivel.Nombre)
                .ThenBy(t => t.Grado.Nombre)
                .ThenBy(t => t.Nombre)
                .Where(t => t.Estado == "1")
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener un Curso específico a través de su Id.
        /// </summary>
        /// <param name="idCurso">Id del Curso.</param>
        /// <returns>Curso específico.</returns>
        public Curso GetCursoById(int idCurso)
        {
            return GetAllCursos()
                .Where(t => t.Id == idCurso)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un nombre de Curso ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisCurso">Curso.</param>
        /// <returns>Estado del nombre del Curso.</returns>
        public bool IsNombreValido(Curso thisCurso)
        {
            try
            {
                var result = GetAllCursos()
                    .Where(t => t.Nombre.ToLower() == thisCurso.Nombre.ToLower())
                    .Where(t => t.Grado.Nombre == thisCurso.Grado.Nombre)
                    .Where(t => t.Grado.Nivel.Nombre == thisCurso.Grado.Nivel.Nombre)
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisCurso.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un curso en la base de datos.
        /// </summary>
        /// <param name="thisCurso">Curso.</param>
        public void AddCurso(Curso thisCurso)
        {
            var grado = GetGradoById(thisCurso.Grado.Id);
            _context.Grados.Attach(grado);

            _context.Cursos.Add(thisCurso);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un Curso en la base de datos.
        /// </summary>
        /// <param name="cursoToUpdate">Curso con datos actualizados.</param>
        /// <returns>Curso actualizado.</returns>
        public Curso UpdateCurso(Curso cursoToUpdate)
        {
            var thisCurso = _context.Cursos
                .Include(t => t.Grado)
                .Where(t => t.Id == cursoToUpdate.Id)
                .FirstOrDefault();

            thisCurso.Nombre = cursoToUpdate.Nombre;
            var grado = GetGradoById(cursoToUpdate.Grado.Id); _context.Grados.Attach(grado); thisCurso.Grado = grado;
            thisCurso.HorasAcademicas = cursoToUpdate.HorasAcademicas;

            _context.Update(thisCurso);

            return (thisCurso);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Curso en la base de datos.
        /// </summary>
        /// <param name="cursoToDelete">Curso.</param>
        public void DeleteCurso(Curso cursoToDelete)
        {
            var thisCurso = _context.Cursos
                .Include(t => t.Grado)
                .Where(t => t.Id == cursoToDelete.Id)
                .FirstOrDefault();

            thisCurso.Estado = "0";

            _context.Update(thisCurso);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para verificar si un Grado ha excedido 34 horas académicas en la programaciónd de Cursos.
        /// </summary>
        /// <param name="thisCurso">Curso.</param>
        /// <returns>Estado de las horas del Curso.</returns>
        public bool ExcedeMaximoHoras(Curso thisCurso)
        {
            try
            {
                var horasCursoActual = 0;
                var currentCurso = _context.Cursos.Where(t => t.Id == thisCurso.Id).FirstOrDefault();

                if (currentCurso != null)
                    horasCursoActual = currentCurso.HorasAcademicas;
                else
                    currentCurso = thisCurso;

                var horasAsignadas = _context.Cursos
                    .Where(t => t.Grado == currentCurso.Grado && t.Estado == "1")
                    .GroupBy(t => t.Grado.Id)
                    .Select(t => t.Sum(x => x.HorasAcademicas))
                    .SingleOrDefault() - horasCursoActual + thisCurso.HorasAcademicas;

                if (currentCurso.Grado.Nivel.Nombre == "Primaria")
                {
                    if (horasAsignadas > 27)
                        return true;
                    else
                        return false;
                }
                else if (currentCurso.Grado.Nivel.Nombre == "Secundaria")
                {
                    if (horasAsignadas > 34)
                        return true;
                    else
                        return false;
                }
                return false;
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return false;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los Profesores que pueden dictar un Curso específico.
        /// </summary>
        /// <param name="idCurso">Id del Curso.</param>
        /// <returns>Lista de profesores.</returns>
        public IEnumerable<Profesor> GetProfesoresCursoById(int idCurso)
        {
            return _context.ProfesorCursos
                .Include(t => t.Profesor)
                .Where(t => t.CursoId == idCurso)
                .Select(t => t.Profesor)
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar los cursos que dicta un Profesor.
        /// </summary>
        /// <param name="cursoToUpdate">Curso a actualizar.</param>
        /// <param name="thisProfesor">Profesor.</param>
        /// <returns></returns>
        public bool UpdateCursoProfesor(Curso cursoToUpdate, Profesor thisProfesor)
        {
            var currentAnioAcademico = _context.AniosAcademicos
                .Where(t => t.FechaInicio.Value.Year == DateTime.Now.Year)
                .FirstOrDefault();

            if (currentAnioAcademico != null)
            {
                var result = _context.CursosAnioAcademico
               .Where(t => t.AnioAcademicoId == currentAnioAcademico.Id)
               .Where(t => t.CursoId == cursoToUpdate.Id)
               .Where(t => t.GradoId == cursoToUpdate.Grado.Id)
               .FirstOrDefault();

                result.ProfesorId = thisProfesor.Id;

                _context.Update(result);

                return true;
            }
            else
                return false;           
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener el Profesor que está dictando un Curso en el Año Académico actual.
        /// </summary>
        /// <param name="idCurso">Id del Curso.</param>
        /// <returns>Profesor que dicta el Curso.</returns>
        public Profesor GetProfesorByIdCurso(int idCurso)
        {
            var currentCurso = GetCursoById(idCurso);

            var currentAnioAcademico = _context.AniosAcademicos
                .Where(t => t.FechaInicio.Value.Year == DateTime.Now.Year)
                .FirstOrDefault();

            if (currentAnioAcademico != null)
                return _context.CursosAnioAcademico
                    .Include(t => t.Profesor)
                    .Where(t => t.AnioAcademicoId == currentAnioAcademico.Id)
                    .Where(t => t.CursoId == currentCurso.Id)
                    .Where(t => t.GradoId == currentCurso.Grado.Id)
                    .Select(t => t.Profesor)
                    .FirstOrDefault();
            else
                return null;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener la lista de Profesores activos.
        /// </summary>
        /// <returns>Lista de Profesores activos.</returns>
        public IEnumerable<Profesor> GetAllProfesores()
        {
            return _context.Profesores
                .Include(t => t.ProfesorCurso)
                .ThenInclude(t => t.Curso)
                .Where(t => t.Estado == "1")
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener un Profesor específico a través de un Id.
        /// </summary>
        /// <param name="idProfesor">Id del Profesor</param>
        /// <returns>Profesor específico.</returns>
        public Profesor GetProfesorById(int idProfesor)
        {
            return _context.Profesores
                .Include(t => t.ProfesorCurso)
                .Where(t => t.Id == idProfesor)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un Dni ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisProfesor">Profesor</param>
        /// <returns>Estado del Dni del Profesor.</returns>
        public bool IsDniValido(Profesor thisProfesor)
        {
            try
            {
                var result = GetAllProfesores()
                    .Where(t => t.Dni == thisProfesor.Dni)
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisProfesor.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un un Profesor tiene Cursos asociados.
        /// </summary>
        /// <param name="thisProfesor"></param>
        /// <returns></returns>
        public bool IsEliminable(Profesor thisProfesor)
        {
            var result = _context.ProfesorCursos
               .Where(t => t.ProfesorId == thisProfesor.Id)
               .ToList();

            if (result.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los Cursos que puede dictar un Profesor.
        /// </summary>
        /// <param name="idProfesor">Id del Profesor.</param>
        /// <returns>Lista de cursos.</returns>
        public IEnumerable<Curso> GetCursosByIdProfesor(int idProfesor)
        {
            var cursosProfesor = _context.ProfesorCursos
                .Where(t => t.ProfesorId == idProfesor)
                .ToList();

            List<Curso> cursos = new List<Curso>();
            foreach(ProfesorCurso curso in cursosProfesor)
            {
                cursos.Add(GetCursoById(curso.CursoId));
            }

            return cursos.AsEnumerable();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un Profesor en la base de datos.
        /// </summary>
        /// <param name="thisProfesor">Profesor.</param>
        /// <param name="cursos">Lista de Cursos.</param>
        public void AddProfesor(Profesor thisProfesor, IEnumerable<Curso> cursos)
        {
            _context.Profesores.Add(thisProfesor);
            
            if (cursos != null)
            {
                _context.SaveChanges();
                
                foreach (Curso curso in cursos)
                {
                    _context.Database.ExecuteSqlCommand(String.Format("EXEC SP_AddProfesorCurso @idProfesor='{0}', @idCurso='{1}'", thisProfesor.Id, curso.Id));
                }
                _context.Update(thisProfesor);
            }  
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un Profesor en la base de datos.
        /// </summary>
        /// <param name="profesorToUpdate">Profesor con datos actualizados.</param>
        /// <param name="cursos">Cursos actualizados.</param>
        /// <returns>Profesor actualizado.</returns>
        public Profesor UpdateProfesor(Profesor profesorToUpdate, IEnumerable<Curso> cursos)
        {
            var thisProfesor = GetProfesorById(profesorToUpdate.Id);

            thisProfesor.ApellidoPaterno = profesorToUpdate.ApellidoPaterno;
            thisProfesor.ApellidoMaterno = profesorToUpdate.ApellidoMaterno;
            thisProfesor.Nombres = profesorToUpdate.Nombres;
            thisProfesor.Dni = profesorToUpdate.Dni;
            thisProfesor.Sexo = profesorToUpdate.Sexo;
            thisProfesor.Celular = profesorToUpdate.Celular;
            thisProfesor.Email = profesorToUpdate.Email;

            _context.Update(thisProfesor);

            _context.Database.ExecuteSqlCommand(String.Format("EXEC SP_DeleteCursos @idProfesor='{0}'", thisProfesor.Id));
            foreach (Curso curso in cursos)
            {
                _context.Database.ExecuteSqlCommand(String.Format("EXEC SP_AddProfesorCurso @idProfesor='{0}', @idCurso='{1}'", thisProfesor.Id, curso.Id));
            }

            return thisProfesor;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Profesor en la base de datos.
        /// </summary>
        /// <param name="profesorToDelete">Profesor.</param>
        public void DeleteProfesor(Profesor profesorToDelete)
        {
            var thisProfesor = GetProfesorById(profesorToDelete.Id);
            thisProfesor.Estado = "0";

            _context.Database.ExecuteSqlCommand(String.Format("EXEC SP_DeleteCursos @idProfesor='{0}'", thisProfesor.Id));

            _context.Update(thisProfesor);
        }

								/// <author>Julissa Zaida Huaman Hilari</author>
								/// <summary>
								/// Método para obtener los Alumnos activos.
								/// </summary>
								/// <returns>Lista de Alumnos.</returns>
								//public IEnumerable<Alumno> GetAllAlumnos()
								//{
								//    return _context.Alumnos
								//        .Include(t => t.Apoderado)
								//        .Where(t => t.Estado == "1")
								//        .ToList();
								//}

								/// <author>Julissa Zaida Huaman Hilari</author>
								/// <summary>
								/// Método para obtener un Alumno específico a través de un Id.
								/// </summary>
								/// <param name="idAlumno">Id del Alumno.</param>
								/// <returns>Alumno específico.</returns>
								//public Alumno GetAlumnoById(int idAlumno)
								//{
								//    return _context.Alumnos
								//        .Include(t => t.Apoderado)
								//        .Where(t => t.Id == idAlumno)
								//        .FirstOrDefault();
								//}

								/// <author>Eddy Wilmer Canaza Tito</author>
								/// <summary>
								/// Método para obtener un Alumno específico a través de un Dni.
								/// </summary>
								/// <param name="dni">Dni del Alumno.</param>
								/// <returns>Alumno específico.</returns>
								public Alumno GetAlumnoByDni(string dni)
        {
            return _context.Alumnos
                .Include(t => t.Apoderado)
                .Where(t => t.Dni == dni)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener la última matrícula del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Última Matrícula.</returns>
        public Matricula GetLastMatricula(int idAlumno)
        {
            return _context.Matriculas
                .Include(t => t.Alumno)
                .Include(t => t.Seccion)
                .ThenInclude(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .Include(t => t.Registrador)
                .Include(t => t.AnioAcademico)
                .Where(t => t.AlumnoId == idAlumno)
                .Last();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener el próximo Grado de un Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Siguiente Grado.</returns>
        public Grado GetNextGrado(int idAlumno)
        {
            try
            {
                var matricula = GetLastMatricula(idAlumno);

                var cursosDesaprobados = GetNotasLastMatricula(idAlumno)
                    .Where(t => t.Calificacion <= 11)
                    .ToList();

                if (cursosDesaprobados.Count > 0)
                {
                    return matricula.Seccion.Grado;
                }
                else
                {
                    return _context.Grados
                    .Include(t => t.GradoRequisito)
                    .Where(t => t.GradoRequisito.Id == matricula.Seccion.Grado.Id)
                    .Include(t => t.Nivel)
                    .OrderBy(t => t.Nivel.Nombre)
                    .ThenBy(t => t.Nombre)
                    .FirstOrDefault();
                }
            }
            catch
            {
                return _context.Grados
                    .Include(t => t.GradoRequisito)
                    .Where(t => t.GradoRequisito == null)
                    .Include(t => t.Nivel)
                    .OrderBy(t => t.Nivel.Nombre)
                    .ThenBy(t => t.Nombre)
                    .FirstOrDefault();
            }       
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener las últimas Notas del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Lista de últimas Notas.</returns>
        public IEnumerable<Nota> GetNotasLastMatricula(int idAlumno)
        {
            var idMatricula = GetLastMatricula(idAlumno);

            return _context.Notas
                .Where(t => t.MatriculaId == idMatricula.Id)
                .Include(t => t.Curso)
                .ThenInclude(t => t.Grado)
                .ThenInclude(t => t.Nivel)
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener las útlimas Deudas del Alumno.
        /// </summary>
        /// <param name="idAlumno">Id del Alumno.</param>
        /// <returns>Lista de últimas Deudas.</returns>
        public IEnumerable<Deuda> GetDeudasLastMatricula(int idAlumno)
        {
            var idMatricula = GetLastMatricula(idAlumno);

            return _context.Deudas
                .Include(t => t.Matricula)
                .ThenInclude(t => t.AnioAcademico)
                .Where(t => t.MatriculaId == idMatricula.Id)
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un Dni ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisAlumno">Alumno.</param>
        /// <returns>Estado del Dni del Alumno.</returns>
        public bool IsDniValido(Alumno thisAlumno)
        {
            try
            { 
                var result = GetAllAlumnos()          
                    .Where(t => t.Dni == thisAlumno.Dni)
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisAlumno.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
}

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para agregar un Alumno en la base de datos.
        /// </summary>
        /// <param name="thisAlumno">Alumno</param>
        public void AddAlumno(Alumno thisAlumno)
        {
            _context.Alumnos.Add(thisAlumno);
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para actualizar un Alumno en la base de datos.
        /// </summary>
        /// <param name="alumnoToUpdate">Alumno con datos actualizados.</param>
        /// <returns>Alumno actualizado.</returns>
        public void UpdateAlumno(Alumno alumnoToUpdate)
        {
            var thisAlumno = GetAlumnoById(alumnoToUpdate.Id);
            thisAlumno.ApellidoPaterno = alumnoToUpdate.ApellidoPaterno;
            thisAlumno.ApellidoMaterno = alumnoToUpdate.ApellidoMaterno;
            thisAlumno.Nombres = alumnoToUpdate.Nombres;
            thisAlumno.Dni = alumnoToUpdate.Dni;
            thisAlumno.Sexo = alumnoToUpdate.Sexo;
            thisAlumno.Direccion = alumnoToUpdate.Direccion;
            thisAlumno.FechaNacimiento = alumnoToUpdate.FechaNacimiento;

            _context.Update(thisAlumno);
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para eliminar lógicamente un Alumno en la base de datos.
        /// </summary>
        /// <param name="alumnoToDelete">Alumno</param>
        public void DeleteAlumno(Alumno alumnoToDelete)
        {
            var thisAlumno = GetAlumnoById(alumnoToDelete.Id);

            thisAlumno.Estado = "0";

            _context.Update(thisAlumno);
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para obtener los Apoderados activos.
        /// </summary>
        /// <returns>Lista de Apoderados.</returns>
        public IEnumerable<Apoderado> GetAllApoderados()
        {
            return _context.Apoderados
                .ToList();
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para actualizar un Apoderado en la base de datos.
        /// </summary>
        /// <param name="apoderadoToUpdate">Apoderado con datos actualizados.</param>
        /// <returns>Apoderado actualizado.</returns>
        public void UpdateApoderado(Apoderado apoderadoToUpdate)
        {
            var thisApoderado = GetApoderadoById(apoderadoToUpdate.Id);
            thisApoderado.ApellidoPaterno = apoderadoToUpdate.ApellidoPaterno;
            thisApoderado.ApellidoMaterno = apoderadoToUpdate.ApellidoMaterno;
            thisApoderado.Nombres = apoderadoToUpdate.Nombres;
            thisApoderado.Dni = apoderadoToUpdate.Dni;
            thisApoderado.Sexo = apoderadoToUpdate.Sexo;
            thisApoderado.EstadoCivil = apoderadoToUpdate.EstadoCivil;

            _context.Update(thisApoderado);
        }

        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Método para obtener un Apoderado específico a través de un Id.
        /// </summary>
        /// <param name="idApoderado">Id del Apoderado.</param>
        /// <returns>Apoderado específico.</returns>
        public Apoderado GetApoderadoById(int idApoderado)
        {
            return _context.Apoderados
                .Where(t => t.Id == idApoderado)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un Dni ya está registrado en la base de datos
        /// </summary>
        /// <param name="thisApoderado">Apoderado.</param>
        /// <returns>Estado del Dni del Apoderado.</returns>
        public bool IsDniValido(Apoderado thisApoderado)
        {
            var result = GetAllApoderados()
                .Where(t => t.Estado == "1")
                .Where(t => t.Dni == thisApoderado.Dni)
                .FirstOrDefault();

            if (result == null)
            {
                return true;
            }
            else
            {
                if (thisApoderado.Id == result.Id)
                    return true;
                else
                    return false;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener los Años Académicos.
        /// </summary>
        /// <returns>Lista de Años Académicos.</returns>
        public IEnumerable<AnioAcademico> GetAllAniosAcademicos()
        {
            return _context.AniosAcademicos
                .Where(t => t.Estado == "1")
                .ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener un Año Académico específico a través de un Id.
        /// </summary>
        /// <param name="idAnioAcademico">Id del Año Académico.</param>
        /// <returns>Año Académico específico.</returns>
        public AnioAcademico GetAnioAcademicoById(int idAnioAcademico)
        {
            return _context.AniosAcademicos
                .Where(t => t.Id == idAnioAcademico)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un nombre ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisAnioAcademico">Año Académico.</param>
        /// <returns>Estao del Nombre del Año Académico.</returns>
        public bool IsNombreValido(AnioAcademico thisAnioAcademico)
        {
            try
            { 
                var result = GetAllAniosAcademicos()
                    .Where(t => t.Nombre.ToLower() == thisAnioAcademico.Nombre.ToLower())
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisAnioAcademico.Id == result.Id)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
}

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si las fechas de un Año Académico están dentro del mismo año.
        /// </summary>
        /// <param name="thisAnioAcademico">Año Académico.</param>
        /// <returns>Estado de las Fechas del Año Académico.</returns>
        public bool IsFechasValidas(AnioAcademico thisAnioAcademico)
        {
            if (thisAnioAcademico.FechaInicio == null || thisAnioAcademico.FechaFin == null)
                return true;
            else
            {
                var result = _context.AniosAcademicos
                    .Where(t => t.FechaInicio.Value.Year == thisAnioAcademico.FechaInicio.Value.Year)
                    .Where(t => t.FechaFin.Value.Year == thisAnioAcademico.FechaFin.Value.Year)
                    .FirstOrDefault();

                if (result == null)
                {
                    if (thisAnioAcademico.FechaInicio <= thisAnioAcademico.FechaFin &&
                        thisAnioAcademico.FechaInicio.Value.Year == thisAnioAcademico.FechaFin.Value.Year)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if (thisAnioAcademico.Id == result.Id &&
                        thisAnioAcademico.FechaInicio <= thisAnioAcademico.FechaFin)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un Año Académico en la base de datos.
        /// </summary>
        /// <param name="thisAnioAcademico">Año Académico.</param>
        public void AddAnioAcademico(AnioAcademico thisAnioAcademico)
        {
            _context.AniosAcademicos.Add(thisAnioAcademico);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un Año Académico en la base de datos.
        /// </summary>
        /// <param name="anioAcademicoToUpdate">Año Académico con los datos actualizados.</param>
        /// <returns>Año Académico actualizado.</returns>
        public AnioAcademico UpdateAnioAcademico(AnioAcademico anioAcademicoToUpdate)
        {
            var thisAnioAcademico = GetAnioAcademicoById(anioAcademicoToUpdate.Id);
            thisAnioAcademico.Nombre = anioAcademicoToUpdate.Nombre;
            thisAnioAcademico.FechaInicio = anioAcademicoToUpdate.FechaInicio;
            thisAnioAcademico.FechaFin = anioAcademicoToUpdate.FechaFin;

            _context.Update(thisAnioAcademico);

            return (thisAnioAcademico);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Año Académico en la base de datos.
        /// </summary>
        /// <param name="anioAcademicoToDelete">Año Académico.</param>
        /// <returns>Año Académico eliminado.</returns>
        public AnioAcademico DeleteAnioAcademico(AnioAcademico anioAcademicoToDelete)
        {
            var thisAnioAcademico = GetAnioAcademicoById(anioAcademicoToDelete.Id);
            thisAnioAcademico.Estado = "0";

            _context.Update(thisAnioAcademico);

            return (thisAnioAcademico);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para obtener los Cronogramas de Matrícula.
        /// </summary>
        /// <returns>Lista de Cronogramas Académicos.</returns>
        public IEnumerable<CronogramaMatricula> GetAllCronogramasMatriculas()
        {
            return _context.CronogramasMatricula
                .Where(t => t.Estado == "1")
                .Include(t => t.AnioAcademico)
                .ToList();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para obtener los Cronogramas de Matrícula de un Año Académico específico a través de su Id.
        /// </summary>
        /// <param name="idAnioAcademico">Id del Año Académico.</param>
        /// <returns>Lista de Cronogramas Académicos del Año Académico.</returns>
        public IEnumerable<CronogramaMatricula> GetAllCronogramasMatriculasByAnioAcademicoId(int idAnioAcademico)
        {
            return _context.CronogramasMatricula
                .Include(t => t.AnioAcademico)
                .Where(t => t.AnioAcademico.Id == idAnioAcademico)
                .ToList();
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para obtener el Cronograma de Matrícula.
        /// </summary>
        /// <param name="idAnioAcademico">Año Académico.</param>
        /// <param name="nombre">Nombre del Cronograma de Matrícula.</param>
        /// <returns></returns>
        public CronogramaMatricula GetCronogramaMatriculaById(int idAnioAcademico, string nombre)
        {
            return _context.CronogramasMatricula
                .Include(t => t.AnioAcademico)
                .Where(t => t.AnioAcademicoId == idAnioAcademico)
                .Where(t => t.Nombre == nombre)
                .FirstOrDefault();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si un nombre de Cronograma de Matrícula ya está registrado en la base de datos.
        /// </summary>
        /// <param name="thisCronogramaMatricula">Cronograma de Matrícula.</param>
        /// <returns>Estado del Nombre del Cronograma de Matrícula.</returns>
        public bool IsNombreValido(CronogramaMatricula thisCronogramaMatricula)
        {
            try
            {
                var result = GetAllCronogramasMatriculasByAnioAcademicoId(thisCronogramaMatricula.AnioAcademicoId)
                    .Where(t => t.Estado == "1")
                    .Where(t => t.Nombre.ToLower() == thisCronogramaMatricula.Nombre.ToLower())
                    .FirstOrDefault();

                if (result == null)
                {
                    return true;
                }
                else
                {
                    if (thisCronogramaMatricula.AnioAcademicoId == result.AnioAcademicoId &&
                        thisCronogramaMatricula.Nombre == result.Nombre)
                        return true;
                    else
                        return false;
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError(e.Message);
                return true;
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para determinar si las fechas del Cronograma de Matrícula sean del mismo año que al Año Académico.
        /// </summary>
        /// <param name="thisCronogramaMatricula">Cronograma de Matrícula.</param>
        /// <returns>Estado de las Fechas del Cronograma de Matrícula.</returns>
        public bool IsFechasValidas(CronogramaMatricula thisCronogramaMatricula)
        {
            if (thisCronogramaMatricula.FechaInicio == null && thisCronogramaMatricula.FechaFin == null)
                return true;
            else
            {
                var currentAnioAcademico = GetAnioAcademicoById(thisCronogramaMatricula.AnioAcademicoId);

                if (thisCronogramaMatricula.FechaInicio <= thisCronogramaMatricula.FechaFin &&
                    currentAnioAcademico.FechaInicio.Value.Year == thisCronogramaMatricula.FechaInicio.Value.Year &&
                    currentAnioAcademico.FechaFin.Value.Year == thisCronogramaMatricula.FechaFin.Value.Year)
                    return true;
                else
                    return false;
            }
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para agregar un Cronograma de Matrícula en la base de datos.
        /// </summary>
        /// <param name="thisCronogramaMatricula">Cronograma de Matrícula.</param>
        public void AddCronogramaMatricula(CronogramaMatricula thisCronogramaMatricula)
        {
            _context.CronogramasMatricula.Add(thisCronogramaMatricula);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para actualizar un Cronograma de Matrícula en la base de datos.
        /// </summary>
        /// <param name="cronogramaMatriculaToUpdate">Cronograma de Matrícula con los datos actualizados.</param>
        /// <returns>Cronograma de Matrícula actualizado.</returns>
        public CronogramaMatricula UpdateCronogramaMatricula(CronogramaMatricula cronogramaMatriculaToUpdate)
        {
            var thisCronogramaMatricula = GetCronogramaMatriculaById(cronogramaMatriculaToUpdate.AnioAcademico.Id, cronogramaMatriculaToUpdate.Nombre);
            thisCronogramaMatricula.FechaInicio = cronogramaMatriculaToUpdate.FechaInicio;
            thisCronogramaMatricula.FechaFin = cronogramaMatriculaToUpdate.FechaFin;

            _context.Update(thisCronogramaMatricula);

            return (thisCronogramaMatricula);
        }

        /// <author>Luis Fernando Yana Espinoza</author>
        /// <summary>
        /// Método para eliminar lógicamente un Cronograma de Matrícual en la base de datos.
        /// </summary>
        /// <param name="cronogramaMatriculaToDelete">Cronograma de Matrícula.</param>
        /// <returns>Cronograma de Matrícula eliminado.</returns>
        public CronogramaMatricula DeleteCronogramaMatricula(CronogramaMatricula cronogramaMatriculaToDelete)
        {
            var thisCronogramaMatricula = GetCronogramaMatriculaById(cronogramaMatriculaToDelete.AnioAcademico.Id, cronogramaMatriculaToDelete.Nombre);
            thisCronogramaMatricula.Estado = "0";

            _context.Update(thisCronogramaMatricula);

            return (thisCronogramaMatricula);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para registrar una matrícula.
        /// </summary>
        /// <param name="idAlumno">Id del alumno.</param>
        /// <param name="idColaborador">Id del registrador.</param>
        /// <returns>Estado del proceso de matrícula.</returns>
        /// <returns>0 Error.</returns>
        /// <returns>1 Matrícula correcta.</returns>
        /// <returns>2 Secciones no disponibles.</returns>
        /// <returns>3 Matrícula ya registrada.</returns>
        public int RegistrarMatricula(int idAlumno, int idColaborador)
        {
            var alumno = GetAlumnoById(idAlumno);
            DateTime fechaActual = new DateTime(2017, 3, 10);

            var anioAcademico = GetAllAniosAcademicos()
                .Where(t => t.FechaInicio.Value.Year == fechaActual.Year)
                .FirstOrDefault();

            // Evalua si el alumno ya ha sido matriculado en el actual proceso de matríucla
            var aux = GetAllMatriculas()
                .Where(t => t.AnioAcademicoId == anioAcademico.Id)
                .Where(t => t.AlumnoId == idAlumno)
                .FirstOrDefault();

            if (aux != null)
                return 3;

            var cronograma = GetAllCronogramasMatriculas()
                .Where(t => t.FechaInicio <= fechaActual && t.FechaFin >= fechaActual)
                .FirstOrDefault();

            if (cronograma != null)
            {
                var nextGrado = GetNextGrado(idAlumno);
                
                var secciones = _context.Secciones.FromSql(String.Format("EXEC SP_SeccionesDisponibles @idGrado={0}, @idAnioAcademico={1}", nextGrado.Id, anioAcademico.Id)).ToList();

                if (secciones.Count == 0)
                    return 2;

                Random random = new Random();
                var seccionElegida = secciones.ElementAt(random.Next(secciones.Count));

                Matricula newMatricula = new Matricula()
                {
                    AlumnoId = idAlumno,
                    AnioAcademicoId = anioAcademico.Id,
                    Fecha = fechaActual,
                    RegistradorId = idColaborador,
                    SeccionId = seccionElegida.Id
                };
                _context.Matriculas.Add(newMatricula);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para obtener las Matrículas
        /// </summary>
        /// <returns>Lista de Matrículas.</returns>
        public IEnumerable<Matricula> GetAllMatriculas(){
            return _context.Matriculas.ToList();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para procesar las acciones en la base de datos
        /// </summary>
        /// <returns>Hilo de procesamiento.</returns>
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

								public IEnumerable<Alumno> GetAllAlumnos()
								{
												throw new NotImplementedException();
								}

								public Alumno GetAlumnoById(int idAlumno)
								{
												throw new NotImplementedException();
								}
				}
}
