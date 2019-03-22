-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateCustomer]    Script Date: 2019-03-22 9:18:07 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[UpdateCustomer]
@CustomerID INT,

@Street NVARCHAR(50),
@City NVARCHAR(20),
@Province NVARCHAR(2),
@Pcode NVARCHAR(6),
@Phone NVARCHAR(10)
AS
BEGIN
    UPDATE Customers SET
    
    street = @Street,
    city = @City,
    province = @Province,
    postalCode = @Pcode,
    phone = @Phone
    WHERE id = @CustomerId
END
