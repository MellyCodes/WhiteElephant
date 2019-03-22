-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectOrderDetails]    Script Date: 2019-03-22 9:17:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectOrderDetails]
@OrderId INT
AS 
BEGIN
	SELECT 
	od.id
	,p.id as ProductId
	,p.[name]
	,od.quantity
	,p.price
	,i.[name] as ImageName
	,SUM(od.quantity * p.price) as linetotal	
	 FROM OrderDetails od INNER JOIN Products p ON od.productId = p.id
	 INNER JOIN SiteImages i ON p.imageId = i.id
	WHERE 
	(@OrderId IS NULL OR od.orderNumber = @OrderId)
	GROUP BY od.id,p.id,p.[name]
	,p.price,od.quantity,i.[name]
END
