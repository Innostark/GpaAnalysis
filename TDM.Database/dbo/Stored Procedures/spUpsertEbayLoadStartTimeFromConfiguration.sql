-- =============================================
-- Author:		Bilal Rehman
-- Create date: 21-05-2015
-- Description:	Inserts the ebay StartTimeFrom for the last batch load
-- =============================================
CREATE PROCEDURE [dbo].[spUpsertEbayLoadStartTimeFromConfiguration] 
	-- Add the parameters for the stored procedure here
	@EbayLoadStartTimeFrom datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @CountEbayLoadStartTimeFrom int;
	DECLARE @EbayLoadStartTimeFromId int;

	SELECT @EbayLoadStartTimeFromId = Id
	FROM   [dbo].[Configurations] AS conf WITH(NOLOCK)
	WHERE  LOWER(conf.Name) = LOWER('EbayLoadStartTimeFrom');
	
	SET @CountEbayLoadStartTimeFrom = @@ROWCOUNT;
		
	PRINT 'There''s ' + CAST(@CountEbayLoadStartTimeFrom AS varchar(3)) + ' EbayLoadStartTimeFrom Configuration records';

    IF (@CountEbayLoadStartTimeFrom = 0)
	BEGIN
		PRINT 'Creating a new EbayLoadStartTimeFrom Configuration record';
		
		INSERT INTO [dbo].[Configurations]
					([Name]
					,[Type]
					,[Value])
				VALUES
					('EbayLoadStartTimeFrom'
					,'DateTime'
					, @EbayLoadStartTimeFrom);
		
		SELECT 1;	
	END
	ELSE 
	BEGIN
		PRINT 'Updating EbayLoadStartTimeFrom Configuration record with Id = ' + CAST(@CountEbayLoadStartTimeFrom AS varchar(3));
		
		UPDATE [dbo].[Configurations]
		SET [Value] = @EbayLoadStartTimeFrom
		WHERE [Id] = @EbayLoadStartTimeFromId;

		SELECT 2;
	END

	SELECT -1;
END