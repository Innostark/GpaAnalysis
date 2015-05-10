CREATE TABLE [dbo].[STG_EBayBatchImports] (
    [EBayBatchImportId] INT           IDENTITY (1, 1) NOT NULL,
    [InProcess]         BIT           CONSTRAINT [DF_STG_EBayBatchImport_InProcess] DEFAULT ((0)) NOT NULL,
    [CreatedOn]         DATETIME      CONSTRAINT [DF_STG_EBayBatchImport_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [StartedOn]         DATETIME      NULL,
    [CompletedOn]       DATETIME      NULL,
    [Imported]          INT           NULL,
    [Failed]            INT           NULL,
    [Auctions]          INT           NULL,
    [FixedPrice]        INT           NULL,
    [EbayTimestamp]     DATETIME      NULL,
    [EbayVersion]       NVARCHAR (25) NULL,
    CONSTRAINT [PK_STG_EBayBatchImport] PRIMARY KEY CLUSTERED ([EBayBatchImportId] ASC)
);



