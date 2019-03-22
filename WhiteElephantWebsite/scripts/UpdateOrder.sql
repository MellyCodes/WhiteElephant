USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateOrder]    Script Date: 2019-03-22 9:18:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[UpdateOrder]
@OrderNumber INT,
@OrderDate DATE,
@OrderDetails OrderDetail READONLY
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	BEGIN

	DECLARE @OrderTotal MONEY
	
	/*
		Update
	*/

	SET @OrderTotal = (
		SELECT SUM(OrderDetailLineTotal) 
		FROM (
			SELECT (oDet.Qty * (SELECT price FROM Products WHERE id = oDet.ProductId)
		) as OrderDetailLineTotal
		FROM @OrderDetails oDet) 
		LineTotalsTableAlias
	)

	/*
		Clear and re-create order details
	*/
	DELETE FROM OrderDetails WHERE orderNumber = @OrderNumber

	IF @@Error<>0
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Error updating order details - purge. Transaction cancelled.',16,1)
		RETURN
		END

	/*
		Insert the Order Details Records
	*/
	INSERT INTO OrderDetails
	SELECT @OrderNumber,ProductId,Qty FROM @OrderDetails

	IF @@Error<>0
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Error updating order details - insert. Transaction cancelled.',16,1)
		RETURN
		END

	/*
		All Clear
	*/

	IF @@Error=0
		BEGIN
		COMMIT TRANSACTION
		END
END
