CREATE PROCEDURE [dbo].[DeleteStudent]
	@id int
AS
BEGIN
	DELETE FROM [Student]
		WHERE [Id] = @id
END
