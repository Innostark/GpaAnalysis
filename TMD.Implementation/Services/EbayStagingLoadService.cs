using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;

namespace TMD.Implementation.Services
{
    public class EbayStagingLoadService : IStagingEbayLoadService
    {
       #region 'Private and Constructor'
        private readonly ISTGEbayBatchImportsRepository istgEbayBatchImportsRepository;
        private readonly ISTGEbayItemRepository istgEbayItemRepository;

        public EbayStagingLoadService(ISTGEbayBatchImportsRepository istgEbayBatchImportsRepository, ISTGEbayItemRepository istgEbayItemRepository)
        {
            this.istgEbayBatchImportsRepository = istgEbayBatchImportsRepository;
            this.istgEbayItemRepository = istgEbayItemRepository;
        }

       #endregion 'Private and Constructor'

        public bool CanExecuteEbayLoad()
        {
            return !this.istgEbayBatchImportsRepository.IsEbayLoadRunning();
        }

        public void LoadEbayData()
        {
            throw new System.NotImplementedException();
        }

        public bool CreateNewStagingEbayLoadBatch()
        {
            //toBeSave = istgEbayBatchImportsRepository.Create();

            //toBeSave.CompletedOn =  DateTime.Now;
            //toBeSave.EbayVersion = "V1.0.0.1";
            //    istgEbayBatchImportsRepository.Add(toBeSave);
            //istgEbayBatchImportsRepository.SaveChanges();

            return true;

        }

        public void Dispose()
        {
            
        }
    }
}
