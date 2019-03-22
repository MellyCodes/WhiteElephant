-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[ActivateCustomer]    Script Date: 2019-03-22 9:16:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER       PROCEDURE [dbo].[ActivateCustomer]
@Email NVARCHAR(50)

AS
IF EXISTS (Select * FROM Customers WHERE Customers.email = @Email)
BEGIN
	UPDATE Customers
	SET isConfirmed = 1 
	WHERE Customers.email = @Email 	
END