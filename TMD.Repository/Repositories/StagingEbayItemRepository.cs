using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Odbc;
using System.Linq;
using TMD.Interfaces.Repository;
using TMD.Models.Common;
using TMD.Models.DomainModels;
using TMD.Models.ResponseModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;
using System;
using System.Linq.Expressions;

namespace TMD.Repository.Repositories
{
    public sealed class StagingEbayItemRepository : BaseRepository<StagingEbayItem>, IStagingEbayItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StagingEbayItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StagingEbayItem> DbSet
        {
            get { return db.StagingEbayItems; }
        }
        #endregion

        public IEnumerable<StagingEbayItem> GetAllEbayItems()
        {
            return DbSet;
        }

        public bool EbayItemExists(string itemId, out StagingEbayItem item)
        {
            return db.EbayItemExists(itemId, out item);
        }

        public void LoadStagingEbayItemToRepositoryObjectForCreate(StagingEbayItem item, ref StagingEbayItem repositoryItem)
        {
            repositoryItem.EbayBatchImportId = item.EbayBatchImportId;  
            repositoryItem.ToyGraderItemId = item.ToyGraderItemId; 
            repositoryItem.CreatedBy = item.CreatedBy;
            repositoryItem.CreatedOn = item.CreatedOn;
            repositoryItem.ModifiedBy = item.ModifiedBy;
            repositoryItem.ModifiedOn = item.ModifiedOn;
            repositoryItem.Deleted = item.Deleted;
            repositoryItem.DeletedOn = item.DeletedOn;
            repositoryItem.DeletedBy = item.DeletedBy;
            repositoryItem.Condition = item.Condition;
            repositoryItem.CountryCode = item.CountryCode;
            repositoryItem.GalleryURL = item.GalleryURL;
            repositoryItem.GlobalId = item.GlobalId;
            repositoryItem.ItemId = item.ItemId;
            repositoryItem.ListingInfoBuyItNowAvailable = item.ListingInfoBuyItNowAvailable;
            repositoryItem.ListingInfoBuyItNowPrice = item.ListingInfoBuyItNowPrice;
            repositoryItem.ListingInfoEndTime = item.ListingInfoEndTime; 
            repositoryItem.ListingInfoGift = item.ListingInfoGift; 
            repositoryItem.ListingInfoListingType = item.ListingInfoListingType; 
            repositoryItem.ListingInfoStartTime = item.ListingInfoStartTime;  
            repositoryItem.PrimaryCategory = item.PrimaryCategory; 
            repositoryItem.ProducitId = item.ProducitId;
            repositoryItem.SecondaryCategory = item.SecondaryCategory; 
            repositoryItem.SellerInfoTopRatedSeller = item.SellerInfoTopRatedSeller;
            repositoryItem.SellingStatusBidCount = item.SellingStatusBidCount;
            repositoryItem.SellingStatusCurrentPrice = item.SellingStatusCurrentPrice; 
            repositoryItem.SellingStatusSellingState = item.SellingStatusSellingState;
            repositoryItem.SellingStatusTimeLeft = item.SellingStatusTimeLeft; 
            repositoryItem.StoreInfoStoreName = item.StoreInfoStoreName;
            repositoryItem.StoreInfoStoreURL = item.StoreInfoStoreURL; 
            repositoryItem.SubTitle = item.SubTitle; 
            repositoryItem.Title = item.Title; 
            repositoryItem.ViewItemUrl = item.ViewItemUrl;
        }



        Dictionary<StagingEbayItemRequestByColumn, Func<StagingEbayItem, object>> batchClause =
            new Dictionary<StagingEbayItemRequestByColumn, Func<StagingEbayItem, object>>
                {
                    
                    {StagingEbayItemRequestByColumn.EbayBatchImportId, c => c.EbayBatchImportId},
                    {StagingEbayItemRequestByColumn.StoreInfoStoreName, c => c.StoreInfoStoreName},
                    {StagingEbayItemRequestByColumn.SubTitle, c => c.SubTitle},
                    {StagingEbayItemRequestByColumn.Title, c => c.Title},
                    {StagingEbayItemRequestByColumn.ViewItemUrl, c => c.ViewItemUrl}
                   
                };
        public Models.ResponseModels.EbayItemSearchResponse GetImports(Models.RequestModels.StagingEbayItemRequest searchRequest)
        {
            int batchId = 0;
            if (!string.IsNullOrEmpty(searchRequest.BatchId))
            {
                if (!int.TryParse(searchRequest.BatchId, out batchId))
                {
                    batchId = -1;
                }
            }

            
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<StagingEbayItem, bool>> query =
                    s => (
                            (
                            (string.IsNullOrEmpty(searchRequest.Title) || s.Title.Contains(searchRequest.Title))
                            && (batchId ==0 || s.EbayBatchImportId.Equals(batchId))
                            && (string.IsNullOrEmpty(searchRequest.AFASerial) || s.AFASerial.Contains(searchRequest.AFASerial))
                            && (string.IsNullOrEmpty(searchRequest.ToyGraderID) || s.ToyGraderItemId.Value.ToString().Contains(searchRequest.AFASerial))
                            && (searchRequest.CreatedOn == null ||  EntityFunctions.TruncateTime(s.CreatedOn) == searchRequest.CreatedOn.Value)
                            

                            
                            )


                        );
            IEnumerable<StagingEbayItem> oList =
                searchRequest.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(batchClause[searchRequest.EbayItemOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(batchClause[searchRequest.EbayItemOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new EbayItemSearchResponse { EbayItemImports = oList, TotalCount = DbSet.Count(), FilteredCount = DbSet.Count(query) };

        }


        public StagingEbayItem GetEbayImportById(string szId)
        {
            int Id = int.Parse(szId);
            return DbSet.Where(x => x.EbayItemtId == Id).FirstOrDefault();
        }
    }
}
