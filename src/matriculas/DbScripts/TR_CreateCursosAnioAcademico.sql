/*
Trigger para crear los cursos que se dictarán por grado en un año académico.
*/
CREATE TRIGGER TR_CreateCursosAnioAcademico
ON AniosAcademicos
AFTER INSERT 
AS
DECLARE
	@idAnioAcademico int;

	SELECT 
		@idAnioAcademico = Id
	FROM inserted;
BEGIN
	INSERT INTO CursosAniosAcademicos
	(
		AnioAcademicoId,
		GradoId,
		CursoId		
	)
	SELECT 
		@idAnioAcademico, 
		GradoId,
		Id  
	FROM Cursos
	WHERE Estado = '1';
END;