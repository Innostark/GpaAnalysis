using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Ajax.Utilities;
using Microsoft.Practices.Unity;
using TMD.Interfaces.IServices;
using TMD.Web.Ebay.FindingService;
using TMD.WebBase.UnityConfiguration;

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

            using (IStagingEbayLoadService stagingEbayLoadService = UnityConfig.GetConfiguredContainer().Resolve<IStagingEbayLoadService>())
            {
                if (stagingEbayLoadService.CanExecuteEbayLoad())
                {
                    //stagingEbayLoadService.CreateNewStagingEbayLoadBatch(new STGEbayBatchImport());

                    using (FindingServicePortTypeClient client = new FindingServicePortTypeClient())
                    {
                        MessageHeader header = MessageHeader.CreateHeader("My-CustomHeader",
                            "http://www.toymarketdata.com", "Custom Header");
                        using (OperationContextScope operationContextScope = new OperationContextScope(client.InnerChannel))
                        {

                            OperationContext.Current.OutgoingMessageHeaders.Add(header);
                            HttpRequestMessageProperty httpRequestProperty = new HttpRequestMessageProperty();

                            httpRequestProperty.Headers.Add("X-EBAY-SOA-SECURITY-APPNAME",
                                "InnoSTAR-8fa0-4259-ad7d-b348e40fe0f4");
                            httpRequestProperty.Headers.Add("X-EBAY-SOA-OPERATION-NAME", "findItemsByKeywords");
                            httpRequestProperty.Headers.Add("X-EBAY-SOA-GLOBAL-ID", "EBAY-US");
                            OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

                            FindItemsByKeywordsRequest request = new FindItemsByKeywordsRequest();

                            request.keywords = "afa star wars -ready -lot -set -worthy";

                            FindItemsByKeywordsResponse check = client.findItemsByKeywords(request);

                            int totalKeywordMatchedEntriesInEbay = check.paginationOutput.totalEntries;
                            int totalKeywordMatchedEntriesWithGlobalIdEbayUs = 0;
                            int totalPages = (int)Math.Ceiling((double)totalKeywordMatchedEntriesInEbay / 100.00);
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
                                    foreach (
                                        var item in
                                            response.searchResult.item.Where(
                                                i => !String.IsNullOrWhiteSpace(i.globalId) &&
                                                     i.globalId.ToUpper().Equals("EBAY-US")))
                                    {
                                        totalKeywordMatchedEntriesWithGlobalIdEbayUs++;
                                        //if (item.sellingStatus.sellingState)
                                        //{
                                        //Console.WriteLine(item.viewItemURL + "," + item.sellingStatus.sellingState);

                                        //tw.WriteLine(item.viewItemURL.ToString() + "," + item.sellingStatus.sellingState);
                                        //}

                                    }
                                }

                            }

                        }

                    }
                }
            }

            //TODO: Authenticate the passed details

            //Check if there is a load already running.


        }
    }
}
