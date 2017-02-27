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
    /// Clase para definir la entidad Rol que se mostrará en la vista.
    /// </summary>
    public class CargoViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "El campo Nombres es obligatorio.")]
        [StringLength(25, ErrorMessage = "El campo Nombre debe contener como máximo 25 caracteres.")]
        public string Nombre { get; set; }
    }
}
