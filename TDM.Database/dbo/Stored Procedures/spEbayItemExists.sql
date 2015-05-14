-- =============================================
-- Author:		Bilal Rehman
-- Create date: 14 May 2015
-- Description:	Checks if a ebay items exists in the STG_EBayItems table for a
--              respective ItemId (passed as the parameter). TRIMS the parameter
--              & Lower case comparision.
--              Return the number of row found (logically this should be 0 or 1)
-- =============================================
CREATE PROCEDURE [dbo].[spEbayItemExists] 
	-- Add the parameters for the stored procedure here
	@itemId NVARCHAR(19)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT [EBayItemtId]
		  ,[EBayBatchImportId]
		  ,[ToyGraderItemId]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[ModifiedBy]
		  ,[ModifiedOn]
		  ,[Deleted]
		  ,[DeletedOn]
		  ,[DeletedBy]
		  ,[Condition]
		  ,[CountryCode]
		  ,[GalleryURL]
		  ,[GlobalId]
		  ,[ItemId]
		  ,[ListingInfoBuyItNowAvailable]
		  ,[ListingInfoBuyItNowPrice]
		  ,[ListingInfoEndTime]
		  ,[ListingInfoGift]
		  ,[ListingInfoListingType]
		  ,[ListingInfoStartTime]
		  ,[PrimaryCategory]
		  ,[ProducitId]
		  ,[SecondaryCategory]
		  ,[SellerInfoTopRatedSeller]
		  ,[SellingStatusBidCount]
		  ,[SellingStatusCurrentPrice]
		  ,[SellingStatusSellingState]
		  ,[SellingStatusTimeLeft]
		  ,[StoreInfoStoreName]
		  ,[StoreInfoStoreURL]
		  ,[SubTitle]
		  ,[Title]
		  ,[ViewItemUrL]
	FROM   [dbo].[STG_EBayItems] AS sei WITH(NOLOCK)
	WHERE  sei.ItemId = @itemId;

	RETURN @@ROWCOUNT;
END