using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TMD.Web.Models
{
    public class StagingEbayItemModel
    {
        public int EbayItemtId { get; set; }
        public int EbayBatchImportId { get; set; }
        public int? ToyGraderItemId { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public bool Deleted { get; set; }
        public string DeletedOn { get; set; }
        public string DeletedBy { get; set; }
        public string Condition { get; set; }
        public string CountryCode { get; set; }
        public string GalleryURL { get; set; }
        public string GlobalId { get; set; }
        public string ItemId { get; set; }
        public bool? ListingInfoBuyItNowAvailable { get; set; }
        public decimal? ListingInfoBuyItNowPrice { get; set; }
        public DateTime? ListingInfoEndTime { get; set; }
        public bool? ListingInfoGift { get; set; }
        public string ListingInfoListingType { get; set; }
        public string ListingInfoStartTime { get; set; }
        public string PrimaryCategory { get; set; }
        public string ProducitId { get; set; }
        public string SecondaryCategory { get; set; }
        public bool? SellerInfoTopRatedSeller { get; set; }
        public int? SellingStatusBidCount { get; set; }
        public decimal? SellingStatusCurrentPrice { get; set; }
        public string SellingStatusSellingState { get; set; }
        public string SellingStatusTimeLeft { get; set; }
        public string StoreInfoStoreName { get; set; }
        public string StoreInfoStoreURL { get; set; }
        public string SubTitle { get; set; }
        public string Title { get; set; }
        public string ViewItemUrl { get; set; }
        public string EbayItemDetails { get; set; }
        public string AFASerial { get; set; }
    }
}