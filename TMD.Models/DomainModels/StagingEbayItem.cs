using System;

namespace TMD.Models.DomainModels
{
    public class StagingEbayItem
    {
        public int EbayItemtId  {get; set;} 
        public int EbayBatchImportId  {get; set;}  
        public int? ToyGraderItemId  {get; set;} 
        public string CreatedBy  {get; set;}
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy  {get; set;}
        public DateTime? ModifiedOn  {get; set;}
        public bool Deleted  {get; set;}
        public DateTime? DeletedOn  {get; set;}
        public string DeletedBy  {get; set;}
        public string Condition  {get; set;}
        public string CountryCode  {get; set;}
        public string GalleryURL  {get; set;}
        public string GlobalId  {get; set;}
        public string ItemId { get; set; }
        public bool? ListingInfoBuyItNowAvailable  {get; set;}
        public decimal? ListingInfoBuyItNowPrice  {get; set;}
        public DateTime? ListingInfoEndTime  {get; set;} 
        public bool? ListingInfoGift  {get; set;} 
        public string ListingInfoListingType  {get; set;} 
        public DateTime? ListingInfoStartTime  {get; set;}  
        public string PrimaryCategory  {get; set;} 
        public string ProducitId  {get; set;}
        public string SecondaryCategory  {get; set;} 
        public bool? SellerInfoTopRatedSeller  {get; set;}
        public int? SellingStatusBidCount  {get; set;}
        public decimal? SellingStatusCurrentPrice  {get; set;} 
        public string SellingStatusSellingState  {get; set;}
        public string SellingStatusTimeLeft  {get; set;} 
        public string StoreInfoStoreName  {get; set;}
        public string StoreInfoStoreURL  {get; set;} 
        public string SubTitle  {get; set;} 
        public string Title  {get; set;} 
        public string ViewItemUrl  {get; set;}

        public virtual StagingEbayBatchImport StagingEbayBatchImport { get; set; }
        public virtual AspNetUser AspNetUserCreatedBy { get; set; }
        public virtual AspNetUser AspNetUserDeletedBy { get; set; }
        public virtual AspNetUser AspNetUserModifiedBy { get; set; }
    }
}
