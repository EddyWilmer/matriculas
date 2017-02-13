using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase para definir la entidad Curso que se mostrará en la vista.
    /// </summary>
    public class CursoViewModel
    {
        //[Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[a-zñáéíóúA-ZÑÁÉÍÓÚ0-9 ]{2,25}", ErrorMessage = "Este campo debe contener entre 2 y 25 letras.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public virtual Grado Grado { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("([1-8])", ErrorMessage = "Este campo debe estar en el rango del 1 al 8.")]
        public int HorasAcademicas { get; set; }

        public string Estado { get; set; }
    }
}
