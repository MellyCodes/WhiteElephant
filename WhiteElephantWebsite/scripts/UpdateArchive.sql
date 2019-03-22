USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateArchive]    Script Date: 2019-03-22 9:17:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER         PROCEDURE [dbo].[UpdateArchive]
@CustomerID INT,
@Archive BIT
AS
BEGIN
    UPDATE Customers SET
    IsArchived = @Archive
    WHERE id = @CustomerId
END
