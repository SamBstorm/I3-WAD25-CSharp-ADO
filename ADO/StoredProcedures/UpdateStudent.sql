CREATE PROCEDURE [dbo].[UpdateStudent]
	@id int,
	@sectionid int,
	@yearresult int
AS
BEGIN
	UPDATE [Student]
		SET [SectionID] = @sectionid,
			[YearResult] = @yearresult
		WHERE [Id] = @id
END