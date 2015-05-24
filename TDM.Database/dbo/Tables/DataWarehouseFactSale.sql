CREATE TABLE [dbo].[DataWarehouseFactSale] (
    [SaleId]        INT IDENTITY (1, 1) NOT NULL,
    [DateId]        INT NULL,
    [ProductID]     INT NULL,
    [ProductTypeId] INT NULL,
    [CompanyId]     INT NULL,
    CONSTRAINT [PK_Fact_Sale] PRIMARY KEY CLUSTERED ([SaleId] ASC),
    CONSTRAINT [FK_DataWarehouseFactSale_DataWarehouseDimComapany] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[DataWarehouseDimComapany] ([CompanyId]),
    CONSTRAINT [FK_DataWarehouseFactSale_DataWarehouseDimDate] FOREIGN KEY ([DateId]) REFERENCES [dbo].[DataWarehouseDimDate] ([DateId]),
    CONSTRAINT [FK_DataWarehouseFactSale_DataWarehouseDimProduct] FOREIGN KEY ([ProductID]) REFERENCES [dbo].[DataWarehouseDimProduct] ([ProductID]),
    CONSTRAINT [FK_DataWarehouseFactSale_DataWarehouseDimProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [dbo].[DataWarehouseDimProductType] ([ProductTypeId])
);

