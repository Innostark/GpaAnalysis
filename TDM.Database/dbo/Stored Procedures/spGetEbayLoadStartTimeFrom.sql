-- =============================================
-- Author:		Bilal Rehman
-- Create date: 21-05-2015
-- Description:	Gets the EbayLoadStartTimeFrom the configuratin table.
-- =============================================
CREATE PROCEDURE [dbo].[spGetEbayLoadStartTimeFrom] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT Value
	FROM   [dbo].[Configurations] AS conf WITH(NOLOCK)
	WHERE  LOWER(conf.Name) = LOWER('EbayLoadStartTimeFrom');

	RETURN @@ROWCOUNT;
END