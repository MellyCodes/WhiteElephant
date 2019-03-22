-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectCustomers]    Script Date: 2019-03-22 9:17:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectCustomers]
@CustomerId INT = NULL,
@EmailAddress NVARCHAR(50) = NULL
AS
BEGIN
	SELECT * FROM Customers
	WHERE	
	(@EmailAddress IS NULL OR email = @EmailAddress)
	ORDER BY LastName,FirstName, City
END
