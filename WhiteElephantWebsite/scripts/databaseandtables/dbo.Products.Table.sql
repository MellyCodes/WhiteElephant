--Courtney Diotte
--Melanie Roy-Plommer
--Date: March 22, 2019
USE [WhiteElephantStore]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2019-03-22 5:50:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1000,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[briefDescription] [nvarchar](max) NOT NULL,
	[fullDescription] [nvarchar](max) NOT NULL,
	[statusCode] [bit] NOT NULL,
	[price] [money] NOT NULL,
	[featured] [bit] NOT NULL,
	[categoryId] [int] NOT NULL,
	[imageId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
