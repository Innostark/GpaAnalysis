using System.Runtime.Serialization;

namespace TMD.Web.Integration.Ebay.Faults
{
    [DataContract(Namespace = "http://toymarketdata.com/integrations/ebay/v1/")]
    public class BaseCustomFault
    {
        
        [DataMember]
        public bool Result { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public string ErrorDetails { get; set; }
    }
}