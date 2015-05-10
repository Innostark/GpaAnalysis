using System;

namespace TMD.Interfaces.IServices
{
    public interface IStagingEbayLoadService: IDisposable
    {
        bool CanExecuteEbayLoad();

        void LoadEbayData();

        bool CreateNewStagingEbayLoadBatch();
    }
}
