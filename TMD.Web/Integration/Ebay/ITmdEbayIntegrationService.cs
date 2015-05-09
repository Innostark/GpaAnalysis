using System.ServiceModel;
namespace TMD.Web.Integration.Ebay
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ITmdEbayIntegrationService" in both code and config file together.
    [ServiceContract]
    public interface ITmdEbayIntegrationService
    {
        [OperationContract]
        void StartEbayLoad(string username, string password);
    }
}
