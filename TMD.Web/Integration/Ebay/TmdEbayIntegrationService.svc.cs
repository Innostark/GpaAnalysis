using System;
using System.Data.Entity.Core.Objects;
using TMD.Repository.BaseRepository;
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

            //TODO: Authenticate the passed details

            //Check if there is a load already running.
            using(BaseDbContext dbContext = new BaseDbContext())
            {
                if(!dbContext.IsEbayLoadRunning())
                {

                }
                else
                {
                    //TODO: Raise error that the load is already running
                }
            }

        }
    }
}
