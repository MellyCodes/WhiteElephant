-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectImages]    Script Date: 2019-03-22 9:17:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectImages]
@Id INT = NULL
AS
BEGIN
	SELECT * FROM SiteImages
	WHERE (@Id IS NULL OR id = @Id)
END
