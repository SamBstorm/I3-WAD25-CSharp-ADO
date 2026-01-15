CREATE TABLE [dbo].[Product]
(
	[ProductId] INT NOT NULL IDENTITY, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreationDate] DATETIME2 NOT NULL DEFAULT GETDATE(), 
    CONSTRAINT [PK_Product] PRIMARY KEY ([ProductId]), 
    CONSTRAINT [UK_Product_Name] UNIQUE ([Name])
)
