using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eBay.Services.Finding;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;
using TMD.Implementation.Identity;
using TMD.Interfaces.IServices;
using TMD.Models.DomainModels;
using TMD.Web.ModelMappers;
using eBay.Services;
using System.Configuration;
using UnityConfig = TMD.WebBase.UnityConfiguration.UnityConfig;

namespace TMD.Web.Integration.Ebay
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select TmdEbayIntegrationService.svc or TmdEbayIntegrationService.svc.cs at the Solution Explorer and start debugging.
    public class TmdEbayIntegrationService : ITmdEbayIntegrationService
    {
        private const string EbayListingTypeAuctionInLower = "auction";
        private const string EbayListingTypeAuctionWithBinInLower = "auctionwithbin";
        private const string EbayListingTypeClassifiedInLower = "classified";
        private const string EbayListingTypeFixedPriceInLower = "fixedprice";
        private const string EbayListingTypeStoreInventory = "storeinventory";
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

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

            //TOdo: Syed Jobs to add authentication
            //DB AUTHENTICATION OF USERNAME AND PASSWORD

            #region Authentication

            var user = UserManager.Find(username, password);
            if (user != null)
            {
                if (!UserManager.IsEmailConfirmed(user.Id))
                {
                    //TODO: Return error as user is not confirm
                }
                else if (user.LockoutEnabled)
                {
                    
                        //TODO: Return error as user is locked
                }

                var role = user.AspNetRoles.FirstOrDefault();
                if (role.Id != Utility.AdminRoleId)
                {
                    //TODO: RETURN Error user is of other role
                }

                
                //HERE WE SHOULD HAVE THE LOGIC
                //todo: bilal right positive logic here, AND IN ABOVE ALL CONDITION RETURN ERROR
            }
            else
            {
                //TODO: Return error as user is not found
            }

            #endregion


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

                    if (stagingEbayBatchImport == null)
                    {
                        //TODO: Raise error as batch was not created
                    }
                    else
                    {
                        SetBatchDefaults(stagingEbayBatchImport);
                        stagingEbayLoadService.UpdateStagingEbayLoadBatch(stagingEbayBatchImport, true);

                        var config = new ClientConfig
                        {
                            // Finding API service end-point configuration
                            EndPointAddress = ConfigurationManager.AppSettings["EbayFindingAPIEndPointAddress"],
                            // eBay developer account AppID
                            ApplicationId = ConfigurationManager.AppSettings["EbayFindindAPIApplicationId"],
                            // timeout value for this call
                            HttpTimeout = 1500000 //25 minutes
                        };

                        // Create a service client
                        FindingServicePortTypeClient findingServicePortTypeClient = FindingServiceClientFactory.getServiceClient(config);

                        // Create request object
                        var request = new FindItemsByKeywordsRequest
                        {
                            keywords = ConfigurationManager.AppSettings["EbayFindingApiKeywords"]
                        };

                        var itemFilters = new List<ItemFilter>();
                        itemFilters.Add(new ItemFilter
                        {
                            name = ItemFilterType.AvailableTo,
                            value = new[] { ConfigurationManager.AppSettings["EbayAvailableToItemFilter"] }
                        });

                        if (!String.IsNullOrWhiteSpace(ebayLoadStartTimeFromConfiguration))
                        {
                            itemFilters.Add(new ItemFilter
                            {
                                name = ItemFilterType.StartTimeFrom,
                                value =
                                    new[]
                                    {
                                        //(Convert.ToDateTime(ebayLoadStartTimeFromConfiguration)).ToString(iso8601DatetimeFormat)
                                        DateTime.Now.AddDays(-1).ToString(iso8601DatetimeFormat)
                                    }
                            });
                        }
                        request.itemFilter = itemFilters.ToArray();

                        FindItemsByKeywordsResponse check = findingServicePortTypeClient.findItemsByKeywords(request);
                        DateTime ebayCheckTime = DateTime.UtcNow;
                        int totalKeywordMatched = check.paginationOutput.totalEntries;
                        var totalPages = (int)Math.Ceiling(totalKeywordMatched / 100.00);
                        stagingEbayBatchImport.TotalKeywordMatched = totalKeywordMatched;
                        stagingEbayBatchImport.EbayVersion = findingServicePortTypeClient.getVersion(new GetVersionRequest()).version;

                        for (int curPage = 1; curPage <= totalPages; curPage++)
                        {
                            request.paginationInput = new PaginationInput
                            {
                                entriesPerPageSpecified = true,
                                entriesPerPage = 100,
                                pageNumberSpecified = true,
                                pageNumber = curPage,
                            };

                            FindItemsByKeywordsResponse response = findingServicePortTypeClient.findItemsByKeywords(request);
                            if (response != null && (response.searchResult.item != null && response.searchResult.item.Length > 0))
                            {
                                IEnumerable<SearchItem> searchItems = response.searchResult.item.Where(EBayGlobalIdUsStore).DistinctBy(i => i.itemId);
                                foreach (SearchItem ebaySearchItem in searchItems)
                                {
                                    stagingEbayBatchImport.ToBeProcessed++;
                                    StagingEbayItem stagingEbayItem;
                                    if (stagingEbayLoadService.EbayItemExists(ebaySearchItem.itemId, out stagingEbayItem))
                                    {
                                        stagingEbayBatchImport.Duplicates++;
                                        stagingEbayBatchImport.Failed++;
                                        continue;
                                    }
                                    if ((ebaySearchItem.listingInfo == null ||
                                         String.IsNullOrWhiteSpace(ebaySearchItem.listingInfo.listingType)))
                                    {
                                        stagingEbayBatchImport.NoListingType++;
                                        stagingEbayBatchImport.Failed++;
                                        continue;
                                    }

                                    stagingEbayItem = CreateStagingEbayItem(stagingEbayLoadService, ebaySearchItem, stagingEbayBatchImport.EbayBatchImportId, ebayCheckTime, userId);
                                    UpdateCounts(stagingEbayItem, stagingEbayBatchImport);
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

        private void SetBatchDefaults(StagingEbayBatchImport stagingEbayBatchImport)
        {
            if (stagingEbayBatchImport != null)
            {
                stagingEbayBatchImport.StartedOn = DateTime.Now;
                stagingEbayBatchImport.InProcess = true;
                stagingEbayBatchImport.TotalKeywordMatched = 0;
                stagingEbayBatchImport.ToBeProcessed = 0;
                stagingEbayBatchImport.Auctions = 0;
                stagingEbayBatchImport.AuctionsWithBIN = 0;
                stagingEbayBatchImport.Classified = 0;
                stagingEbayBatchImport.FixedPrice = 0;
                stagingEbayBatchImport.StoreInventory = 0;
                stagingEbayBatchImport.Failed = 0;
                stagingEbayBatchImport.Imported = 0;
                stagingEbayBatchImport.NoListingType = 0;
                stagingEbayBatchImport.Duplicates = 0;
            }
        }

        private StagingEbayItem CreateStagingEbayItem(IStagingEbayLoadService stagingEbayLoadService, SearchItem ebaySearchItem, int batchId, DateTime ebayCheckTime, string userId)
        {
            StagingEbayItem stagingEbayItem = FindingServiceSearchItemMapper.SearchItemToStgEbayItem(ebaySearchItem);
            stagingEbayItem.EbayBatchImportId = batchId;
            //Set the created-on for staging ebay item record
            stagingEbayItem.CreatedOn = ebayCheckTime;
            stagingEbayItem.CreatedBy = userId;
            //Need to set the datetime to null because the default values are the start of time
            stagingEbayItem.DeletedOn = null;
            stagingEbayItem.ModifiedOn = null;

            //Call service create ebay item method
            stagingEbayLoadService.CreateStagingEbayItem(stagingEbayItem, true);
            return stagingEbayItem;
        }

        private void UpdateCounts(StagingEbayItem stagingEbayItem, StagingEbayBatchImport stagingEbayBatchImport)
        {
            /***ebay Listing Type values
                **AdFormat                                        
                    * Advertisement to solicit inquiries on listings such as real estate. Permits no bidding on that item, service, or property. To express interest, a buyer fills out a contact form that eBay forwards to the seller as a lead. This format does not enable buyers and sellers to transact online through eBay and eBay Feedback is not available for ad format listings.
                **Auction
                    * Competitive-bid on-line auction format. Buyers engage in competitive bidding, although Buy It Now may be offered as long as no valid bids have been placed. Online auctions are listed on eBay.com; they can also be listed in a seller's eBay Store if the seller is a Store owner.
                **AuctionsWithBIN
                    * Same as Auction format, but Buy It Now is enabled. AuctionsWithBIN changes to Auction if a valid bid has been placed on the item. Valid bids include bids that are equal to or above any specified reserve price.
                **Classified
                    * Classified Ads connect buyers and sellers, who then complete the sale outside of eBay. This format does not enable buyers and sellers to transact online through eBay and eBay Feedback is not available for these listing types.
                **FixedPrice
                * A fixed-price listing. Auction-style bidding is not allowed. On some sites, this auction format is also known as "Buy It Now Only" (not to be confused with the Buy It Now option available with competitive- bidding auctions). Fixed-price listings appear on eBay.com; they can also be listed in a seller's eBay Store if the seller is a Store owner.
            ***/
            switch (stagingEbayItem.ListingInfoListingType.ToLower())
            {
                case EbayListingTypeAuctionInLower:
                    stagingEbayBatchImport.Auctions++;
                    break;
                case EbayListingTypeAuctionWithBinInLower:
                    stagingEbayBatchImport.AuctionsWithBIN++;
                    break;
                case EbayListingTypeClassifiedInLower:
                    stagingEbayBatchImport.Classified++;
                    break;
                case EbayListingTypeFixedPriceInLower:
                    stagingEbayBatchImport.FixedPrice++;
                    break;
                case EbayListingTypeStoreInventory:
                    stagingEbayBatchImport.StoreInventory++;
                    break;
                default:
                    var a = 1;
                    break;
            }

            stagingEbayBatchImport.Imported++;
        }

        private bool EBayGlobalIdUsStore(SearchItem item)
        {
            return !String.IsNullOrWhiteSpace(item.globalId) && item.globalId.ToUpper().Equals(ConfigurationManager.AppSettings["EbayGlobalIdUS"].ToUpper());
        }
    }
}
