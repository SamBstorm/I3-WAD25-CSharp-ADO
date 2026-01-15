CREATE PROCEDURE [dbo].[SP_Product_Create]
	@name NVARCHAR(128),
	@desc NVARCHAR(MAX)
AS
BEGIN
	INSERT INTO [Product] ([Name], [Description])
		VALUES (TRIM(@name), TRIM(@desc))
END
