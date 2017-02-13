/*
Trigger para crear el cronograma de matrícula cuando se agrege un año académico.
*/
CREATE TRIGGER TR_CreateCronogramasMatricula
ON AniosAcademicos
AFTER INSERT 
AS
DECLARE
	@idAnioAcademico int;

	SELECT 
		@idAnioAcademico = Id
	FROM inserted;
BEGIN
	INSERT INTO CronogramasMatricula
	(
		AnioAcademicoId,
		Nombre
	)
	VALUES
	(
		@idAnioAcademico,
		'Alumnos regulares'
	);
END;