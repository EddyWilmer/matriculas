using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    public class AnioAcademico
    {
        /// <author>Eddy Wilmer Canaza Tito</author>
        /// <summary>
        /// Clase que describe la entidad AnioAcademico (Año Académico).
        /// </summary>
        [Key]
        public int Id { get; set; }

        public int Nombre { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "DATE")]
        public DateTime? FechaFin { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }
    }
}
