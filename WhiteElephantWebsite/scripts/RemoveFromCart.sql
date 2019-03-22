USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[RemoveFromCart]    Script Date: 2019-03-22 9:17:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[RemoveFromCart]
@CartUId VARCHAR(50),
@ProdId INT
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	BEGIN

		DELETE FROM Cart WHERE cartUId = @CartUId AND prodId = @ProdId
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
