CREATE TABLE [dbo].[StagingToyGraderBatchImports] (
    [ToyGraderBatchImporttId] INT        IDENTITY (1, 1) NOT NULL,
    [InProcess]               BIT        CONSTRAINT [DF_STG_ToyGraderBatchImport_InProcess] DEFAULT ((0)) NULL,
    [CreatedOn]               DATETIME   CONSTRAINT [DF_STG_ToyGraderBatchImport_CreatedOn] DEFAULT (getutcdate()) NOT NULL,
    [StartedOn]               DATETIME   NULL,
    [CompletedOn]             NCHAR (10) NULL,
    [Imported]                INT        NULL,
    [Failed]                  INT        NULL,
    CONSTRAINT [PK_STG_ToyGraderBatchImport] PRIMARY KEY CLUSTERED ([ToyGraderBatchImporttId] ASC)
);

