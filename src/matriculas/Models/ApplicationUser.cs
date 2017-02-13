using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    /// Clase que describe la entidad ApplicationUser (Usuario de la aplicación).
    /// Esta clase es usada para la administración de cuentas de usuario de la aplicación.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Colaborador")]
        public int ColaboradorId { get; set; }

        public virtual Colaborador Colaborador { get; set; }
    }
}
