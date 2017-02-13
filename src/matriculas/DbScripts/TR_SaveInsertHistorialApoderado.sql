/*
Trigger para registrar el historial de la asignación de un apoderado a un alumno.
*/
CREATE TRIGGER TR_SaveInsertHistorialApoderado
ON Alumnos
AFTER INSERT
AS
BEGIN
	DECLARE
		@alumnoId INT,
		@apoderadoId INT;

		SELECT 
			@alumnoId = Id,
			@apoderadoId = ApoderadoId 
		FROM inserted;

		INSERT INTO HistorialApoderados
		(
			Id,
			AlumnoId,
			FechaInicio,
			ApellidoPaterno,
			ApellidoMaterno,
			Nombres,
			Dni,
			Sexo,
			EstadoCivil
		)
		SELECT 
			Id,
			@alumnoId,
			GETDATE(),
			ApellidoPaterno,
			ApellidoMaterno,
			Nombres,
			Dni,
			Sexo,
			EstadoCivil
		FROM Apoderados
		WHERE Id = @apoderadoId;
END;