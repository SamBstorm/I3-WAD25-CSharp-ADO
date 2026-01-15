CREATE TABLE [dbo].[course] (
    [course_id]    VARCHAR (8)    NOT NULL,
    [course_name]  VARCHAR (200)  NOT NULL,
    [course_ects]  DECIMAL (3, 1) NOT NULL,
    [professor_id] INT            NOT NULL,
    CONSTRAINT [PK_course] PRIMARY KEY CLUSTERED ([course_id] ASC),
    CONSTRAINT [FK_course_professor] FOREIGN KEY ([professor_id]) REFERENCES [dbo].[professor] ([professor_id])
);

