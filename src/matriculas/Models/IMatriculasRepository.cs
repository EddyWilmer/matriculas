using System.Collections.Generic;
using System.Threading.Tasks;

namespace Matriculas.Models
{
    /// <author>Eddy Wilmer Canaza Tito</author>
    /// <summary>
    /// Clase que describe la interfaz de la Clase MatriculasRepository.
    /// Esta clase es usada para proporcionarle acceso a clases externas mediante inyecciones.
    /// </summary>
    public interface IMatriculasRepository
    {
        // Métodos de la entidad Colaborador
        IEnumerable<Colaborador> GetAllColaboradores();
        Colaborador GetColaboradorById(int idColaborador);
        bool IsDniValido(Colaborador thisColaborador);
        bool IsEmailValido(Colaborador thisColaborador);
        Colaborador AddColaborador(Colaborador thisColaborador);
        Colaborador UpdateColaborador(Colaborador colaboradorToUpdate);
        void DeleteColaborador(Colaborador colaboradorToDelete);
        Colaborador ChangeEstadoUsuario(Colaborador colaboradorToChangeEstado);

        // Métodos de la entidad Rol
        IEnumerable<Rol> GetAllRoles();
        Rol GetRolById(int idRol);     

        // Métodos de la entidad Grado
        IEnumerable<Grado> GetAllGrados();
        Grado GetGradoById(int idGrado);
        IEnumerable<Curso> GetCursosGradoById(int idGrado);
        bool IsNombreValido(Grado thisGrado);
        bool IsEliminable(Grado thisGrado);
        void AddGrado(Grado thisGrado);
        Grado UpdateGrado(Grado gradoToUpdate);
        void DeleteGrado(Grado gradoToDelete);

        // Métodos de la entidad Nivel
        IEnumerable<Nivel> GetAllNiveles();
        Nivel GetNivelById(int idNivel);

        // Métodos de la entidad Seccion
        IEnumerable<Seccion> GetAllSecciones();
        Seccion GetSeccionById(int idSeccion);
        bool IsNombreValido(Seccion thisSeccion);
        void AddSeccion(Seccion thisSeccion);
        Seccion UpdateSeccion(Seccion seccionToUpdate);
        void DeleteSeccion(Seccion seccionToDelete);
        IEnumerable<Alumno> GetListaAlumnosByIdSeccion(int idSeccion);

        // Métodos de la entidad Curso
        IEnumerable<Curso> GetAllCursos();
        Curso GetCursoById(int idCurso);
        bool IsNombreValido(Curso thisCurso);
        void AddCurso(Curso thisCurso);
        Curso UpdateCurso(Curso cursoToUpdate);
        void DeleteCurso(Curso cursoToDelete);
        bool ExcedeMaximoHoras(Curso thisCurso);
        IEnumerable<Profesor> GetProfesoresCursoById(int idCurso);
        bool UpdateCursoProfesor(Curso cursoToUpdate, Profesor thisProfesor);
        Profesor GetProfesorByIdCurso(int idCurso);

        // Métodos de la entidad Profesor
        IEnumerable<Profesor> GetAllProfesores();
        Profesor GetProfesorById(int idProfesor);
        bool IsDniValido(Profesor thisProfesor);
        bool IsEliminable(Profesor thisProfesor);
        IEnumerable<Curso> GetCursosByIdProfesor(int idProfesor);
        void AddProfesor(Profesor thisProfesor, IEnumerable<Curso> cursos);
        Profesor UpdateProfesor(Profesor profesorToUpdate, IEnumerable<Curso> cursos);
        void DeleteProfesor(Profesor profesorToDelete);       

        // Métodos de la entidad Alumno
        IEnumerable<Alumno> GetAllAlumnos();
        Alumno GetAlumnoById(int idAlumno);
        Alumno GetAlumnoByDni(string dni);
        Matricula GetLastMatricula(int idAlumno);
        Grado GetNextGrado(int idAlumno);
        IEnumerable<Nota> GetNotasLastMatricula(int idAlumno);
        IEnumerable<Deuda> GetDeudasLastMatricula(int idAlumno);
        bool IsDniValido(Alumno thisAlumno);
        void AddAlumno(Alumno thisAlumno);
        Alumno UpdateAlumno(Alumno alumnoToUpdate);     
        void DeleteAlumno(Alumno alumnoToDelete);

        // Métodos de la entidad Apoderado
        IEnumerable<Apoderado> GetAllApoderados();
        Apoderado GetApoderadoById(int idApoderado);
        bool IsDniValido(Apoderado thisApoderado);
        Apoderado UpdateApoderado(Apoderado apoderadoToUpdate);

        // Métodos de la entidad AniosAcademicos
        IEnumerable<AnioAcademico> GetAllAniosAcademicos();
        AnioAcademico GetAnioAcademicoById(int idAnioAcademico);
        bool IsNombreValido(AnioAcademico thisAnioAcademico);
        bool IsFechasValidas(AnioAcademico thisAnioAcademico);
        void AddAnioAcademico(AnioAcademico thisAnioAcademico);
        AnioAcademico UpdateAnioAcademico(AnioAcademico anioAcademicoToUpdate);
        AnioAcademico DeleteAnioAcademico(AnioAcademico anioAcademicoToDelete);

        // Métodos de la entidad CronogramasMatricula
        IEnumerable<CronogramaMatricula> GetAllCronogramasMatriculas();
        IEnumerable<CronogramaMatricula> GetAllCronogramasMatriculasByAnioAcademicoId(int idAnioAcademico);
        CronogramaMatricula GetCronogramaMatriculaById(int idAnioAcademico, string nombre);
        bool IsNombreValido(CronogramaMatricula thisCronogramaMatricula);
        bool IsFechasValidas(CronogramaMatricula thisCronogramaMatricula);
        void AddCronogramaMatricula(CronogramaMatricula thisCronogramaMatricula);
        CronogramaMatricula UpdateCronogramaMatricula(CronogramaMatricula cronogramaMatriculaToUpdate);
        CronogramaMatricula DeleteCronogramaMatricula(CronogramaMatricula cronogramaMatriculaToDelete);

        // Métodos de la entidad Matricula
        int RegistrarMatricula(int idAlumno, int idColaborador);
        IEnumerable<Matricula> GetAllMatriculas();

        Task<bool> SaveChangesAsync();
    }
}