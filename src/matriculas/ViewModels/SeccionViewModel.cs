using Matriculas.Models;
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
    /// Clase para definir la entidad Sección que se mostrará en la vista.
    /// </summary>
    public class SeccionViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[A-Z]{1}", ErrorMessage = "Este campo debe contener una letra mayúscula.")]
        public string Nombre { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio.")]
        public virtual Grado Grado { get; set; }

        public string Estado { get; set; }
    }
}
