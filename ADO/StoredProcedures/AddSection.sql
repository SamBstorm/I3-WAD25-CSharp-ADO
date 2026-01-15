CREATE PROCEDURE [dbo].[AddSection]
	@id int,
	@name VARCHAR(50)
AS
BEGIN
	INSERT INTO [Section]
		VALUES (@id, @name)
END
