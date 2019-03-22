USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCategory]    Script Date: 2019-03-22 9:18:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[UpdateCategory]
@CategoryID INT,
@CategoryName NVARCHAR(50),
@Description NVARCHAR(MAX) = NULL
AS
IF EXISTS (SELECT * FROM Categories WHERE [name]=@CategoryName AND id <> @CategoryID)
	BEGIN	
	RAISERROR('This category already exists. Duplicate not alllowed',16,1)
	RETURN
	END

	UPDATE Categories SET 
	[name]=@CategoryName
	,[description] = @Description
	WHERE id=@CategoryID
