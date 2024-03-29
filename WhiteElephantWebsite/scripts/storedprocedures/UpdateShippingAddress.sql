-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateShippingAddress]    Script Date: 2019-03-22 9:18:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER       PROCEDURE [dbo].[UpdateShippingAddress]
@CustomerID INT,
@ShippingStreet NVARCHAR(50),
@ShippingCity NVARCHAR(20),
@ShippingProvince NVARCHAR(2),
@ShippingPcode NVARCHAR(6)
AS
BEGIN
    UPDATE Customers SET
    shippingStreet = @ShippingStreet,
    shippingCity = @ShippingCity,
    shippingProvince = @ShippingProvince,
    shippingPostalCode = @ShippingPcode
    WHERE id = @CustomerId
END