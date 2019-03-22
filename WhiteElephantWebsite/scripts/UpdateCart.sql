USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCart]    Script Date: 2019-03-22 9:18:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER     PROC [dbo].[UpdateCart]
@CartUId VARCHAR(50),
@ProdId INT,
@Qty INT
AS
BEGIN 
	IF @Qty = 0
		BEGIN
			DELETE FROM Cart WHERE 
			cartUId=@CartUId AND prodId=@ProdId
		END
	ELSE
		BEGIN
			UPDATE Cart 
			SET
			qty=@Qty
			WHERE 
			cartUId=@CartUId AND prodId=@ProdId
		END

	
END
