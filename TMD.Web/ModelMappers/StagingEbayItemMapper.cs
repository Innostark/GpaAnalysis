using System;
using System.Collections.Generic;
using System.Configuration;
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
            var hostURL = ConfigurationManager.AppSettings["HostURL"];

            var oModel = new StagingEbayItemModel
            {
                EbayItemtId = source.EbayItemtId,
                EbayBatchImportId = source.EbayBatchImportId,
                ToyGraderItemId = source.ToyGraderItemId,
                CreatedBy = string.IsNullOrEmpty(source.CreatedBy) ? "" : source.AspNetUserCreatedBy.FirstName + " " + source.AspNetUserCreatedBy.LastName,
                CreatedOn = source.CreatedOn.ToShortDateString(),
                ModifiedBy = string.IsNullOrEmpty(source.ModifiedBy)?"":source.AspNetUserModifiedBy.FirstName+" "+source.AspNetUserModifiedBy.LastName,
                ModifiedOn = source.ModifiedOn != null ? source.ModifiedOn.Value.ToShortDateString() : "",
                Deleted = source.Deleted,
                DeletedOn = source.DeletedOn != null ? source.DeletedOn.Value.ToShortDateString() : "",
                DeletedBy = source.DeletedBy,
                Condition = source.Condition,
                CountryCode = source.CountryCode,
                GalleryURL = source.GalleryURL,
                GlobalId = source.GlobalId,
                ItemId = source.ItemId,
                ListingInfoBuyItNowAvailable = Convert.ToBoolean(source.ListingInfoBuyItNowAvailable),
                ListingInfoBuyItNowPrice = source.ListingInfoBuyItNowPrice,
                ListingInfoEndTime = source.ListingInfoEndTime,
                ListingInfoGift = Convert.ToBoolean(source.ListingInfoGift),
                ListingInfoListingType = source.ListingInfoListingType,
                ListingInfoStartTime =
                    source.ListingInfoStartTime != null ? source.ListingInfoStartTime.Value.ToShortDateString() : "",
                PrimaryCategory = source.PrimaryCategory,
                ProducitId = source.ProducitId,
                SecondaryCategory = source.SecondaryCategory,
                SellerInfoTopRatedSeller = Convert.ToBoolean(source.SellerInfoTopRatedSeller),
                SellingStatusBidCount = source.SellingStatusBidCount,
                SellingStatusCurrentPrice = source.SellingStatusCurrentPrice,
                SellingStatusSellingState = source.SellingStatusSellingState,
                SellingStatusTimeLeft = source.SellingStatusTimeLeft,
                StoreInfoStoreName = source.StoreInfoStoreName,
                StoreInfoStoreURL = source.StoreInfoStoreURL,
                StoreInfoStoreURLTag = string.IsNullOrEmpty(source.StoreInfoStoreURL) ? "" : "<a href='" + source.StoreInfoStoreURL + "' target='_blank'><i class='fa fa-external-link'></i></a>",
                AFASerial =  source.AFASerial,
                SubTitle = source.SubTitle,
                Title = "<a href='" + hostURL + "Admin/EbayItemImportDetail?vpek=" + source.EbayItemtId + "'>"+source.Title+"</a>",
                TitleDescription = source.Title,
                ViewItemUrlTag = string.IsNullOrEmpty(source.ViewItemUrl) ? "" : "<a href='" + source.ViewItemUrl + "' target='_blank'><i class='fa fa-external-link'></i></a>",
                ViewItemUrl = source.ViewItemUrl,
                EbayItemDetails = "<a href='" + hostURL + "Admin/EbayItemImportDetail?vpek=" + source.EbayItemtId+ "'>Details</a>"
            };
            return oModel;


        }
    }
}