--Courtney Diotte
--Melanie Roy-Plommer
--Date: March 22, 2019
USE [WhiteElephantStore]
GO
/****** Object:  UserDefinedTableType [dbo].[OrderDetail]    Script Date: 2019-03-22 5:50:07 PM ******/
CREATE TYPE [dbo].[OrderDetail] AS TABLE(
	[ProductID] [int] NOT NULL,
	[Qty] [int] NOT NULL
)
GO
