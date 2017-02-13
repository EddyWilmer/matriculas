/*
Procedimiento almacenado para agregar un curso a un profesor.
*/
CREATE PROCEDURE SP_AddProfesorCurso
	@idProfesor INT,
	@idCurso INT
AS
	INSERT INTO ProfesorCursos VALUES(@idProfesor, @idCurso);