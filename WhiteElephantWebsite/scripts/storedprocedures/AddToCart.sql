-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[AddToCart]    Script Date: 2019-03-22 9:16:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER    PROC [dbo].[AddToCart]
@CartUId VARCHAR(50),
@ProductId INT,
@Qty INT
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
IF EXISTS (SELECT * FROM Cart WHERE cartUID=@CartUId AND prodId=@ProductId)
	BEGIN
	UPDATE Cart SET qty=qty+@Qty WHERE cartUID=@CartUId AND prodId=@ProductId
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
ELSE
	BEGIN
	INSERT INTO Cart (cartUID,prodId,qty) VALUES (@CartUId,@ProductId,@Qty)
	IF @@Error<>0
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Insert into cart error. Transaction cancelled.',16,1)
		RETURN
		END
	IF @@Error=0
		BEGIN
		COMMIT TRANSACTION
		END
	END
