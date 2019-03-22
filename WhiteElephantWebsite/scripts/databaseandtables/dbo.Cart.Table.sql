--Courtney Diotte
--Melanie Roy-Plommer
--Date: March 22, 2019
USE [WhiteElephantStore]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 2019-03-22 5:50:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[cartUId] [varchar](50) NOT NULL,
	[prodId] [int] NOT NULL,
	[qty] [int] NOT NULL,
	[date] [date] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cart] ADD  CONSTRAINT [DF_Cart_date]  DEFAULT (getdate()) FOR [date]
GO
