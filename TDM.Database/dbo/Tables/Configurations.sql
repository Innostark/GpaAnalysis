CREATE TABLE [dbo].[Configurations] (
    [Id]    INT             IDENTITY (1, 1) NOT NULL,
    [Name]  NVARCHAR (50)   NOT NULL,
    [Type]  NVARCHAR (50)   NOT NULL,
    [Value] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_Configurations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

