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
    /// Clase que describe la entidad Matricula (Matrícula).
    /// </summary>
    public class Matricula
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Alumno")]
        public int AlumnoId { get; set; }

        [ForeignKey("Seccion")]
        public int SeccionId { get; set; }

        [ForeignKey("Registrador")]
        public int RegistradorId { get; set; }

        [ForeignKey("AnioAcademico")]
        public int AnioAcademicoId { get; set; }

        public DateTime? Fecha { get; set; }

        public virtual Alumno Alumno { get; set; }

        [NotMapped]
        public virtual Grado Grado { get; set; }

        public virtual Seccion Seccion { get; set; }

        public virtual Colaborador Registrador { get; set; }

        public virtual AnioAcademico AnioAcademico{ get; set; }
    }
}
