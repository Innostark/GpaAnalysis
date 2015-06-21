namespace TMD.Web.Integration.Ebay.Faults
{
    /// <summary>
    /// Custom class for logger faults
    /// All faults start from code 300
    /// </summary>
    public class LoggerFault : BaseCustomFault
    {
        public const string FaultCodeLoggerCanotBeLoaded = "300";
        public const string FaultMessageLoggerCanotBeLoaded = "Service logger could not be loaded .";
    }
}