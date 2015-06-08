using System;
using TMD.Models.DomainModels;
using TMD.Models.RequestModels;
using TMD.Models.ResponseModels;

namespace TMD.Interfaces.IServices
{
    public interface IStagingEbayLoadService: IDisposable
    {
        bool CanExecuteEbayLoad();
        void LoadEbayData();
        StagingEbayBatchImport CreateStagingEbayLoadBatch();
        void UpdateStagingEbayLoadBatch(StagingEbayBatchImport batch, bool commit);
        void CreateStagingEbayItem(StagingEbayItem item, bool commit = false);
        bool EbayItemExists(string itemId, out StagingEbayItem item);
        bool UpdateEbayItemImportDetail(int itemId,string afaSerial, string updatedBy);
        string GetEbayLoadStartTimeFrom();
        int UpsertEbayLoadStartTimeFromConfiguration(DateTime ebayLoadStartTimeFrom);

        EbayItemSearchResponse GetImports(StagingEbayItemRequest oReq);

        StagingEbayItem GetEbayImportById(string Id);
    }
}
