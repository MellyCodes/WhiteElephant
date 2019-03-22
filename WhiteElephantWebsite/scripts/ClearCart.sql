-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[ClearCart]    Script Date: 2019-03-22 9:16:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER   PROC [dbo].[ClearCart]
@CartUId VARCHAR(50)
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	BEGIN
	DELETE FROM Cart WHERE cartUId=@CartUId
	IF @@Error<>0
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Delete cart error. Transaction cancelled.',16,1)
		RETURN
		END
	IF @@Error=0
		BEGIN
		COMMIT TRANSACTION
		END
	END
