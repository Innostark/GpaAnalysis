using System;
using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;

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

        public STGEbayBatchImport CreateNewStagingEbayLoadBatch()
        {
            var toBeSave = istgEbayBatchImportsRepository.Create();

            toBeSave.CompletedOn =  DateTime.Now;
            toBeSave.EbayVersion = "V1.0.0.1";
               istgEbayBatchImportsRepository.Add(toBeSave);
            istgEbayBatchImportsRepository.SaveChanges();

            return new STGEbayBatchImport();

        }

        public bool EbayItemExists(string itemId, out STGEbayItem item)
        {
            this.istgEbayItemRepository.EbayItemExists(itemId);

            item = null;
            return false;

        }

        public void Dispose()
        {
            
        }
    }
}
