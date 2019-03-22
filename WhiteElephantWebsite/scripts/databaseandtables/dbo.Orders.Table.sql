--Courtney Diotte
--Melanie Roy-Plommer
--Date: March 22, 2019
USE [WhiteElephantStore]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 2019-03-22 5:50:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[orderNumber] [int] IDENTITY(999,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[orderDate] [date] NOT NULL,
	[totalAmount] [money] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[orderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_orderDate]  DEFAULT (getdate()) FOR [orderDate]
GO
