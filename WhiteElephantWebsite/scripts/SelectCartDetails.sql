-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectCartDetails]    Script Date: 2019-03-22 9:17:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER   proc [dbo].[SelectCartDetails]
@CartUId VARCHAR(50)
AS
BEGIN
	SELECT 
	cartUId
	, c.prodId
	, c.qty
	, p.price	
	FROM 
	Cart c INNER JOIN Products p ON c.prodId = p.id 
	WHERE cartUId = @CartUId
END

