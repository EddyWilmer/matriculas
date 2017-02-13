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
    /// Clase que describe la entidad Historial Alumno.
    /// Esta clase es usada para controlar el cambio de estado de un alumno.
    /// </summary>
    public class HistorialAlumno
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Alumno")]
        public int AlumnoId { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime? FechaIngreso { get; set; }

        public DateTime? FechaRetiro { get; set; }

        public virtual Alumno Alumno { get; set; }
    }
}
