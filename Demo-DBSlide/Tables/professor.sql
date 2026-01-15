CREATE TABLE [dbo].[professor] (
    [professor_id]        INT          NOT NULL,
    [professor_name]      VARCHAR (30) NOT NULL,
    [professor_surname]   VARCHAR (30) NOT NULL,
    [section_id]          INT          NOT NULL,
    [professor_office]    INT          NOT NULL,
    [professor_email]     VARCHAR (30) NOT NULL,
    [professor_hire_date] DATETIME     NOT NULL,
    [professor_wage]      INT          NOT NULL,
    CONSTRAINT [PK_professor] PRIMARY KEY CLUSTERED ([professor_id] ASC),
    CONSTRAINT [FK_professor_section] FOREIGN KEY ([section_id]) REFERENCES [dbo].[section] ([section_id])
);

