-- Courtney Diotte
-- Melanie Roy-Plommer
-- March 22, 2019
-- WhiteElephantStore Stored Procedure
USE [WhiteElephantStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 2019-03-22 9:18:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER     PROCEDURE [dbo].[UpdateProduct]
@ProductId INT,
@ProductName NVARCHAR(50) = NULL,
@ProductBriefDesc NVARCHAR(MAX) = NULL,
@ProductFullDesc NVARCHAR(MAX) = NULL,
@StatusCode BIT = NULL,
@ProductPrice MONEY = NULL,
@Featured BIT = NULL,
@CategoryId INT = NULL,
@ImageName NVARCHAR(50) = NULL,
@ImageUploadDate DATETIME = NULL,
@AltText NVARCHAR(50) = NULL
AS
BEGIN TRANSACTION
	SET NOCOUNT ON
	BEGIN

	/*
		SiteImages PK Variable for Image Delete
	*/
	DECLARE @ImageId INT = NULL

	IF @ImageName IS NOT NULL
		BEGIN
			/*
				Get Old Image
			*/
			SET @ImageId = (
				SELECT imageId FROM Products WHERE id = @ProductId
			)

			DELETE FROM SiteImages
			WHERE id = @ImageID
	
			IF @@Error<>0
				BEGIN
				ROLLBACK TRANSACTION
				RAISERROR('Delete SiteImage error. Transaction cancelled.',16,1)
				RETURN
				END

			/*
				All clear. Insert the product	 Image
				Get the SiteImages PK
			*/
			INSERT INTO SiteImages
			([name],uploadDate,altText)
			VALUES
			(@ImageName,@ImageUploadDate,@AltText)


			SET @ImageID = @@IDENTITY
		END
	
	UPDATE Products 
	SET
	[name] = ISNULL(@ProductName,[name])
	,briefDescription = ISNULL(@ProductBriefDesc,briefDescription)
	,fullDescription = ISNULL(@ProductFullDesc,fullDescription)
	,statusCode = ISNULL(@StatusCode,statusCode)
	,price = ISNULL(@ProductPrice,price)
	,featured = ISNULL(featured,@Featured)
	,categoryId = ISNULL(@CategoryId,categoryId)
	,imageId = ISNULL(@ImageID,imageId)
	WHERE id= @ProductId
		
	IF @@Error<>0
		BEGIN
		ROLLBACK TRANSACTION
		RAISERROR('Update product error. Transaction cancelled.',16,1)
		RETURN
		END

	/*
		All clear.
	*/
	
	IF @@Error=0
		BEGIN
		COMMIT TRANSACTION
		END
	END
