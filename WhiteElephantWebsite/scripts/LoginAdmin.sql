-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[LoginAdmin]    Script Date: 2019-03-22 9:16:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[LoginAdmin]
@Email NVARCHAR(50),
@Password NVARCHAR(50)
AS
SELECT email,[password] FROM adminLogin 
WHERE email=@Email AND [password]=@Password

IF @@rowcount = 0 
	BEGIN
		RAISERROR('Username or password incorrect', 16, 1)
		RETURN
	END
