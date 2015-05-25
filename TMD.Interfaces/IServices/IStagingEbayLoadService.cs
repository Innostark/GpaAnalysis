using System;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.IServices
{
    public interface IStagingEbayLoadService: IDisposable
    {
        bool CanExecuteEbayLoad();
        void LoadEbayData();
        StagingEbayBatchImport CreateStagingEbayLoadBatch();
        void CreateStagingEbayItem(StagingEbayItem item, bool commit = false);
        bool EbayItemExists(string itemId, out StagingEbayItem item);
        string GetEbayLoadStartTimeFrom();
        int UpsertEbayLoadStartTimeFromConfiguration(DateTime ebayLoadStartTimeFrom);
    }
}
