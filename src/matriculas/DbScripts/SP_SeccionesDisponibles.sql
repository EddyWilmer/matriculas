/*
Procedimiento almacenado para recuperar las secciones disponibles de un grado tomando en cuenta la capacidad.
*/
CREATE PROCEDURE SP_SeccionesDisponibles
	@idGrado INT,
	@idAnioAcademico INT
AS
	SELECT s.*
	FROM secciones s
	INNER JOIN Grados g ON s.GradoId = g.Id
	LEFT JOIN (SELECT s.Id, COUNT(m.SeccionId) NroMatriculados
				FROM secciones s
				LEFT JOIN Matriculas m ON s.Id = m.SeccionId	
				WHERE m.AnioAcademicoId = @idAnioAcademico 
				GROUP BY s.Id) c ON s.Id = c.Id
	WHERE (c.NroMatriculados < g.Capacidad OR NroMatriculados IS NULL)
	AND g.Id = @idGrado;