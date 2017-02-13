/*
Trigger para guardar el alumno al historial.
*/
CREATE TRIGGER TR_SaveInsertHistorialAlumno
ON Alumnos
AFTER INSERT
AS
BEGIN
	DECLARE
		@alumnoId INT;

		SELECT 
			@alumnoId = Id
		FROM inserted;

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
			null
		);
END;