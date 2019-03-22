-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[DeleteFromCart]    Script Date: 2019-03-22 9:16:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROC [dbo].[DeleteFromCart]
@CartUId VARCHAR(50),
@ProdId INT
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	BEGIN
	Delete FROM Cart 
	WHERE cartUId=@CartUId AND prodId=@ProdId
	IF @@Error<>0
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Update cart error. Transaction cancelled.',16,1)
		RETURN
		END
	IF @@Error=0
		BEGIN
		COMMIT TRANSACTION
		END
	END
