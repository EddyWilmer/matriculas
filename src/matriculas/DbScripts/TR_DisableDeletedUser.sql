/*
Trigger para deshabilitar el acceso al sistema de un usuario eliminado.
*/
CREATE TRIGGER TR_DisableDeletedUser
ON Colaboradores
AFTER UPDATE
AS
DECLARE
	@idColaborador INT,
	@new_estado NVARCHAR(1),
	@old_estado NVARCHAR(1)

	SELECT 
		@new_estado = Estado,
		@idColaborador = Id
	FROM inserted;

	SELECT 
		@old_estado = Estado
	FROM deleted;
BEGIN
	IF (@new_estado != @old_estado)
	BEGIN
		IF (@new_estado = '2')
			UPDATE AspNetUsers
			SET LockoutEnabled = 'False'
			WHERE ColaboradorId = @idColaborador;
	END;
END;