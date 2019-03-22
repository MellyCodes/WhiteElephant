-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertCategory]    Script Date: 2019-03-22 9:16:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[InsertCategory]
@CategoryName NVARCHAR(50),
@Description NVARCHAR(MAX) = NULL,
@Id INT OUTPUT
AS
IF EXISTS (SELECT * FROM Categories WHERE [name]=@CategoryName)
	BEGIN	
	RAISERROR('This category already exists. Duplicate not allowed',16,1)
	RETURN
	END
	
INSERT INTO Categories ([name],[description]) VALUES (@CategoryName,@Description)
SET @id = @@IDENTITY