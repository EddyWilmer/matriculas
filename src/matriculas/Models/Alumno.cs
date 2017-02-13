using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    public class Alumno
    {
        /// <author>Julissa Zaida Huaman Hilari</author>
        /// <summary>
        /// Clase que describe la entidad Alumno.
        /// </summary>
        [Key]
        public int Id { get; set; }
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
        [StringLength(70)]
        public string Direccion { get; set; }
        [Column(TypeName = "DATE")]
        public DateTime FechaNacimiento { get; set; }
        public virtual Apoderado Apoderado { get; set; }
        [StringLength(1)]
        public string Estado { get; set; }
    }
}
