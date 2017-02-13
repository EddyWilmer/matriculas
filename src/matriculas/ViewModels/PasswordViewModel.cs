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
    /// Clase para definir la entidad Password que se mostrará en la vista.
    /// </summary>
    public class PasswordViewModel
    {
        [RegularExpression("[$@$!%*?&a-zA-Z0-9]{8,15}", ErrorMessage = "La contraseña no es válida.")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string CurrentPassword { get; set; }

        [RegularExpression("[$@$!%*?&a-zA-Z0-9]{8,15}", ErrorMessage = "La contraseña no es válida.")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string NewPassword { get; set; }

        [RegularExpression("[$@$!%*?&a-zA-Z0-9]{8,15}", ErrorMessage = "La contraseña no es válida.")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public string RepeatNewPassword { get; set; }
    }
}
