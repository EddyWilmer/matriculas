using Matriculas.Models;
using Matriculas.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Julissa Zaida Huaman Hilari</author>
    /// <summary>
    /// Clase para definir la entidad Alumno que se mostrará en la vista.
    /// </summary>
    public class AlumnoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[a-zñáéíóúA-ZÑÁÉÍÓÚ ]{2,25}", ErrorMessage = "Este campo debe contener entre 2 y 25 letras.")]
        public string ApellidoPaterno { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[a-zñáéíóúA-ZÑÁÉÍÓÚ ]{2,25}", ErrorMessage = "Este campo debe contener entre 2 y 25 letras.")]
        public string ApellidoMaterno { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("[a-zñáéíóúA-ZÑÁÉÍÓÚ ]{2,50}", ErrorMessage = "Este campo debe contener entre 2 y 50 letras.")]
        [StringLength(50)]
        public string Nombres { get; set; }

        [Remote("IsDniAlumnoUnique", "Validator", AdditionalFields = "Id", HttpMethod = "Post")]
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [RegularExpression("([0-9]{8})", ErrorMessage = "Este campo debe contener 8 números.")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(1)]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio.")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "Este campo debe contener entre 2 y 70 caracteres.")]

        public string Direccion { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }

        [Required, ValidateObject]
        public virtual ApoderadoViewModel Apoderado { get; set; }

        public string Estado { get; set; }
    }
}
