USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[CartCount]    Script Date: 2019-03-22 9:16:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER      PROC [dbo].[CartCount]
@CartUId VARCHAR(50)
AS
SELECT SUM(qty) FROM Cart
WHERE cartUId=@CartUId	
