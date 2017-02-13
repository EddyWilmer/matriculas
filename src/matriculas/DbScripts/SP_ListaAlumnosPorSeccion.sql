/*
Procedimiento almacenado para listar los alumnos de la sección del año académico actual.
*/
CREATE PROCEDURE SP_ListaAlumnosPorSeccion
	@idAnioAcademico INT,
	@idSeccion INT
AS
	SELECT a.*
	FROM Alumnos a
	INNER JOIN Matriculas m ON a.Id = m.AlumnoId
	WHERE m.AnioAcademicoId = @idAnioAcademico
	AND m.SeccionId = @idSeccion
	ORDER BY ApellidoPaterno, ApellidoMaterno, Nombres;