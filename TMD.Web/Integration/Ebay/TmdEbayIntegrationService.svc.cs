using System;
using Microsoft.Practices.Unity;
using TMD.Interfaces.IServices;
using TMD.Models.DomainModels;
using TMD.WebBase.UnityConfiguration;

namespace TMD.Web.Integration.Ebay
{   
    // NOTE: In order to launch WCF Test Client for testing this service, please select TmdEbayIntegrationService.svc or TmdEbayIntegrationService.svc.cs at the Solution Explorer and start debugging.
    public class TmdEbayIntegrationService : ITmdEbayIntegrationService
    {
        public void StartEbayLoad(string username, string password)
        {
            if(String.IsNullOrWhiteSpace(username))
            {
                //TODO: Raise error
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                //TODO: Raise error
            }

            IStagingEbayLoadService service = UnityConfig.GetConfiguredContainer().Resolve<IStagingEbayLoadService>();

            service.CreateSTGEbayBatchImport(new STGEbayBatchImport());

            //TODO: Authenticate the passed details

            //Check if there is a load already running.


        }
    }
}
