using System;
using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;

namespace TMD.Implementation.Services
{
    public class EbayStagingLoadService : IStagingEbayLoadService
    {
       #region 'Private and Constructor'
        private readonly IStagingEbayBatchImportsRepository istgEbayBatchImportsRepository;
        private readonly IStagingEbayItemRepository istgEbayItemRepository;
        private readonly IConfigurationRepository iCongifRepository;

        public EbayStagingLoadService(IStagingEbayBatchImportsRepository istgEbayBatchImportsRepository, IStagingEbayItemRepository istgEbayItemRepository, IConfigurationRepository iCongifRepository)
        {
            this.istgEbayBatchImportsRepository = istgEbayBatchImportsRepository;
            this.istgEbayItemRepository = istgEbayItemRepository;
            this.iCongifRepository = iCongifRepository;
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

        public StagingEbayBatchImport CreateNewStagingEbayLoadBatch()
        {
            var newBatch = istgEbayBatchImportsRepository.Create();

            newBatch.CreatedOn =  DateTime.Now;
            istgEbayBatchImportsRepository.Add(newBatch);

            return newBatch;

        }

        public bool EbayItemExists(string itemId, out StagingEbayItem item)
        {
            this.istgEbayItemRepository.EbayItemExists(itemId);

            item = null;
            return false;

        }

        public string GetEbayLoadStartTimeFrom()
        {
            return this.iCongifRepository.GetEbayLoadStartTimeFrom();
        }

        public int UpsertEbayLoadStartTimeFromConfiguration(DateTime ebayLoadStartTimeFrom)
        {
            return iCongifRepository.UpsertEbayLoadStartTimeFromConfiguration(ebayLoadStartTimeFrom);
        }

        public void Dispose()
        {
            
        }
    }
}
