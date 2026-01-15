CREATE TABLE [dbo].[PriceHistory]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY, 
    [ProductId] INT NOT NULL, 
    [StartDate] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [EndDate] DATETIME2 NULL, 
    [Price] MONEY NOT NULL, 
    CONSTRAINT [CK_PriceHistory_Price] CHECK (Price > 0), 
    CONSTRAINT [CK_PriceHistory_Dates] CHECK (EndDate IS NULL OR EndDate > StartDate), 
    CONSTRAINT [FK_PriceHistory_Product] FOREIGN KEY ([ProductId]) REFERENCES [Product]([ProductId])
)

GO

CREATE TRIGGER [dbo].[Trigger_PriceHistory]
    ON [dbo].[PriceHistory]
    INSTEAD OF DELETE
    AS
    BEGIN
        SET NoCount ON
        DECLARE @id INT = (SELECT [Id] FROM [deleted])
        UPDATE [PriceHistory]
            SET [EndDate] = GETDATE()
            WHERE [Id] = @id
    END