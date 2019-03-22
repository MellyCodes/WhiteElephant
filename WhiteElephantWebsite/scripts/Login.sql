USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 2019-03-22 9:16:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[Login]
@Email NVARCHAR(50),
@Password NVARCHAR(50)
AS
BEGIN
	SELECT Email from Customers WHERE email = @Email and password = @Password AND isArchived = 0 AND isConfirmed = 1


	IF @@rowcount = 0 
		BEGIN
			RAISERROR('Username or password incorrect', 16, 1)
		END
END
