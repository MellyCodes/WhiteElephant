USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[CartTotal]    Script Date: 2019-03-22 9:16:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER       PROCEDURE [dbo].[CartTotal]
@CartUId VARCHAR(50)
AS
BEGIN
	SELECT 
	SUM(p.price * c.qty) as Total 
	FROM Cart c INNER JOIN Products p ON c.prodId = p.Id 
	WHERE c.cartUId = @CartUId
END
