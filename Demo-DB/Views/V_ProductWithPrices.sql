CREATE VIEW [dbo].[V_ProductWithPrices]
	AS 
	SELECT	[Product].[ProductId],
			[Name],
			[Description],
			[CreationDate],
			[Id] AS [PriceId],
			[Price],
			[StartDate],
			[EndDate]
		FROM [Product]
			LEFT JOIN [PriceHistory]
				ON [Product].[ProductId] = [PriceHistory].[ProductId]
