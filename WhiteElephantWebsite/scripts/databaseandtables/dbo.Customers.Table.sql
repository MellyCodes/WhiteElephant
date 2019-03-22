USE [WhiteElephantStore]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 2019-03-22 5:50:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[id] [int] IDENTITY(975,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[password] [nvarchar](15) NOT NULL,
	[street] [nvarchar](50) NOT NULL,
	[city] [nvarchar](20) NOT NULL,
	[province] [nvarchar](2) NOT NULL,
	[postalCode] [nvarchar](6) NOT NULL,
	[phone] [nvarchar](10) NOT NULL,
	[shippingStreet] [nvarchar](50) NULL,
	[shippingCity] [nvarchar](20) NULL,
	[shippingProvince] [nvarchar](2) NULL,
	[shippingPostalCode] [nvarchar](6) NULL,
	[isArchived] [bit] NOT NULL,
	[isConfirmed] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Archived]  DEFAULT ((0)) FOR [isArchived]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_isConfirmed]  DEFAULT ((0)) FOR [isConfirmed]
GO
