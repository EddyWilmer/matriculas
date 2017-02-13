using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Luis Fernando Yana Espinoza</author>
    /// <summary>
    /// Clase que describe la entidad ProfesorCurso.
    /// Esta clase es usada para asociar cursos a profesores.
    /// </summary>
    public class ProfesorCurso
    {
        public int ProfesorId { get; set; }

        public Profesor Profesor { get; set; }

        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }
}
