-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectProductMaintenance]    Script Date: 2019-03-22 9:17:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectProductMaintenance] 
@ProductId INT = NULL,
@Keyword NVARCHAR(MAX) = NULL
AS
BEGIN

	SELECT p.*,
	(
		SELECT name FROM Categories WHERE id = p.categoryId
	) as CategoryName,
	(
		SELECT name FROM SiteImages WHERE id=p.imageId
	) as ImageName
	FROM Products p
	WHERE 
	(@ProductId IS NULL OR p.id = @ProductId)
	AND
	(@Keyword IS NULL OR 
		(
			[name] LIKE '%'+@Keyword+'%' OR briefDescription LIKE '%'+@Keyword+'%' OR fullDescription LIKE '%'+@Keyword+'%' 
		)
	)
	
END
