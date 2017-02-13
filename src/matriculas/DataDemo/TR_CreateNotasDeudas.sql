/*
Trigger para crear las notas y deudas que genera la matrícula.
*/
CREATE TRIGGER TR_CreateNotasDeudas
ON Matriculas
AFTER INSERT
AS
BEGIN
	DECLARE
		@idAlumno INT,
		@idMatricula INT,
		@idSeccion INT
		SELECT 
			@idMatricula = Id,
			@idAlumno = AlumnoId,
			@idSeccion = SeccionId
		FROM inserted;
	BEGIN
		INSERT INTO	Notas
		(
			MatriculaId,
			CursoId,
			Calificacion
		)
		SELECT @idMatricula, c.Id, -1
		FROM Cursos c
		INNER JOIN Grados g ON c.GradoId = g.Id
		INNER JOIN Secciones s ON g.Id = s.GradoId
		WHERE s.Id = 1;

		DECLARE @cnt INT = 3;
		WHILE @cnt <= 12
		BEGIN		
			INSERT INTO	Deudas
			(
				MatriculaId,
				Mes,
				Monto
			)
			VALUES
			(
				@idMatricula,
				@cnt,
				250
			)
			SET @cnt = @cnt + 1;
		END;
	END;
END;