using AutoMapper;
using Matriculas.Models;
using Matriculas.ViewModels;
using Microsoft.AspNetCore.Identity;
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
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que permite la interacción de las vistas con la entidad Colaboradores.
    /// </summary>
    public class ColaboradoresController : Controller
    {
        private ILogger<ColaboradoresController> _logger;
        private IMatriculasRepositorys _repository;
        private UserManager<ApplicationUser> _userManager;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase ColaboradoresController.
        /// </summary>
        /// <param name="repository">Instancia del respositorio.</param>
        /// <param name="logger">Administrador de logging.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        public ColaboradoresController(IMatriculasRepositorys repository, ILogger<ColaboradoresController> logger, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para recuperar la lista de Colaboradores activos.
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/colaboradores")]
        public IActionResult GetColaboradores()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de colaboradores.");
                var result = _repository.GetAllColaboradores();
                return Ok(Mapper.Map<IEnumerable<ColaboradorViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los colaboradores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }          
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        ///  Método para recuperar un Colaborador específico a través de su Id.
        /// </summary>
        /// <param name="idColaborador">Id del Colaborador.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/colaboradores/{idColaborador}")]
        public IActionResult GetColaboradorEspecifico(int idColaborador)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del colaborador.");
                var result = _repository.GetColaboradorById(idColaborador);
                return Ok(Mapper.Map<ColaboradorViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del colaborador: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para agregar un Colaborador en la base de datos.
        /// </summary>
        /// <param name="thisColaborador">Colaborador.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/colaboradores/crear")]
        public async Task<IActionResult> PostCrearColaborador([FromBody] ColaboradorViewModel thisColaborador)
        {
            _logger.LogInformation("Agregando al colaborador.");

            if (!_repository.IsDniValido(Mapper.Map<Colaborador>(thisColaborador)))
                ModelState.AddModelError("dniMessageValidation", "Este DNI ya fue registrado.");

            if (!_repository.IsEmailValido(Mapper.Map<Colaborador>(thisColaborador)))
                ModelState.AddModelError("emailMessageValidation", "Este correo electrónico ya fue registrado.");

            if (ModelState.IsValid)
            {
                var newColaborador = Mapper.Map<Colaborador>(thisColaborador);

                var colaboradorCreated = _repository.AddColaborador(newColaborador);
                if (await _repository.SaveChangesAsync())
                {
                    var user = new ApplicationUser()
                    {
                        UserName = thisColaborador.Email,
                        Email = thisColaborador.Email + "@trilce.edu.pe",
                        ColaboradorId = colaboradorCreated.Id
                    };
                    await _userManager.CreateAsync(user, thisColaborador.Dni);
                    await _userManager.AddToRoleAsync(user, thisColaborador.Rol.Nombre);
                    await _userManager.GeneratePasswordResetTokenAsync(user);

                    return Created($"api/usuarios/{colaboradorCreated.Id}", Mapper.Map<ColaboradorViewModel>(colaboradorCreated));
                }
            }

            _logger.LogError("No se pudo agregar al colaborador.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para actualizar un colaborador en la base de datos.
        /// </summary>
        /// <param name="thisColaborador">Colaborador con los datos actualizados.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/colaboradores/editar")]
        public async Task<IActionResult> PostEditarColaborador([FromBody] ColaboradorViewModel thisColaborador)
        {
            _logger.LogInformation("Actualizando los datos del colaborador.");

            if (!_repository.IsDniValido(Mapper.Map<Colaborador>(thisColaborador)))
                ModelState.AddModelError("dniMessageValidation", "Este DNI ya fue registrado.");

            if (!_repository.IsEmailValido(Mapper.Map<Colaborador>(thisColaborador)))
                ModelState.AddModelError("emailMessageValidation", "Este correo electrónico ya fue registrado.");

            if (ModelState.IsValid)
            {
                var colaboradorToUpdate = Mapper.Map<Colaborador>(thisColaborador);
                
                var updatedColaborador = _repository.UpdateColaborador(colaboradorToUpdate);               
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/colaboradores/{updatedColaborador.Id}", Mapper.Map<ColaboradorViewModel>(updatedColaborador));
                }                         
            }

            _logger.LogError("No se pudo actualizar el colaborador.");
            return BadRequest(ModelState);
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para eliminar lógicamente un Colaborador en la base de datos.
        /// </summary>
        /// <param name="thisColaborador">Colaborador.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost("api/colaboradores/eliminar")]
        public async Task<IActionResult> PostEliminarColaborador([FromBody] ColaboradorViewModel thisColaborador)
        {
            _logger.LogInformation("Eliminando el colaborador.");

            var result = _repository.GetColaboradorById(thisColaborador.Id);

            var colaboradorToDelete = Mapper.Map<Colaborador>(result);

            _repository.DeleteColaborador(colaboradorToDelete);
            if (await _repository.SaveChangesAsync())
            {
                return Ok("Se eliminó el colaborador correctamente.");
            }

            _logger.LogError("No se pudo eliminar el colaborador.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para cambiar el estado de la cuenta de usuario de un Colaborador.
        /// </summary>
        /// <param name="idColaborador">Id del Colaborador.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/colaboradores/cambiar/{idColaborador}")]
        public async Task<IActionResult> ChangeEstadoUsuario(int idColaborador)
        {
            _logger.LogInformation("Cambiando el estado de la cuenta de usuario del colaborador.");

            var result = _repository.GetColaboradorById(idColaborador);

            var colaboradorToChangeEstado = Mapper.Map<Colaborador>(result);

            var updatedColaborador = _repository.ChangeEstadoUsuario(colaboradorToChangeEstado);
            if (await _repository.SaveChangesAsync())
            {
                return Created($"api/colaboradores/{updatedColaborador.Id}", Mapper.Map<ColaboradorViewModel>(updatedColaborador));
            }

            _logger.LogError("No se pudo cambiar el estado de la cuenta de usuario del colaborador.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para restaurar la contraseña la cuenta de usuario de un Colaborador.
        /// </summary>
        /// <param name="idColaborador">Id del Colaborador.</param>
        /// <returns>Acción con la respuesta.</returns>
        [HttpGet("api/colaboradores/restaurar/{idColaborador}")]
        public IActionResult RestaurarPasswordColaborador(int idColaborador)
        {
            _logger.LogInformation("Restaurando el password de la cuenta de usuario del colaborador.");

            var colaborador = _repository.GetColaboradorById(idColaborador);
            var user = _userManager.Users.Where(t => t.ColaboradorId == idColaborador).FirstOrDefault();

            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = hasher.HashPassword(user, colaborador.Dni);
            _userManager.UpdateAsync(user);

            return Ok();
        }
    }
}
