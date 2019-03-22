-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteCategory]    Script Date: 2019-03-22 9:16:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROC [dbo].[DeleteCategory]
@CategoryID INT
AS
IF EXISTS (Select * FROM Products WHERE Products.categoryId = @CategoryID)
	BEGIN
	RAISERROR('Cannot delete Category because it contains one or more Products', 16, 1)
	RETURN
	END
DELETE FROM Categories
WHERE Categories.id=@CategoryID
 