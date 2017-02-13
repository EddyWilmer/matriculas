using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase para definir la entidad CursoProfesor que se mostrará en la vista.
    /// </summary>
    public class CursoProfesorViewModel
    {
        public CursoViewModel Curso { get; set; }
        public ProfesorViewModel Profesor { get; set; }
    }
}
