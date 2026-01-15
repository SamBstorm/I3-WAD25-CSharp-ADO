CREATE PROCEDURE [dbo].[AddStudent]
	@firstname varchar(50),
	@lastname varchar(50),
	@birthdate datetime2,
	@sectionid int
AS
BEGIN
	INSERT INTO [Student]([FirstName],[LastName],[BirthDate],[SectionID],[YearResult])
	OUTPUT [inserted].[Id]
	VALUES (@firstname, @lastname, @birthdate, @sectionid, 0)
END