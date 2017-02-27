using Matriculas.Models;
using Matriculas.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Controllers
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que define el controlador de accesos a los módulos del sistema
    /// </summary>
    public class AuthController : Controller
    {
        private SignInManager<ApplicationUser> _signInManager;
        private MatriculasContext _context;
        private UserManager<ApplicationUser> _userManager;

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Constructor de la clase AuthController
        /// </summary>
        /// <param name="signInManager">Administrador de registro.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        /// <param name="repository">Instancia del repositorio.</param>
        /// <param name="context">Contexto de la aplicación.</param>
        public AuthController(SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager,
            MatriculasContext context)
        {
            _signInManager = signInManager;
            _context = context;
            _userManager = userManager;
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redigir al Login de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método asíncrono para identificar si el usuario tiene acceso al sistema.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var a = _userManager.FindByNameAsync(vm.UserName).Result;
                if (a != null && a.LockoutEnabled == true)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, true);

                    if (signInResult.Succeeded)
                    {
                        if (string.IsNullOrWhiteSpace(returnUrl))
                        {
                            return RedirectToAction("Index", "App");
                        }
                        else
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("UsernamePasswordIncorrecto", "Username o password incorrectos.");
                    }
                }
                else if(a != null && a.LockoutEnabled == false)
                {
                    ModelState.AddModelError("CuentaBloqueada", "Cuenta suspendida.");
                }
                else
                {
                    ModelState.AddModelError("UsernamePasswordIncorrecto", "Usuario no registrado.");
                }
            }
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para cerrar sesión del usuario.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        public async Task<ActionResult> LogOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction("Login", "Auth");
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redigir al área de cambio de contraseña de la aplicación.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Método para redigir al identificar si el cambio de contraseña es válido.
        /// </summary>
        /// <returns>Acción con la respuesta.</returns>
        [HttpPost]
        public async Task<ActionResult> ChangePassword(PasswordViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(this.User.Identity.Name).Result;

                if (vm.NewPassword != vm.RepeatNewPassword)
                    ModelState.AddModelError("PasswordDiferentes", "Los nuevos passwords no coinciden.");

                var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, vm.CurrentPassword, false, true);
                if (!signInResult.Succeeded)
                {
                    ModelState.AddModelError("PasswordActual", "El password actual no coincide.");
                }
                if (ModelState.IsValid)
                {
                    var result = await _userManager.ChangePasswordAsync(user, vm.CurrentPassword, vm.NewPassword);
                    await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (User.Identity.IsAuthenticated)
                        {
                            await _signInManager.SignOutAsync();
                        }
                        return RedirectToAction("Login", "Auth");
                    }
                }
            }
            return View();
        }
    }
}
