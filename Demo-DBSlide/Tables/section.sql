CREATE TABLE [dbo].[section] (
    [section_id]   INT          NOT NULL,
    [section_name] VARCHAR (50) NULL,
    [delegate_id]  INT          NULL,
    CONSTRAINT [PK_section] PRIMARY KEY CLUSTERED ([section_id] ASC)
);

