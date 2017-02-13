using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que describe la entidad Nota.
    /// </summary>
    public class Nota
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Matricula")]
        public int MatriculaId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Curso")]
        public int CursoId { get; set; }

        public int? Calificacion { get; set; }

        public virtual Matricula Matricula { get; set; }

        public virtual Curso Curso { get; set; }
    }
}
