using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Luis Fernando Yana Espinoza</author>
    /// <summary>
    /// Clase que describe la entidad Grado.
    /// </summary>
    public class Grado
    {
        [Key]
        public int Id { get; set; }

        [StringLength(25)]
        public string Nombre { get; set; }

        public virtual Nivel Nivel { get; set; }

        public virtual Grado GradoRequisito { get; set; }

        public int Capacidad { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }
    }
}
