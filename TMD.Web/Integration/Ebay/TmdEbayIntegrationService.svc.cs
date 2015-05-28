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

                    int totalPages;
                    if (stagingEbayBatchImport == null)
                    {
                        //TODO: Raise error as batch was not created
                    }
                    else
                    {
                        stagingEbayBatchImport.StartedOn = DateTime.Now;
                        stagingEbayBatchImport.InProcess = true;
                        stagingEbayBatchImport.Auctions = 0;
                        stagingEbayBatchImport.Failed = 0;
                        stagingEbayBatchImport.FixedPrice = 0;
                        stagingEbayBatchImport.Imported = 0;
                        stagingEbayLoadService.UpdateStagingEbayLoadBatch(stagingEbayBatchImport, true);

                        var config = new ClientConfig()
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
                        var request = new FindItemsByKeywordsRequest
                        {
                            keywords = ConfigurationManager.AppSettings["EbayFindingApiKeywords"]
                        };

                        var itemFilters = new List<ItemFilter>
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
                                value =
                                    new string[]
                                    {
                                        (Convert.ToDateTime(ebayLoadStartTimeFromConfiguration)).ToString(
                                            iso8601DatetimeFormat)
                                    }
                            });
                        }

                        request.itemFilter = itemFilters.ToArray();

                        FindItemsByKeywordsResponse check = client.findItemsByKeywords(request);
                        DateTime ebayCheckTime = DateTime.UtcNow;

                        int totalKeywordMatchedEntriesInEbay = check.paginationOutput.totalEntries;
                        int totalKeywordMatchedEntriesWithGlobalIdEbayUs = 0;
                        totalPages = (int) Math.Ceiling((double) totalKeywordMatchedEntriesInEbay/100.00);

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

                            if (response != null && (response.searchResult.item != null && response.searchResult.item.Length > 0))
                            {
                                IEnumerable<SearchItem> searchItems = response.searchResult.item.Where(EBayGlobalIdUsStore);

                                foreach (SearchItem ebaySearchItem in searchItems)
                                {
                                    StagingEbayItem stagingEbayItem = null;
                                    if (stagingEbayLoadService.EbayItemExists(ebaySearchItem.itemId, out stagingEbayItem))
                                    {
                                        stagingEbayBatchImport.Failed++;
                                    }
                                    else
                                    {
                                        stagingEbayItem =
                                            FindingServiceSearchItemMapper.SearchItemToStgEbayItem(ebaySearchItem);
                                        stagingEbayItem.EbayBatchImportId = stagingEbayBatchImport.EbayBatchImportId;
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
                                    stagingEbayBatchImport.Imported++;

                                    if (String.IsNullOrWhiteSpace(stagingEbayItem.ListingInfoListingType)) continue;
                                    /***ebay Listing Type values
                                         **AdFormat                                        
                                         * Advertisement to solicit inquiries on listings such as real estate. Permits no bidding on that item, service, or property. To express interest, a buyer fills out a contact form that eBay forwards to the seller as a lead. This format does not enable buyers and sellers to transact online through eBay and eBay Feedback is not available for ad format listings.
                                         **Auction
                                         * Competitive-bid on-line auction format. Buyers engage in competitive bidding, although Buy It Now may be offered as long as no valid bids have been placed. Online auctions are listed on eBay.com; they can also be listed in a seller's eBay Store if the seller is a Store owner.
                                         **AuctionWithBIN
                                         * Same as Auction format, but Buy It Now is enabled. AuctionWithBIN changes to Auction if a valid bid has been placed on the item. Valid bids include bids that are equal to or above any specified reserve price.
                                         **Classified
                                         * Classified Ads connect buyers and sellers, who then complete the sale outside of eBay. This format does not enable buyers and sellers to transact online through eBay and eBay Feedback is not available for these listing types.
                                         **FixedPrice
                                         * A fixed-price listing. Auction-style bidding is not allowed. On some sites, this auction format is also known as "Buy It Now Only" (not to be confused with the Buy It Now option available with competitive- bidding auctions). Fixed-price listings appear on eBay.com; they can also be listed in a seller's eBay Store if the seller is a Store owner.
                                        ***/
                                    if (
                                        stagingEbayItem.ListingInfoListingType.ToLower().Equals("Auction".ToLower()) ||
                                        stagingEbayItem.ListingInfoListingType.ToLower()
                                            .Equals("AuctionWithBIN".ToLower()))
                                    {
                                        stagingEbayBatchImport.Auctions++;
                                    }
                                    else if (
                                        stagingEbayItem.ListingInfoListingType.ToLower()
                                            .Equals("FixedPrice".ToLower()))
                                    {
                                        stagingEbayBatchImport.FixedPrice++;
                                    }
                                }
                            }
                        }
                        stagingEbayLoadService.UpsertEbayLoadStartTimeFromConfiguration(ebayCheckTime);
                    }
                    if (stagingEbayBatchImport == null) return;
                    stagingEbayBatchImport.CompletedOn = DateTime.Now;
                    stagingEbayBatchImport.InProcess = false;
                    stagingEbayLoadService.UpdateStagingEbayLoadBatch(stagingEbayBatchImport, true);
                }
            }
        }

        private bool EBayGlobalIdUsStore(SearchItem item)
        {
            return !String.IsNullOrWhiteSpace(item.globalId) && item.globalId.ToUpper().Equals(ConfigurationManager.AppSettings["EbayGlobalIdUS"].ToUpper());
        }

        //TODO: Authenticate the passed details

        //Check if there is a load already running.

    }
}
