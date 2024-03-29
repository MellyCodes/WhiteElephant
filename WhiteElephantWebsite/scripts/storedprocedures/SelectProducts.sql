-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectProducts]    Script Date: 2019-03-22 9:17:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectProducts]
@ProductID INT = NULL,
@CategoryId INT = NULL,
@Keyword VARCHAR(50) = NULL,
@Price1 MONEY = NULL,
@Price2 MONEY = NULL,
@Featured BIT = NULL,
@StatusCode BIT = NULL 
AS
BEGIN 
	
	SELECT 
	p.id
	,p.[name]
	,p.price
	,p.briefDescription
	,p.fullDescription
	,i.[name] as ImageName
	,i.altText
	,p.statusCode
	,p.featured
	,(
		SELECT c.[name] FROM Categories c WHERE c.id = p.categoryId 
	) as CategoryName
	FROM Products p INNER JOIN SiteImages i ON p.imageId = i.id
	WHERE 
	(@ProductID IS NULL OR p.id = @ProductID)
	AND
	(@Featured IS NULL OR p.featured = @Featured)
	AND 
	(@StatusCode IS NULL OR p.statusCode = @StatusCode)
	AND 
	(@CategoryId IS NULL OR p.categoryId = @CategoryId)
	AND
	(
		(@Price1 IS NULL OR p.price >= @Price1)
		AND 
		(@Price2 IS NULL OR p.price <= @Price2)
	)
	ORDER BY p.[name],p.price,p.CategoryId

END


