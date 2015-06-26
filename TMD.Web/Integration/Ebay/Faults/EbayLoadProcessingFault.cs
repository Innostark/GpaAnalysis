namespace TMD.Web.Integration.Ebay.Faults
{
    /// <summary>
    /// Custom class for ebay load processing faults
    /// All faults start from code 2000
    /// </summary>
    public class EbayLoadProcessingFault: BaseCustomFault
    {
        public const string FaultCodeUnhandledError = "2000";
        public const string FaultMessageUnhandledError = "Error in ebay batch load, check details for error.";

        public const string FaultCodeBatchAlreadyRunning = "2001";
        public const string FaultMessageBatchAlreadyRunning = "An ebay batch load is already running, please wait for the previous load to finish.";

        public const string FaultCodeBatchWasNotCreated = "2002";
        public const string FaultMessageBatchWasNotCreated = "A new ebay batch load cannot be created, the execution will now stop.";

        public const string FaultCodeFindItemBykeywordResposeIsNull = "2003";
        public const string FaultMessageFindItemBykeywordResposeIsNull = "There is an error in the created findItemsByKeyword request, the returned response is null.";

        public const string FaultCodeFindItemBykeywordReturnedFailure = "2004";
        public const string FaultMessageFindItemBykeywordReturnedFailure = "The findItemsByKeyword request returned failure (details in the message).";
    }
}