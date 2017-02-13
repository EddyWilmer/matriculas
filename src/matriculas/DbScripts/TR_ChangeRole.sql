/*
Trigger para cambiar el rol en la tabla de roles
*/
CREATE TRIGGER TR_ChangeRole
ON Colaboradores
AFTER UPDATE 
AS
DECLARE
	@idColaborador INT,
	@new_rol NVARCHAR,
	@old_rol NVARCHAR;

	SELECT 
		@idColaborador = Id,
		@new_rol = RolId
	FROM inserted;

	SELECT 
		@old_rol = RolId
	FROM deleted;
BEGIN
	IF @new_rol <> @old_rol
		UPDATE AspNetUserRoles
		SET RoleId = (SELECT Id
					  FROM AspNetRoles
				      WHERE NormalizedName = (SELECT UPPER(Nombre) 
											  FROM Cargos 
											  WHERE Id = @new_rol))
		WHERE UserId = (SELECT Id 
						FROM AspNetUsers
						WHERE ColaboradorId = @idColaborador);
END;