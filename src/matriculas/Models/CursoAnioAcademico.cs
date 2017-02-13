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
    /// Clase que describe la entidad CursoAnioAcademico (Curso del Año Académico).
    /// Esta clase es usada para describir los cursos que se dictan en un año académico específico.
    /// </summary>
    public class CursoAnioAcademico
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("AnioAcademico")]
        public int AnioAcademicoId { get; set; }

        [Key]
        [Column(Order = 1)]
        [ForeignKey("Curso")]
        public int CursoId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Grado")]
        public int GradoId { get; set; }

        [ForeignKey("Profesor")]
        public int? ProfesorId { get; set; }

        public virtual AnioAcademico AnioAcademico { get; set; }

        public virtual Curso Curso { get; set; }

        public virtual Grado Grado { get; set; }

        public virtual Profesor Profesor { get; set; }
    }
}
