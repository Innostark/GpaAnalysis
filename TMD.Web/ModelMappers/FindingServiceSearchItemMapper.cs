using System;
using eBay.Services.Finding;
using TMD.Models.DomainModels;

namespace TMD.Web.ModelMappers
{
    public static class FindingServiceSearchItemMapper
    {
        public static void SearchItemToStgEbayItem(SearchItem ebayItem, StagingEbayItem stgItem)
        {
            //Condition
            if (ebayItem.condition != null && ebayItem.condition.conditionIdSpecified)
            {
                stgItem.Condition = ebayItem.condition.conditionDisplayName;
            }
            stgItem.CountryCode = ebayItem.country;
            stgItem.GalleryURL = ebayItem.galleryURL;
            stgItem.GlobalId = ebayItem.globalId;
            stgItem.ItemId = ebayItem.itemId;
            //Listing Information
            if (ebayItem.listingInfo != null)
            {
                ListingInfo listingInfo = ebayItem.listingInfo;
                if (listingInfo.buyItNowAvailableSpecified)
                {
                    stgItem.ListingInfoBuyItNowAvailable = listingInfo.buyItNowAvailable;
                    stgItem.ListingInfoBuyItNowPrice = Convert.ToDecimal(listingInfo.buyItNowPrice.Value);
                }
                else
                {
                    stgItem.ListingInfoBuyItNowAvailable = false;
                }
                if (listingInfo.endTimeSpecified)
                {
                    stgItem.ListingInfoEndTime = listingInfo.endTime;
                }
                if (listingInfo.giftSpecified)
                {
                    stgItem.ListingInfoGift = listingInfo.gift;
                }
                stgItem.ListingInfoListingType = listingInfo.listingType;
                if (listingInfo.startTimeSpecified)
                {
                    stgItem.ListingInfoStartTime = listingInfo.startTime;
                }
            }
            if(ebayItem.primaryCategory != null)
            {
                stgItem.PrimaryCategory = ebayItem.primaryCategory.categoryName;
            }
            if(ebayItem.productId != null)
            {
                stgItem.ProducitId = ebayItem.productId.Value;
            }
            if(ebayItem.secondaryCategory != null)
            {
                stgItem.SecondaryCategory = ebayItem.secondaryCategory.categoryName;
            }
            if (ebayItem.sellerInfo != null)
            {
                stgItem.SellerInfoTopRatedSeller = ebayItem.sellerInfo.topRatedSellerSpecified &&
                                                   ebayItem.sellerInfo.topRatedSeller;
            }
            //Selling Status
            if (ebayItem.sellingStatus != null)
            {
                SellingStatus sellingStatus = ebayItem.sellingStatus;
                if (sellingStatus.bidCountSpecified)
                {
                    stgItem.SellingStatusBidCount = sellingStatus.bidCount;
                }

                stgItem.SellingStatusCurrentPrice = Convert.ToDecimal(sellingStatus.currentPrice.Value);
                stgItem.SellingStatusSellingState = sellingStatus.sellingState;
                stgItem.SellingStatusTimeLeft = Convert.ToDateTime(sellingStatus.timeLeft);
            }
            //Store Info
            if (ebayItem.storeInfo != null)
            {
                Storefront storefront = ebayItem.storeInfo;
                stgItem.StoreInfoStoreName = storefront.storeName;
                stgItem.StoreInfoStoreURL = storefront.storeURL;
            }
            stgItem.SubTitle = ebayItem.subtitle;
            stgItem.Title = ebayItem.title;
            stgItem.ViewItemUrL = ebayItem.viewItemURL;

        }
    }
}