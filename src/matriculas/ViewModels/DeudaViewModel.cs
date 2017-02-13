using Matriculas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matriculas.ViewModels
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase para definir la entidad Deuda que se mostrará en la vista.
    /// </summary>
    public class DeudaViewModel
    {
        public int MatriculaId { get; set; }
        public int Mes { get; set; }
        public double Monto { get; set; }
        public virtual Matricula Matricula { get; set; }
    }
}
