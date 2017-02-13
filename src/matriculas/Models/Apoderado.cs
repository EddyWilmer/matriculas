using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Julissa Zaida Huamán Hilari</author>
    /// <summary>
    /// Clase que describe la entidad Apoderado.
    /// </summary>
    public class Apoderado
    {
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

        [StringLength(1)]
        public string EstadoCivil { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }
    }
}
