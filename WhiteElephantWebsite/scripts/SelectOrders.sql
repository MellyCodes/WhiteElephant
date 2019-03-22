USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectOrders]    Script Date: 2019-03-22 9:17:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectOrders]
@OrderId INT = NULL
AS 
BEGIN
	SELECT * FROM Orders
	WHERE 
	(@OrderId IS NULL OR orderNumber = @OrderId)
END
