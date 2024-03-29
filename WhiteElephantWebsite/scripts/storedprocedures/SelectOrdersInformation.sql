-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectOrdersInformation]    Script Date: 2019-03-22 9:17:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectOrdersInformation]
@OrderId INT = NULL
AS 
BEGIN
	SELECT 
	o.*
	,c.*
	 FROM Orders o INNER JOIN Customers c ON o.CustomerId = c.id
	WHERE 
	(@OrderId IS NULL OR o.orderNumber = @OrderId)
END
