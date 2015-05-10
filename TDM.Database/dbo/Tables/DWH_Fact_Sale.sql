CREATE TABLE [dbo].[DWH_Fact_Sale] (
    [SaleId]        INT IDENTITY (1, 1) NOT NULL,
    [DateId]        INT NULL,
    [ProductID]     INT NULL,
    [ProductTypeId] INT NULL,
    [CompanyId]     INT NULL,
    CONSTRAINT [PK_Fact_Sale] PRIMARY KEY CLUSTERED ([SaleId] ASC),
    CONSTRAINT [FK_DWH_Fact_Sale_DWH_Dim_Comapany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[DWH_Dim_Comapany] ([CompanyId]),
    CONSTRAINT [FK_DWH_Fact_Sale_DWH_Dim_Date] FOREIGN KEY ([DateId]) REFERENCES [dbo].[DWH_Dim_Date] ([DateId]),
    CONSTRAINT [FK_DWH_Fact_Sale_DWH_Dim_Product] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[DWH_Dim_Product] ([ProductID]),
    CONSTRAINT [FK_DWH_Fact_Sale_DWH_Dim_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[DWH_Dim_ProductType] ([ProductTypeId])
);

