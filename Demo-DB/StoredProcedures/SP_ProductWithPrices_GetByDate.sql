CREATE PROCEDURE [dbo].[SP_ProductWithPrices_GetByDate]
	@productId int,
	@date DATETIME2
AS
BEGIN
	DECLARE @start DATETIME2 = (
		SELECT	MAX([StartDate])
			FROM [PriceHistory]
			WHERE	[ProductId] = @productId
				AND [StartDate] <= @date
				AND ([EndDate] > @date OR [EndDate] IS NULL)
	)
	SELECT *
		FROM [V_ProductWithPrices]
		WHERE	[ProductId] = @productId
			AND [StartDate] = @start
END