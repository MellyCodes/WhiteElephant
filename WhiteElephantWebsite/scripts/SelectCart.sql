USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectCart]    Script Date: 2019-03-22 9:17:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROC [dbo].[SelectCart]
@CartUId VARCHAR(50),
@CartProdID INT = NULL
AS
BEGIN
	
	SELECT 
	c.cartUId as CartId
	,MAX(p.id) as ProductId
	,MAX(p.[name]) as ProductName
	,SUM(c.qty)	as Qty
	,MAX(p.price) as Price
	,(
		c.qty * p.price
	) AS LineTotal	
	FROM Cart c INNER JOIN Products p ON c.prodId = p.id
	WHERE 
	c.cartUId = @CartUId 
	AND 
	(@CartProdID IS NULL OR c.prodId = @CartProdID)
	GROUP BY c.cartUId,p.[name],c.qty,p.price
END
