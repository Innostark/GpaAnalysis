using System.Runtime.Serialization;

namespace TMD.Web.Integration.Ebay.Faults
{
    [DataContract(Namespace = "http://toymarketdata.com/integrations/ebay/v1/")]
    public class InputParameterFault: BaseCustomFault
    {
        public const string FaultCodeInvalidParameter = "1000";
        public const string FaultMessageInvalidParameter = "The parameter(s) passed to the service call is/are not valid, please check the fault details for more information.";

        public const string FaultCodeUserNameWasNullOrEmpty = "1050";
        public const string FaultMessageUserNameWasNullOrEmpty = "The parameter user name is required.";

        public const string FaultCodePasswordWasNullOrEmpty = "1051";
        public const string FaultMessagePasswordWasNullOrEmpty = "The parameter password is required.";
        
    }
}