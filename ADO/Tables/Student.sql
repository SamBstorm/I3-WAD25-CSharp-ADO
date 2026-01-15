CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [BirthDate] DATETIME2 NOT NULL, 
    [YearResult] INT NOT NULL, 
    [SectionID] INT NOT NULL, 
    [Active] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Student_Section] FOREIGN KEY ([SectionID]) REFERENCES [Section]([Id]), 
    CONSTRAINT [CK_Student_YearResult] CHECK (YearResult BETWEEN 0 AND 20), 
    CONSTRAINT [CK_Student_BirthDate] CHECK (BirthDate >= '1930-01-01')
)
