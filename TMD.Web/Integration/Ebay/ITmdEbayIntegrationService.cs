using System.ServiceModel;
namespace TMD.Web.Integration.Ebay
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITmdEbayIntegrationService" in both code and config file together.
    [ServiceContract(Namespace = "http://toymarketdata.com/integrations/ebay/v1/")]
    public interface ITmdEbayIntegrationService
    {
        [OperationContract]
        void StartEbayLoad(string username, string password);
    }
}
