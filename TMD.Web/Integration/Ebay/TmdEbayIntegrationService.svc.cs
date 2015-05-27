using System;
using System.Collections.Generic;
using System.Linq;
using eBay.Services.Finding;
using Microsoft.Practices.Unity;
using TMD.Interfaces.IServices;
using TMD.Models.DomainModels;
using TMD.Web.ModelMappers;
using TMD.WebBase.UnityConfiguration;
using eBay.Services;
using System.Configuration;

namespace TMD.Web.Integration.Ebay
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select TmdEbayIntegrationService.svc or TmdEbayIntegrationService.svc.cs at the Solution Explorer and start debugging.
    public class TmdEbayIntegrationService : ITmdEbayIntegrationService
    {
        public void StartEbayLoad(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username))
            {
                //TODO: Raise error
            }

            if (String.IsNullOrWhiteSpace(password))
            {
                //TODO: Raise error
            }

            using (
                IStagingEbayLoadService stagingEbayLoadService =
                    UnityConfig.GetConfiguredContainer().Resolve<IStagingEbayLoadService>())
            {
                if (stagingEbayLoadService.CanExecuteEbayLoad())
                {
                    string userId = "0141f5f5-c1d8-4550-921e-099d21c248f1";
                    string iso8601DatetimeFormat = ConfigurationManager.AppSettings["EbayISO8601DateTimeFormat"];
                    string ebayLoadStartTimeFromConfiguration = stagingEbayLoadService.GetEbayLoadStartTimeFrom();
                    StagingEbayBatchImport stagingEbayBatchImport = stagingEbayLoadService.CreateStagingEbayLoadBatch();

                    //Ebay.FindingService

                    ClientConfig config = new ClientConfig()
                    {
                        // Finding API service end-point configuration
                        EndPointAddress = ConfigurationManager.AppSettings["EbayFindingAPIEndPointAddress"],
                        // eBay developer account AppID
                        ApplicationId = ConfigurationManager.AppSettings["EbayFindindAPIApplicationId"],
                        // timeout value for this call
                        HttpTimeout = 1500000 //25 minutes
                    };

                    // Create a service client
                    FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

                    // Create request object
                    FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest
                    {
                        keywords = ConfigurationManager.AppSettings["EbayFindingApiKeywords"]
                    };

                    List<ItemFilter> itemFilters = new List<ItemFilter>
                    {
                        new ItemFilter()
                        {
                            name = ItemFilterType.AvailableTo,
                            value = new string[] {ConfigurationManager.AppSettings["EbayAvailableToItemFilter"]}
                        }
                    };

                    if (!String.IsNullOrWhiteSpace(ebayLoadStartTimeFromConfiguration))
                    {
                        itemFilters.Add(new ItemFilter()
                        {
                            name = ItemFilterType.StartTimeFrom,
                            value = new string[] { (Convert.ToDateTime(ebayLoadStartTimeFromConfiguration)).ToString(iso8601DatetimeFormat) }
                        });
                    }

                    request.itemFilter = itemFilters.ToArray();                    

                    FindItemsByKeywordsResponse check = client.findItemsByKeywords(request);
                    DateTime ebayCheckTime = DateTime.UtcNow;

                    int totalKeywordMatchedEntriesInEbay = check.paginationOutput.totalEntries;
                    int totalKeywordMatchedEntriesWithGlobalIdEbayUs = 0;
                    int totalPages = (int) Math.Ceiling((double) totalKeywordMatchedEntriesInEbay/100.00);
                    bool flag = true;

                    for (int curPage = 1; curPage <= totalPages; curPage++)
                    {
                        request.paginationInput = new PaginationInput()
                        {
                            entriesPerPageSpecified = true,
                            entriesPerPage = 100,
                            pageNumberSpecified = true,
                            pageNumber = curPage,
                        };

                        FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);

                        if (response.searchResult.item != null && response.searchResult.item.Length > 0)
                        {
                            IEnumerable<SearchItem> searchItems =
                                response.searchResult.item.Where(
                                    i =>
                                        !String.IsNullOrWhiteSpace(i.globalId) &&
                                        i.globalId.ToUpper().Equals(ConfigurationManager.AppSettings["EbayGlobalIdUS"]));

                            foreach (SearchItem ebaySearchItem in searchItems)
                            {
                                StagingEbayItem stagingEbayItem = null;
                                if (stagingEbayLoadService.EbayItemExists(ebaySearchItem.itemId, out stagingEbayItem))
                                {
                                    //TODO: Add to failed as ebay item id is unique
                                }
                                else
                                {
                                    stagingEbayItem = FindingServiceSearchItemMapper.SearchItemToStgEbayItem(ebaySearchItem);
                                    stagingEbayItem.EbayBatchImportId = 4;
                                    //Set the created-on for staging ebay item record
                                    stagingEbayItem.CreatedOn = ebayCheckTime;
                                    stagingEbayItem.CreatedBy = userId;
                                    //Need to set the datetime to null because the default values are the start of time
                                    stagingEbayItem.DeletedOn = null;
                                    stagingEbayItem.ModifiedOn = null;

                                    //Call service create ebay item method
                                    stagingEbayLoadService.CreateStagingEbayItem(stagingEbayItem, true);
                                }


                                totalKeywordMatchedEntriesWithGlobalIdEbayUs++;
                                //if (item.sellingStatus.sellingState)
                                //{
                                //Console.WriteLine(item.viewItemURL + "," + item.sellingStatus.sellingState);

                                //tw.WriteLine(item.viewItemURL.ToString() + "," + item.sellingStatus.sellingState);
                                //}

                            }
                        }
                    }

                    stagingEbayLoadService.UpsertEbayLoadStartTimeFromConfiguration(ebayCheckTime);
                }
            }
        }

        //TODO: Authenticate the passed details

        //Check if there is a load already running.

    }
}
