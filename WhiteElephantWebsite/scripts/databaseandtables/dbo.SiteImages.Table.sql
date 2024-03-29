--Courtney Diotte
--Melanie Roy-Plommer
--Date: March 22, 2019
USE [WhiteElephantStore]
GO
/****** Object:  Table [dbo].[SiteImages]    Script Date: 2019-03-22 5:50:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteImages](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[uploadDate] [datetime] NOT NULL,
	[altText] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SiteImages] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SiteImages] ADD  CONSTRAINT [DF_SiteImages_uploadDate]  DEFAULT (getdate()) FOR [uploadDate]
GO
