/*
Trigger para guardar el historial del cambio de apoderado de un alumno.
*/
CREATE TRIGGER TR_SaveUpdateHistorialApoderado
ON Apoderados
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE
		@apoderadoId INT,
		@old_dni NVARCHAR(8),
		@new_dni NVARCHAR(8);

		SELECT 
			@apoderadoId = Id
		FROM inserted;

		SELECT
			@old_dni = Dni
		FROM deleted;

		SELECT 
			@new_dni = Dni
		FROM inserted;

		IF(@old_dni <> @new_dni)
			BEGIN
				UPDATE HistorialApoderados
				SET FechaFin = GETDATE()
				WHERE Id = @apoderadoId
					AND FechaFin IS NULL; 

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
					ap.Id,
					al.Id,
					GETDATE(),
					ap.ApellidoPaterno,
					ap.ApellidoMaterno,
					ap.Nombres,
					ap.Dni,
					ap.Sexo,
					ap.EstadoCivil
				FROM Apoderados ap				
				INNER JOIN Alumnos al ON ap.Id = al.ApoderadoId
				WHERE ap.Id = @apoderadoId;				
			END;	
END;