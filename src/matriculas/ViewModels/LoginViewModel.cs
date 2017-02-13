using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase para definir la entidad Login que se mostrará en la vista.
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string Password { get; set; }
    }
}
