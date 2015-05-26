using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.DomainModels;
using TMD.Web.Models;

namespace TMD.Web.ModelMappers
{
    public static class StagingEbayItemMapper
    {


        public static StagingEbayItemModel CreateFrom(this StagingEbayItem source)
        {
            var oModel = new StagingEbayItemModel
            {
                EbayItemtId = source.EbayItemtId,
                EbayBatchImportId = source.EbayBatchImportId,
                ToyGraderItemId = source.ToyGraderItemId,
                CreatedBy = source.CreatedBy,
                CreatedOn = source.CreatedOn != null ? source.CreatedOn.Value.ToShortDateString() : "",
                ModifiedBy = source.ModifiedBy,
                ModifiedOn = source.ModifiedOn != null ? source.ModifiedOn.ToShortDateString() : "",
                Deleted = source.Deleted,
                DeletedOn = source.DeletedOn != null ? source.DeletedOn.Value.ToShortDateString() : "",
                DeletedBy = source.DeletedBy,
                Condition = source.Condition,
                CountryCode = source.CountryCode,
                GalleryURL = source.GalleryURL,
                GlobalId = source.GlobalId,
                ItemId = source.ItemId,
                ListingInfoBuyItNowAvailable = source.ListingInfoBuyItNowAvailable,
                ListingInfoBuyItNowPrice = source.ListingInfoBuyItNowPrice,
                ListingInfoEndTime = source.ListingInfoEndTime,
                ListingInfoGift = source.ListingInfoGift,
                ListingInfoListingType = source.ListingInfoListingType,
                ListingInfoStartTime =
                    source.ListingInfoStartTime != null ? source.ListingInfoStartTime.Value.ToShortDateString() : "",
                PrimaryCategory = source.PrimaryCategory,
                ProducitId = source.ProducitId,
                SecondaryCategory = source.SecondaryCategory,
                SellerInfoTopRatedSeller = source.SellerInfoTopRatedSeller,
                SellingStatusBidCount = source.SellingStatusBidCount,
                SellingStatusCurrentPrice = source.SellingStatusCurrentPrice,
                SellingStatusSellingState = source.SellingStatusSellingState,
                SellingStatusTimeLeft = source.SellingStatusTimeLeft,
                StoreInfoStoreName = source.StoreInfoStoreName,
                StoreInfoStoreURL = source.StoreInfoStoreURL,
                SubTitle = source.SubTitle,
                Title = source.Title,
                ViewItemUrl = source.ViewItemUrl
            };
            return new StagingEbayItemModel();


        }
    }
}