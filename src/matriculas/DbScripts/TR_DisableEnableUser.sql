/*
Trigger para habilitar o deshabilitar el acceso al sistema de un usuario.
*/
CREATE TRIGGER TR_DisableEnableUser
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
		IF (@new_estado = '1')
			UPDATE AspNetUsers
			SET LockoutEnabled = 'True'
			WHERE ColaboradorId = @idColaborador;
		ELSE IF (@new_estado = '3')
			UPDATE AspNetUsers
			SET LockoutEnabled = 'False'
			WHERE ColaboradorId = @idColaborador;
	END;
END;