-- =============================================
-- Author:		Bilal Rehman
-- Create date: 09/05/2015
-- Description:	Stored procedure to check if an ebay load is running
-- =============================================
CREATE PROCEDURE [dbo].[spIsEbayLoadRunning] 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Statement to select all in process records
	SELECT COUNT(*) As RunningCount
	FROM   STG_EBayBatchImports WITH(NOLOCK)
	WHERE  InProcess = 1;
END
