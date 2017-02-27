using AutoMapper;
using Matriculas.Models;
using Matriculas.Queries.Core.Repositories;
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
    [Route("api/v2/[controller]")]
    public class ColaboradoresController : Controller
    {
        private ILogger<ColaboradoresController> _logger;
        private IAppRepository _repository;
        private UserManager<ApplicationUser> _userManager;

        public ColaboradoresController(IAppRepository repository, ILogger<ColaboradoresController> logger, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet()]
        public IActionResult GetAllColaboradores()
        {
            try
            {
                _logger.LogInformation("Recuperando la lista de colaboradores.");
                var result = _repository.Colaboradores.GetAll();
                return Ok(Mapper.Map<IEnumerable<ColaboradorViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar los colaboradores: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }          
        }

        [HttpGet("{id}")]
        public IActionResult GetColaborador(int id)
        {
            try
            {
                _logger.LogInformation("Recuperando la información del colaborador.");
                var result = _repository.Colaboradores.Get(id);
                return Ok(Mapper.Map<ColaboradorViewModel>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"No se pudo recuperar la información del colaborador: {ex}");
                return BadRequest("No se pudo recuperar la información.");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostColaborador([FromBody] ColaboradorViewModel colaboradorDetails)
        {
            _logger.LogInformation("Agregando al colaborador.");

            var colaborador = Mapper.Map<Colaborador>(colaboradorDetails);

            if (ModelState.IsValid)
            {
                _repository.Colaboradores.Add(colaborador);
                if (await _repository.Complete())
                {
                    var user = new ApplicationUser()
                    {
                        UserName = colaboradorDetails.Email,
                        Email = colaboradorDetails.Email + "@trilce.edu.pe",
                        ColaboradorId = colaborador.Id
                    };
                    await _userManager.CreateAsync(user, colaboradorDetails.Dni);
                    await _userManager.AddToRoleAsync(user, colaboradorDetails.Rol.Nombre);
                    await _userManager.GeneratePasswordResetTokenAsync(user);

                    return Created($"api/usuarios/{colaborador.Id}", Mapper.Map<ColaboradorViewModel>(colaborador));
                }
            }

            _logger.LogError("No se pudo agregar al colaborador.");
            return BadRequest(ModelState);
        }

        [HttpPut()]
        public async Task<IActionResult> PutColaborador([FromBody] ColaboradorViewModel colaboradorDetails)
        {
            _logger.LogInformation("Actualizando los datos del colaborador.");

            var colaborador = Mapper.Map<Colaborador>(colaboradorDetails);

            if (ModelState.IsValid)
            {
                _repository.Colaboradores.Update(colaborador);
                if (await _repository.Complete())
                {
                    return Created($"api/colaboradores/{colaborador.Id}", Mapper.Map<ColaboradorViewModel>(colaborador));
                }
            }

            _logger.LogError("No se pudo actualizar el colaborador.");
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            _logger.LogInformation("Eliminando el colaborador.");

            _repository.Colaboradores.Delete(id);
            if (await _repository.Complete())
            {
                return Created($"api/colaboradores/{id}", Mapper.Map<ColaboradorViewModel>(_repository.Colaboradores.Get(id)));
            }

            _logger.LogError("No se pudo eliminar el colaborador.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }

        [HttpPost("{id}/toggleStatus")]
        public async Task<IActionResult> ToggleEstadoUsuario(int id)
        {
            _logger.LogInformation("Cambiando el estado de la cuenta de usuario del colaborador.");

            _repository.Colaboradores.ToggleEstado(id);

            if (await _repository.Complete())
            {
                return Created($"api/colaboradores/{id}", Mapper.Map<ColaboradorViewModel>(_repository.Colaboradores.Get(id)));
            }

            _logger.LogError("No se pudo cambiar el estado de la cuenta de usuario del colaborador.");
            return BadRequest("No se pudo eliminar este colaborador.");
        }

        [HttpPost("{id}/resetPassword")]
        public async Task<IActionResult> ResetPasswordUsuario(int id)
        {
            _logger.LogInformation("No se pudo cambiar el estado de la cuenta de usuario del colaborador.");

            _repository.Colaboradores.ResetPassword(id, _userManager);

            if (await _repository.Complete())
            {
                return Created($"api/colaboradores/{id}", Mapper.Map<ColaboradorViewModel>(_repository.Colaboradores.Get(id)));
            }

            _logger.LogError("No se pudo restaurar la contraseña.");
            return BadRequest("No se pudo restuaurar la contraseña del colaborador.");
        }
    }
}
