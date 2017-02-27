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
    /// Clase que describe la entidad Colaborador.
    /// </summary>
    public class Colaborador
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

        public virtual Cargo Rol { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(9)]
        public string Celular { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }
    }
}
