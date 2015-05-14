using System;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.IServices
{
    public interface IStagingEbayLoadService: IDisposable
    {
        bool CanExecuteEbayLoad();

        void LoadEbayData();

        STGEbayBatchImport CreateNewStagingEbayLoadBatch();

        bool EbayItemExists(string itemId, out STGEbayItem item);
    }
}
