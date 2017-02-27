using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Luis Fernando Yana Espinoza</author>
    /// <summary>
    /// Clase para definir la entidad Cronograma de Matrícula que se mostrará en la vista.
    /// </summary>
    public class CronogramaViewModel
    {
        public int AnioAcademicoId { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[a-zñáéíóúA-ZÑÁÉÍÓÚ0-9 ]{2,30}", ErrorMessage = "Este campo debe contener entre 2 y 30 letras.")]
        public string Nombre { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaFin { get; set; }    
         
        public virtual AnioAcademico AnioAcademico { get; set; }

        public string Estado { get; set; }
    }
}
