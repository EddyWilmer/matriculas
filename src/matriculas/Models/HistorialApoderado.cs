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
    /// Clase que describe la entidad HistorialApoderado (Historial de Apoderado).
    /// Esta clase es usada para controlar el cambio de apoderado de un alumno.
    /// </summary>
    public class HistorialApoderado
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Apoderado")]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public DateTime? FechaInicio { get; set; }

        [StringLength(25)]
        public string ApellidoPaterno { get; set; }

        [StringLength(25)]
        public string ApellidoMaterno { get; set; }

        [StringLength(50)]
        public string Nombres { get; set; }

        [StringLength(8)]
        public string Dni { get; set; }

        [StringLength(1)]
        public string Sexo { get; set; }

        [StringLength(1)]
        public string EstadoCivil { get; set; }

        [StringLength(1)]
        public virtual Alumno Alumno { get; set; }

        public DateTime? FechaFin { get; set; }

        public virtual Apoderado Apoderado { get; set; }
    }
}
