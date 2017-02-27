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
    /// <author>Luis Fernando Yana Espinoza</author>
    /// <summary>
    /// Clase para definir la entidad Grado que se mostrará en la vista.
    /// </summary>
    public class GradoViewModel
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[a-zñáéíóúA-ZÑÁÉÍÓÚ0-9 ]{2,25}", ErrorMessage = "Este campo debe contener entre 2 y 25 letras.")]
        public string Nombre { get; set; }

        [Required (ErrorMessage = "Este campo es obligatorio.")]
        public virtual Nivel Nivel { get; set; }

        public virtual Grado GradoRequisito { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("(([1-2][0-9])|(30))", ErrorMessage = "Este campo debe estar en el rango de 10 al 30.")]
        public int Capacidad { get; set; }

        public string Estado { get; set; }
    }
}