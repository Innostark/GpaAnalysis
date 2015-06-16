using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Services;
using eBay.Services.Finding;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Practices.Unity;
using TMD.Implementation.Identity;
using TMD.Interfaces.IServices;
using TMD.Models.DomainModels;
using TMD.Web.Integration.Ebay.Faults;
using TMD.Web.ModelMappers;
using eBay.Services;
using System.Configuration;
using TMD.WebBase.Mvc;
using UnityConfig = TMD.WebBase.UnityConfiguration.UnityConfig;


namespace TMD.Web.Integration.Ebay
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select TmdEbayIntegrationService.svc or TmdEbayIntegrationService.svc.cs at the Solution Explorer and start debugging.
    public class TmdEbayIntegrationService : ITmdEbayIntegrationService
    {
        #region 'Private Properties'
        private const string EbayListingTypeAuctionInLower = "auction";
        private const string EbayListingTypeAuctionWithBinInLower = "auctionwithbin";
        private const string EbayListingTypeClassifiedInLower = "classified";
        private const string EbayListingTypeFixedPriceInLower = "fixedprice";
        private const string EbayListingTypeStoreInventory = "storeinventory";
        private const string StartEbayLoadServiceMethodName = "StartEbayLoad";
        private const string TmdEbayIntegrationServiceServiceName = "TmdEbayIntegrationService";
        private readonly string serivceLogMessage = String.Format("{0} service method call - {1}", TmdEbayIntegrationServiceServiceName, StartEbayLoadServiceMethodName);
        private ApplicationUserManager userManager;
        #endregion 'Private Properties'
        
        public void StartEbayLoad(string username, string password)
        {
            var abc = HttpContext.Current.User.Identity.IsAuthenticated;
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
            
            #region 'Parameter validation'
            if (String.IsNullOrWhiteSpace(username))
            {
                //Log the error
                logger.Write(String.Format("{0} call missing the user name, parameter missing a valid value (mandatory).", StartEbayLoadServiceMethodName),
                    LoggerCategories.Error, 0, 0,
                    TraceEventType.Stop,
                    serivceLogMessage,
                    new Dictionary<string, object>());
                //Raise the fault
                throw new FaultException<InputParameterFault>(
                    new InputParameterFault { ErrorDetails = InputParameterFault.FaultCodeUserNameWasNullOrEmpty, ErrorMessage = InputParameterFault.FaultMessageUserNameWasNullOrEmpty, Result = false },
                    new FaultReason(new FaultReasonText(InputParameterFault.FaultMessageInvalidParameter)), new FaultCode(InputParameterFault.FaultCodeInvalidParameter));
            }
            
            if (String.IsNullOrWhiteSpace(password))
            {
                //Log the error
                logger.Write(String.Format("{0} call missing the password, parameter missing a valid value (mandatory).", StartEbayLoadServiceMethodName),
                    LoggerCategories.Error, 0, 0,
                    TraceEventType.Stop,
                    serivceLogMessage,
                    new Dictionary<string, object>());
                throw new FaultException<InputParameterFault>(
                    new InputParameterFault { ErrorDetails = InputParameterFault.FaultCodePasswordWasNullOrEmpty, ErrorMessage = InputParameterFault.FaultMessagePasswordWasNullOrEmpty, Result = false },
                    new FaultReason(new FaultReasonText(InputParameterFault.FaultMessageInvalidParameter)), new FaultCode(InputParameterFault.FaultCodeInvalidParameter));
            }

            #endregion 'Parameter validation'

            #region 'Authentication & Processing'
            //We perform DB authentication & authorisation of user name and password
            userManager = userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            AspNetUser user = userManager.Find(username, password);
            
            if (user == null)
            {
                //Log the error
                logger.Write(String.Format("User could not be authenticated with user name = '{0}' and password = '{1}'.", username, password),
                    LoggerCategories.Error, 0, 0,
                    TraceEventType.Critical,
                    serivceLogMessage,
                    new Dictionary<string, object>());
                //User not authenticated
                throw new FaultException<AuthenticationFault>(
                    new AuthenticationFault { ErrorDetails = AuthenticationFault.FaultCodeCredentialsCouldNotBeValidated, ErrorMessage = AuthenticationFault.FaultMessageCredentialsCouldNotBeValidated, Result = false },
                    new FaultReason(new FaultReasonText(AuthenticationFault.FaultMessageCredentialsCouldNotBeValidated)), new FaultCode(AuthenticationFault.FaultCodeCredentialsCouldNotBeValidated));
            }

            if (!userManager.IsEmailConfirmed(user.Id))
            {
                //Log the error
                logger.Write(String.Format("User's email is not confirmed, user name = '{0}' and password = '{1}'.", username, password),
                    LoggerCategories.Error, 0, 0,
                    TraceEventType.Stop,
                    serivceLogMessage,
                    new Dictionary<string, object>());
                //User is not confirm 
                throw new FaultException<AuthenticationFault>(
                    new AuthenticationFault { ErrorDetails = AuthenticationFault.FaultCodeEmailNotConfirmed, ErrorMessage = AuthenticationFault.FaultMessageEmailNotConfirmed, Result = false },
                    new FaultReason(new FaultReasonText(AuthenticationFault.FaultMessageEmailNotConfirmed)), new FaultCode(AuthenticationFault.FaultCodeEmailNotConfirmed));
            }
            
            if (user.LockoutEnabled)
            {
                //Log the error
                logger.Write(String.Format("User is locked, user name = '{0}' and password = '{1}'.", username, password),
                    LoggerCategories.Error, 0, 0,
                    TraceEventType.Stop,
                    serivceLogMessage,
                    new Dictionary<string, object>());
                //User is locked
                throw new FaultException<AuthenticationFault>(
                    new AuthenticationFault { ErrorDetails = AuthenticationFault.FaultCodeUserIsLocked, ErrorMessage = AuthenticationFault.FaultMessageUserIsLocked, Result = false },
                    new FaultReason(new FaultReasonText(AuthenticationFault.FaultMessageUserIsLocked)), new FaultCode(AuthenticationFault.FaultCodeUserIsLocked));
            }

            AspNetRole role = user.AspNetRoles.FirstOrDefault();
            if (role != null && role.Id != Utility.AdminRoleId)
            {
                //Log the error
                logger.Write(String.Format("User is not authorised for this operation, user name = '{0}' and password = '{1}' (should have Administrator role).", username, password),
                    LoggerCategories.Error, 0, 0,
                    TraceEventType.Stop,
                    serivceLogMessage,
                    new Dictionary<string, object>());
                //User does not have an administrator role
                throw new FaultException<AuthorisationFault>(
                    new AuthorisationFault { ErrorDetails = AuthorisationFault.FaultCodeUserIsNotAdmin, ErrorMessage = AuthorisationFault.FaultMessageUserIsNotAdmin, Result = false },
                    new FaultReason(new FaultReasonText(AuthorisationFault.FaultMessageUserIsNotAdmin)), new FaultCode(AuthorisationFault.FaultCodeUserIsNotAdmin));
            }



            #region 'Processing'

            //User authenticated and authorised, start ebay load processing
            ProcessEbayLoad(logger, user.Id);

            #endregion 'Processing'

            #endregion 'Authentication & Processing'
        }

        public void StartEbayLoadByToken(string token)
        {
            //The token is actually userId : 0 if it is not valid
            var logger = UnityConfig.GetConfiguredContainer().Resolve<ILogger>();
            
            if(logger == null) throw new Exception("Logger is null");

            var decodedUserId = token;
            try
            {


                if (decodedUserId != "0")
                {
                    ProcessEbayLoad(logger, decodedUserId);
                }
                else
                {
                    throw new Exception("Bad token");
                }
            }
            catch (Exception e)
            {


                logger.Write(String.Format("User could not be authenticated"),
                LoggerCategories.Error, 0, 0,
                TraceEventType.Critical,
                serivceLogMessage,
                new Dictionary<string, object>());
                //User not authenticated
                //AuthenticationFault.FaultCodeCredentialsCouldNotBeValidated //BILAL TO CHECK
                throw new FaultException<AuthenticationFault>(
                    new AuthenticationFault { ErrorDetails = e.Message, ErrorMessage = AuthenticationFault.FaultMessageCredentialsCouldNotBeValidated, Result = false },
                    new FaultReason(new FaultReasonText(e.Message)), new FaultCode(AuthenticationFault.FaultCodeCredentialsCouldNotBeValidated));

            }
        
            
        }

        #region 'Private methods'
        
        private void ProcessEbayLoad(ILogger logger, string userId)
        {
            logger.Write(String.Format("Starting the ebay load for user Id = {0}", userId),
                        LoggerCategories.Information, 0, 0,
                        TraceEventType.Information,
                        serivceLogMessage,
                        new Dictionary<string, object>());

            using (
                var stagingEbayLoadService =
                    UnityConfig.GetConfiguredContainer().Resolve<IStagingEbayLoadService>())
            {
                logger.Write(String.Format("ebay load service instantiated for user Id = {0}", userId),
                        LoggerCategories.Information, 0, 0,
                        TraceEventType.Information,
                        serivceLogMessage,
                        new Dictionary<string, object>());

                #region 'Check if an ebay load can run'
                //Check if a new ebay batch load can run (valid only where there is no ebay load currently running)
                if (!stagingEbayLoadService.CanExecuteEbayLoad())
                {
                    //Cannot execute a new ebay batch load
                    //Log the error
                    logger.Write(EbayLoadProcessingFault.FaultMessageBatchAlreadyRunning,
                        LoggerCategories.Error, 0, 0,
                        TraceEventType.Error,
                        serivceLogMessage,
                        new Dictionary<string, object>());
                    //User does not have an administrator role
                    throw new FaultException<EbayLoadProcessingFault>(
                        new EbayLoadProcessingFault { ErrorDetails = EbayLoadProcessingFault.FaultCodeBatchAlreadyRunning, ErrorMessage = EbayLoadProcessingFault.FaultMessageBatchAlreadyRunning, Result = false },
                        new FaultReason(new FaultReasonText(EbayLoadProcessingFault.FaultMessageBatchAlreadyRunning)), new FaultCode(EbayLoadProcessingFault.FaultCodeBatchAlreadyRunning));
                }
                #endregion 'Check if an ebay load can run'

                //Create a new ebay load batch record
                StagingEbayBatchImport stagingEbayBatchImport = stagingEbayLoadService.CreateStagingEbayLoadBatch(userId);
                //Check if a ebay load batch has been created
                if (stagingEbayBatchImport == null)
                {
                    logger.Write(EbayLoadProcessingFault.FaultMessageBatchWasNotCreated,
                        LoggerCategories.Error, 0, 0,
                        TraceEventType.Critical,
                        serivceLogMessage,
                        new Dictionary<string, object>());
                    //User does not have an administrator role
                    throw new FaultException<EbayLoadProcessingFault>(
                        new EbayLoadProcessingFault { ErrorDetails = EbayLoadProcessingFault.FaultCodeBatchWasNotCreated, ErrorMessage = EbayLoadProcessingFault.FaultMessageBatchWasNotCreated, Result = false },
                        new FaultReason(new FaultReasonText(EbayLoadProcessingFault.FaultMessageBatchWasNotCreated)), new FaultCode(EbayLoadProcessingFault.FaultCodeBatchWasNotCreated));
                }
                //Set ebay batch load records default values
                SetBatchDefaults(stagingEbayBatchImport);
                //Update the ebay batch load record with respective data
                stagingEbayLoadService.UpdateStagingEbayLoadBatch(stagingEbayBatchImport, true);

                //ebay Finding API client configuration
                var config = new ClientConfig
                {
                    // Finding API service end-point configuration
                    EndPointAddress = ConfigurationManager.AppSettings["EbayFindingAPIEndPointAddress"],
                    // eBay developer account AppID
                    ApplicationId = ConfigurationManager.AppSettings["EbayFindindAPIApplicationId"],
                    // timeout value for this call
                    HttpTimeout = 1500000 //25 minutes
                };

                //ebay Finding API client service
                FindingServicePortTypeClient findingServicePortTypeClient = FindingServiceClientFactory.getServiceClient(config);

                //ebay finding API request
                var request = new FindItemsByKeywordsRequest
                {
                    keywords = ConfigurationManager.AppSettings["EbayFindingApiKeywords"]
                };

                #region 'ebay Finding API Request Filters'
                    
                var itemFilters = new List<ItemFilter>
                {
                    new ItemFilter
                    {
                        name = ItemFilterType.AvailableTo,
                        value = new[] {ConfigurationManager.AppSettings["EbayAvailableToItemFilter"]}
                    }
                };

                //Get the ebay ISO8601 datetime format from web.config settings
                string iso8601DatetimeFormat = ConfigurationManager.AppSettings["EbayISO8601DateTimeFormat"];

                //Get the start time filter from database for when this ebay batch load was run last
                string ebayLoadStartTimeFromConfiguration = stagingEbayLoadService.GetEbayLoadStartTimeFrom();
                if (!String.IsNullOrWhiteSpace(ebayLoadStartTimeFromConfiguration))
                {
                    itemFilters.Add(new ItemFilter
                    {
                        name = ItemFilterType.StartTimeFrom,
                        value =
                            new[]
                            {
                                //TODO: have to remove this filter below
                                (Convert.ToDateTime(ebayLoadStartTimeFromConfiguration)).AddMinutes(-20).ToString(iso8601DatetimeFormat)
                                //DateTime.Now.AddDays(-1).AddMinutes(-20).ToString(iso8601DatetimeFormat)
                            }
                    });
                }
                request.itemFilter = itemFilters.ToArray();

                #endregion 'ebay Finding API Request Filters'

                //Call the Finding service's Find Items By Keyword method
                FindItemsByKeywordsResponse check = findingServicePortTypeClient.findItemsByKeywords(request);
                DateTime ebayCheckTime = DateTime.UtcNow;

                if (check == null)
                {
                    logger.Write(EbayLoadProcessingFault.FaultMessageFindItemBykeywordResposeIsNull,
                        LoggerCategories.Error, 1, 0,
                        TraceEventType.Critical,
                        serivceLogMessage,
                        new Dictionary<string, object>());
                    //Find item response is ready
                    throw new FaultException<EbayLoadProcessingFault>(
                        new EbayLoadProcessingFault { ErrorDetails = EbayLoadProcessingFault.FaultCodeFindItemBykeywordResposeIsNull, ErrorMessage = EbayLoadProcessingFault.FaultMessageFindItemBykeywordResposeIsNull, Result = false },
                        new FaultReason(new FaultReasonText(EbayLoadProcessingFault.FaultMessageFindItemBykeywordResposeIsNull)), new FaultCode(EbayLoadProcessingFault.FaultCodeFindItemBykeywordResposeIsNull));
                }

                if (check.ack == AckValue.Failure || check.ack == AckValue.PartialFailure)
                {
                    logger.Write(EbayLoadProcessingFault.FaultMessageFindItemBykeywordReturnedFailure + "Failure details: " + check.errorMessage,
                        LoggerCategories.Error, 1, 0,
                        TraceEventType.Critical,
                        serivceLogMessage,
                        new Dictionary<string, object>());

                    //Find item response has a failure
                    throw new FaultException<EbayLoadProcessingFault>(
                        new EbayLoadProcessingFault { ErrorDetails = EbayLoadProcessingFault.FaultCodeFindItemBykeywordReturnedFailure, ErrorMessage = EbayLoadProcessingFault.FaultMessageFindItemBykeywordReturnedFailure, Result = false },
                        new FaultReason(new FaultReasonText(EbayLoadProcessingFault.FaultMessageFindItemBykeywordReturnedFailure)), new FaultCode(EbayLoadProcessingFault.FaultCodeFindItemBykeywordReturnedFailure));
                }

                int totalKeywordMatchedItems = check.paginationOutput.totalEntries;
                var totalPages = (int) Math.Ceiling(totalKeywordMatchedItems/100.00);
                stagingEbayBatchImport.TotalKeywordMatched = totalKeywordMatchedItems;
                stagingEbayBatchImport.EbayVersion = findingServicePortTypeClient.getVersion(new GetVersionRequest()).version;

                logger.Write(
                    String.Format(
                        "ebay Finding Service - findItemsByKeywords call (user id = {0}, batch id = {1}) for selected filters has Total={2} items,  Total Pages (ebay default 100 items each)={3}",
                        userId, stagingEbayBatchImport.EbayBatchImportId, totalKeywordMatchedItems, totalPages),
                    LoggerCategories.Information, 0, 0,
                    TraceEventType.Information,
                    serivceLogMessage,
                    new Dictionary<string, object>());

                for (int curPage = 1; curPage <= totalPages; curPage++)
                {
                    request.paginationInput = new PaginationInput
                    {
                        entriesPerPageSpecified = true,
                        entriesPerPage = 100,
                        pageNumberSpecified = true,
                        pageNumber = curPage,
                    };

                    FindItemsByKeywordsResponse response =
                        findingServicePortTypeClient.findItemsByKeywords(request);
                    if (response != null &&
                        (response.searchResult.item != null && response.searchResult.item.Length > 0))
                    {
                        IEnumerable<SearchItem> searchItems =
                            response.searchResult.item.Where(EBayGlobalIdUsStore).DistinctBy(i => i.itemId);
                        foreach (SearchItem ebaySearchItem in searchItems)
                        {
                            stagingEbayBatchImport.ToBeProcessed++;
                            StagingEbayItem stagingEbayItem;
                            if (stagingEbayLoadService.EbayItemExists(ebaySearchItem.itemId, out stagingEbayItem))
                            {
                                stagingEbayBatchImport.Duplicates++;
                                stagingEbayBatchImport.Failed++;

                                logger.Write(
                                    String.Format(
                                        "ebay Finding Service - item (ebay item id = {2}) already exists (user id = {0}, batch id = {1})",
                                        userId, stagingEbayBatchImport.EbayBatchImportId, ebaySearchItem.itemId),
                                    LoggerCategories.Warning, 0, 0,
                                    TraceEventType.Warning,
                                    serivceLogMessage,
                                    new Dictionary<string, object>());
                                continue;
                            }
                            if ((ebaySearchItem.listingInfo == null ||
                                 String.IsNullOrWhiteSpace(ebaySearchItem.listingInfo.listingType)))
                            {
                                stagingEbayBatchImport.NoListingType++;
                                stagingEbayBatchImport.Failed++;

                                logger.Write(
                                    String.Format(
                                        "ebay Finding Service - item (ebay item id = {2}) has no listing type (user id = {0}, batch id = {1})",
                                        userId, stagingEbayBatchImport.EbayBatchImportId, ebaySearchItem.itemId),
                                    LoggerCategories.Error, 0, 0,
                                    TraceEventType.Error,
                                    serivceLogMessage,
                                    new Dictionary<string, object>());
                                continue;
                            }

                            stagingEbayItem = CreateStagingEbayItem(stagingEbayLoadService, ebaySearchItem,
                                stagingEbayBatchImport.EbayBatchImportId, ebayCheckTime, userId);
                            UpdateCounts(stagingEbayItem, stagingEbayBatchImport);
                        }
                    }
                    //Page processed log entry
                    logger.Write(
                        String.Format("Items page {2} completed (user id = {0}, batch id = {1})", userId,
                            stagingEbayBatchImport.EbayBatchImportId, curPage),
                        LoggerCategories.Information, 0, 0,
                        TraceEventType.Information,
                        serivceLogMessage,
                        new Dictionary<string, object>());
                }

                stagingEbayLoadService.UpsertEbayLoadStartTimeFromConfiguration(ebayCheckTime);

                //Set ebay batch completion data 
                stagingEbayBatchImport.CompletedOn = DateTime.Now;
                stagingEbayBatchImport.InProcess = false;
                stagingEbayLoadService.UpdateStagingEbayLoadBatch(stagingEbayBatchImport, true);

                //Page processed log entry
                logger.Write(String.Format("ebay batch load completed (user id = {0}, batch id = {1}), Summary: TotalKeywordMatched={2}, ToBeProcessed={3}, Failed={4}, Duplicated={11}, Imported={5}, " +
                                           "Auctions={6}, AuctionsWithBIN={7}, Classified={8}, FixedPrice={9}, StoreInventory={10}",
                                           userId, stagingEbayBatchImport.EbayBatchImportId, stagingEbayBatchImport.TotalKeywordMatched, stagingEbayBatchImport.ToBeProcessed,
                                           stagingEbayBatchImport.Failed, stagingEbayBatchImport.Imported, stagingEbayBatchImport.Auctions, stagingEbayBatchImport.AuctionsWithBIN,
                                           stagingEbayBatchImport.Classified, stagingEbayBatchImport.FixedPrice, stagingEbayBatchImport.StoreInventory, stagingEbayBatchImport.Duplicates),
                    LoggerCategories.Information, 0, 0,
                    TraceEventType.Information,
                    serivceLogMessage,
                    new Dictionary<string, object>());
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
                stagingEbayBatchImport.Duplicates = 0;
                stagingEbayBatchImport.Imported = 0;
                stagingEbayBatchImport.NoListingType = 0;
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
                }

            stagingEbayBatchImport.Imported++;
        }

        private bool EBayGlobalIdUsStore(SearchItem item)
        {
            return !String.IsNullOrWhiteSpace(item.globalId) && item.globalId.ToUpper().Equals(ConfigurationManager.AppSettings["EbayGlobalIdUS"].ToUpper());
        } 

        #endregion 'Private methods'
    }
}
