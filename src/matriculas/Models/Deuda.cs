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
    /// Clase que describe la entidad Deuda.
    /// Esta clase es usada para describir la pensión mensual que paga un alumno.
    /// </summary>
    public class Deuda
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Matricula")]
        public int MatriculaId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int Mes { get; set; }

        public double Monto { get; set; }

        public virtual Matricula Matricula { get; set; }
    }
}
