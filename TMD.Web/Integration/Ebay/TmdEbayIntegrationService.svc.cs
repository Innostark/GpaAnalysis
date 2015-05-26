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
                    string ebayLoadStartTimeFromConfiguration = stagingEbayLoadService.GetEbayLoadStartTimeFrom();
                    StagingEbayBatchImport stagingEbayBatchImport = stagingEbayLoadService.CreateStagingEbayLoadBatch();

                    //Ebay.FindingService

                    ClientConfig config = new ClientConfig();
                    // Initialize service end-point configuration
                    config.EndPointAddress = "https://svcs.ebay.com/services/search/FindingService/v1";
                    // set eBay developer account AppID
                    config.ApplicationId = "InnoSTAR-8fa0-4259-ad7d-b348e40fe0f4";

                    // Create a service client
                    FindingServicePortTypeClient client = FindingServiceClientFactory.getServiceClient(config);

                    // Create request object
                    FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();
                    request.keywords = "afa star wars -ready -lot -set -worthy";

                    List<ItemFilter> itemFilters = new List<ItemFilter>();
                    itemFilters.Add( new ItemFilter()
                    {
                        name = ItemFilterType.AvailableTo,
                        value = new string[] { "US" }
                    });

                    if (!String.IsNullOrWhiteSpace(ebayLoadStartTimeFromConfiguration))
                    {
                        itemFilters.Add(new ItemFilter()
                        {
                            name = ItemFilterType.StartTimeFrom,
                            value = new string[] { (Convert.ToDateTime(ebayLoadStartTimeFromConfiguration)).ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'") }
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
                                        i.globalId.ToUpper().Equals("EBAY-US"));

                            foreach (SearchItem ebaySearchItem in searchItems)
                            {
                                StagingEbayItem stagingEbayItem = FindingServiceSearchItemMapper.SearchItemToStgEbayItem(ebaySearchItem);
                                stagingEbayLoadService.CreateStagingEbayItem(stagingEbayItem, true);

                                //if (stagingEbayLoadService.EbayItemExists(item.itemId, out stgItem))
                                //{
                                //}
                                //else
                                //{
                                    
                                //}


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
