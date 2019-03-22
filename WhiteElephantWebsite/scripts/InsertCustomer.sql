USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[InsertCustomer]    Script Date: 2019-03-22 9:16:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[InsertCustomer]
@Email NVARCHAR(50),
@FirstName NVARCHAR(20),
@LastName NVARCHAR(30),
@Pwd NVARCHAR(15),
@Street NVARCHAR(50),
@City NVARCHAR(20),
@Province NVARCHAR(2),
@Pcode NVARCHAR(6),
@Phone NVARCHAR(10),
@Identity INT OUTPUT
AS
BEGIN
	INSERT INTO Customers (lastname, firstname, street, city, province, postalcode, phone, email, [password])
	VALUES (@lastname, @firstname, @street, @city, @province, @pcode, @phone, @email, @pwd)

	SET @Identity = @@IDENTITY
END

