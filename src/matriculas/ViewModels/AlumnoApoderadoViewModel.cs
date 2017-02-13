using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase para definir la entidad Alumno y Apoderado que se mostrará en la vista.
    /// </summary>
    public class AlumnoApoderadoViewModel
    {
        public AlumnoViewModel Alumno { get; set; }
        public ApoderadoViewModel Apoderado { get; set; }
    }
}
