CREATE TABLE [dbo].[student] (
    [student_id]  INT          NOT NULL,
    [first_name]  VARCHAR (50) NULL,
    [last_name]   VARCHAR (50) NULL,
    [birth_date]  DATETIME     NULL,
    [login]       VARCHAR (50) NULL,
    [section_id]  INT          NULL,
    [year_result] INT          NULL,
    [course_id]   VARCHAR (6)  NOT NULL,
    CONSTRAINT [PK_student] PRIMARY KEY CLUSTERED ([student_id] ASC),
    CONSTRAINT [FK_student_section] FOREIGN KEY ([section_id]) REFERENCES [dbo].[section] ([section_id])
);

