using System.Collections.Generic;
using System.Data.Entity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;

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

        public bool EbayItemExists(string itemId)
        {
            return this.db.EbayItemExists(itemId);
            
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
    }
}
