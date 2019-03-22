USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectCategories]    Script Date: 2019-03-22 9:17:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectCategories]
@CategoryID INT = NULL
AS
BEGIN
	SELECT * FROM Categories
	WHERE @CategoryID IS NULL OR id = @CategoryID
END
