/*
Trigger para guardar el historial de cambio de estado del alumno.
*/
CREATE TRIGGER TR_SaveUpdateHistorialAlumno
ON Alumnos
AFTER UPDATE
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE
		@alumnoId INT,
		@old_estado NVARCHAR(1),
		@new_estado NVARCHAR(1);

		SELECT 
			@alumnoId = Id,
			@new_estado = Estado
		FROM inserted;

		SELECT
			@old_estado = Estado
		FROM deleted

		IF(@old_estado <> @new_estado)
			BEGIN
				IF(@old_estado = 1 AND @new_estado = 0)
					UPDATE HistorialAlumnos
					SET FechaRetiro = GETDATE()
					WHERE AlumnoId = @alumnoId;	
				ELSE
					INSERT INTO HistorialAlumnos
					(
						AlumnoId,
						FechaIngreso,
						FechaRetiro
					)
					VALUES 
					(
						@alumnoId,
						GETDATE(),
						NULL
					);
			END;
END;