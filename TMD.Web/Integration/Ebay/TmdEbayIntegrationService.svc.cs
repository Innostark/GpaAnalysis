using System;
using System.Collections.Generic;
using System.Linq;
using eBay.Services.Finding;
using Microsoft.Practices.Unity;
using TMD.Interfaces.IServices;
using TMD.Models.DomainModels;
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
                    //StagingEbayBatchImport stagingEbayBatchImport = stagingEbayLoadService.CreateNewStagingEbayLoadBatch();

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
                    // Set request parameters
                    request.keywords = "afa star wars -ready -lot -set -worthy";
                    PaginationInput pi = new PaginationInput();
                    pi.entriesPerPage = 2;
                    pi.entriesPerPageSpecified = true;
                    request.paginationInput = pi;

                    //// Call the service
                    //FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);

                    //// Show output
                    //logger.Info("Ack = " + response.ack);
                    //logger.Info("Find " + response.searchResult.count + " items.");
                    //SearchItem[] items = response.searchResult.item;
                    //for (int i = 0; i < items.Length; i++)
                    //{
                    //    logger.Info(items[i].title);
                    //}

                    //FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();

                    request.keywords = "afa star wars -ready -lot -set -worthy";

                    ItemFilter filterEndTimeFrom = new ItemFilter();
                    filterEndTimeFrom.name = ItemFilterType.StartTimeFrom;
                    filterEndTimeFrom.value = new string[] { DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd'T'HH:mm:ss.fff'Z'") };


                    ItemFilter filterAvailableTo = new ItemFilter();
                    filterAvailableTo.name = ItemFilterType.AvailableTo;
                    filterAvailableTo.value = new string[] { "US" };

                    request.itemFilter = new ItemFilter[] { filterAvailableTo, filterEndTimeFrom };

                    FindItemsByKeywordsResponse check = client.findItemsByKeywords(request);

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

                            foreach (SearchItem item in searchItems)
                            {
                                StagingEbayItem stgItem;
                                //stagingEbayLoadService.EbayItemExists(item.itemId, out stgItem);
                                totalKeywordMatchedEntriesWithGlobalIdEbayUs++;
                                //if (item.sellingStatus.sellingState)
                                //{
                                //Console.WriteLine(item.viewItemURL + "," + item.sellingStatus.sellingState);

                                //tw.WriteLine(item.viewItemURL.ToString() + "," + item.sellingStatus.sellingState);
                                //}

                            }
                        }

                    }
                    //using (FindingServicePortTypeClient client = new FindingServicePortTypeClient())
                    //{
                    //    MessageHeader header = MessageHeader.CreateHeader("My-CustomHeader", "http://www.toymarketdata.com", "Custom Header");
                    //    using (OperationContextScope operationContextScope = new OperationContextScope(client.InnerChannel))
                    //    {
                    //        OperationContext.Current.OutgoingMessageHeaders.Add(header);
                    //        HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();

                    //        httpRequestProperty.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME", "InnoSTAR-8fa0-4259-ad7d-b348e40fe0f4");
                    //        httpRequestProperty.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
                    //        httpRequestProperty.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                    //        OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

                    //        FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();

                    //        request.keywords = "afa star wars -ready -lot -set -worthy";
                    //        ItemFilter filterEndTimeFrom = new ItemFilter();
                    //        filterEndTimeFrom.name = ItemFilterType.EndTimeFrom;
                    //        filterEndTimeFrom.value = new string[] { DateTime.Now.AddDays(-30).ToString("yyyy-MM-ddT00:00:00.000Z") };
                    //        request.itemFilter = new ItemFilter[] { filterEndTimeFrom };

                    //        FindItemsByKeywordsResponse check = client.findItemsByKeywords(request);

                    //        int totalKeywordMatchedEntriesInEbay = check.paginationOutput.totalEntries;
                    //        int totalKeywordMatchedEntriesWithGlobalIdEbayUs = 0;
                    //        int totalPages = (int)Math.Ceiling((double)totalKeywordMatchedEntriesInEbay / 100.00);
                    //        bool flag = true;

                    //        for (int curPage = 1; curPage <= totalPages; curPage++)
                    //        {
                    //            request.paginationInput = new PaginationInput()
                    //            {
                    //                entriesPerPageSpecified = true,
                    //                entriesPerPage = 100,
                    //                pageNumberSpecified = true,
                    //                pageNumber = curPage,
                    //            };

                    //            FindItemsByKeywordsResponse response = client.findItemsByKeywords(request);

                    //            if (response.searchResult.item != null && response.searchResult.item.Length > 0)
                    //            {
                    //                IEnumerable<SearchItem> searchItems =
                    //                    response.searchResult.item.Where(
                    //                        i =>
                    //                            !String.IsNullOrWhiteSpace(i.globalId) &&
                    //                            i.globalId.ToUpper().Equals("EBAY-US"));

                    //                foreach (SearchItem item in searchItems)
                    //                {
                    //                    StagingEbayItem stgItem;
                    //                    //stagingEbayLoadService.EbayItemExists(item.itemId, out stgItem);
                    //                    totalKeywordMatchedEntriesWithGlobalIdEbayUs++;
                    //                    //if (item.sellingStatus.sellingState)
                    //                    //{
                    //                    //Console.WriteLine(item.viewItemURL + "," + item.sellingStatus.sellingState);

                    //                    //tw.WriteLine(item.viewItemURL.ToString() + "," + item.sellingStatus.sellingState);
                    //                    //}

                    //                }
                    //            }

                    //        }

                    //    }

                }
            }
        }

        //TODO: Authenticate the passed details

        //Check if there is a load already running.

    }
}
