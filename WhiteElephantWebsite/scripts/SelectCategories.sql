-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
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
