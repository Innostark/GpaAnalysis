using TMD.Models.DomainModels;

namespace TMD.Interfaces.IServices
{
    public interface IStagingEbayLoadService
    {
        void LoadEbayData();

        bool CreateSTGEbayBatchImport(STGEbayBatchImport toBeSave);
    }
}
