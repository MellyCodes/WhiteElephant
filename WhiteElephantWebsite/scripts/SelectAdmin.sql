-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectAdmin]    Script Date: 2019-03-22 9:17:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectAdmin]
@Email NVARCHAR(50),
@Pwd NVARCHAR(50)
AS
SELECT email,[password] FROM adminLogin WHERE email=@Email
