using Microsoft.AspNetCore.Mvc;
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
    /// Clase para definir la entidad Año Académico que se mostrará en la vista.
    /// </summary>
    public class AnioAcademicoViewModel
    {
        public int Id { get; set; }

        [Remote("IsNombreAnioAcademicoUnique", "Validator", AdditionalFields = "Id")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression(@"^(\d{4})$", ErrorMessage = "Año no válido.")]
        public int Nombre { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime FechaFin { get; set; }

        public string Estado { get; set; }
    }
}
