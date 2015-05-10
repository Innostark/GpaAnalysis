using System;
using System.ComponentModel;

namespace TMD.Models.DomainModels
{
    public class STGEbayItem
    {
        public int EbayItemtId  {get; set;} 
        public int EbayBatchImportId  {get; set;}  
        public int? ToyGraderItemId  {get; set;} 
        public int CreatedBy  {get; set;}
        public DateTime? CreatedOn { get; set; }
        public int? ModifiedBy  {get; set;}
        public DateTime ModifiedOn  {get; set;}
        public bool Deleted  {get; set;}
        public DateTime? DeletedOn  {get; set;}
        public int? DeletedBy  {get; set;}
        public string Condition  {get; set;}
        public string CountryCode  {get; set;}
        public string GalleryURL  {get; set;}
        public string GlobalId  {get; set;}
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
        public DateTime? SellingStatusTimeLeft  {get; set;} 
        public string StoreInfoStoreName  {get; set;}
        public string StoreInfoStoreURL  {get; set;} 
        public string SubTitle  {get; set;} 
        public string Title  {get; set;} 
        public string ViewItemUrL  {get; set;}

        public virtual STGEbayBatchImport STGEbayBatchImport { get; set; }
    }
}
