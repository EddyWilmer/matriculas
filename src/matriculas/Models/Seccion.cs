using Matriculas.Models;
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
    /// Clase que define la entidad Sección.
    /// </summary>
    public class Seccion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(1)]
        public string Nombre { get; set; }

        public virtual Grado Grado { get; set; }

        [StringLength(1)]
        public string Estado { get; set; }
    }
}
