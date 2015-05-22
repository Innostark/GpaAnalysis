CREATE TABLE [dbo].[DataWarehouseDimProduct] (
    [ProductID] INT IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Dim_Prod] PRIMARY KEY CLUSTERED ([ProductID] ASC)
);

