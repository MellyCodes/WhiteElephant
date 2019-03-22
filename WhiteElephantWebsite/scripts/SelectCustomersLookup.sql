-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[SelectCustomersLookup]    Script Date: 2019-03-22 9:17:38 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[SelectCustomersLookup]
@CustomerId INT = NULL
AS
BEGIN
	SELECT LastName + ', '+ FirstName FROM Customers
	WHERE
	@CustomerId IS NULL OR id = @CustomerId
	ORDER BY LastName,FirstName, City
END
