CREATE TABLE [dbo].[STG_EBayItems] (
    [EBayItemtId]                  INT             IDENTITY (1, 1) NOT NULL,
    [EBayBatchImportId]            INT             NOT NULL,
    [ToyGraderItemId]              INT             NULL,
    [CreatedBy]                    INT             NOT NULL,
    [CreatedOn]                    DATETIME        CONSTRAINT [DF_STG_EBayItem_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [ModifiedBy]                   INT             NULL,
    [ModifiedOn]                   DATETIME        CONSTRAINT [DF_STG_EBayItem_ModifiedOn] DEFAULT (getutcdate()) NULL,
    [Deleted]                      BIT             CONSTRAINT [DF_STG_EBayItem_Deleted] DEFAULT ((0)) NOT NULL,
    [DeletedOn]                    DATETIME        NULL,
    [DeletedBy]                    INT             NULL,
    [Condition]                    NVARCHAR (50)   NULL,
    [CountryCode]                  NVARCHAR (50)   NULL,
    [GalleryURL]                   NCHAR (2048)    NULL,
    [GlobalId]                     NVARCHAR (12)   NULL,
    [ItemId]                       NVARCHAR (19)   NOT NULL,
    [ListingInfoBuyItNowAvailable] BIT             NULL,
    [ListingInfoBuyItNowPrice]     MONEY           NULL,
    [ListingInfoEndTime]           DATETIME        NULL,
    [ListingInfoGift]              BIT             NULL,
    [ListingInfoListingType]       NVARCHAR (20)   NULL,
    [ListingInfoStartTime]         DATETIME        NULL,
    [PrimaryCategory]              NVARCHAR (30)   NULL,
    [ProducitId]                   NVARCHAR (50)   NULL,
    [SecondaryCategory]            NVARCHAR (30)   NULL,
    [SellerInfoTopRatedSeller]     BIT             NULL,
    [SellingStatusBidCount]        INT             NULL,
    [SellingStatusCurrentPrice]    MONEY           NULL,
    [SellingStatusSellingState]    NVARCHAR (20)   NULL,
    [SellingStatusTimeLeft]        DATETIME        NULL,
    [StoreInfoStoreName]           NVARCHAR (200)  NULL,
    [StoreInfoStoreURL]            NVARCHAR (2048) NULL,
    [SubTitle]                     NVARCHAR (1024) NULL,
    [Title]                        NVARCHAR (1024) NULL,
    [ViewItemUrL]                  NVARCHAR (2048) NULL,
    CONSTRAINT [PK_STG_EBayItem] PRIMARY KEY CLUSTERED ([EBayItemtId] ASC),
    CONSTRAINT [FK_STG_EBayItem_STG_EBayBatchImport] FOREIGN KEY ([EBayItemtId]) REFERENCES [dbo].[STG_EBayBatchImports] ([EBayBatchImportId])
);







