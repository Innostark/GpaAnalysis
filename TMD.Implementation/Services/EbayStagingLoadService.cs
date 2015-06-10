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
            return !istgEbayBatchImportsRepository.IsEbayLoadRunning();
        }

        public void LoadEbayData()
        {
            throw new System.NotImplementedException();
        }

        public StagingEbayBatchImport CreateStagingEbayLoadBatch(string createdByUserId)
        {
            var newBatch = istgEbayBatchImportsRepository.Create();

            newBatch.CreatedOn =  DateTime.Now;

            if (!String.IsNullOrWhiteSpace(createdByUserId)) newBatch.CreatedBy = createdByUserId;

            istgEbayBatchImportsRepository.Add(newBatch);

            return newBatch;
        }

        public void UpdateStagingEbayLoadBatch(StagingEbayBatchImport batch, bool commit = false)
        {
            istgEbayBatchImportsRepository.Update(batch);
            if (commit)
            {
                istgEbayBatchImportsRepository.SaveChanges();
            }
        }

        public bool EbayItemExists(string itemId, out StagingEbayItem item)
        {
            return istgEbayItemRepository.EbayItemExists(itemId, out item);
        }

        public bool UpdateEbayItemImportDetail(int itemId,string afaSerial, string updatedBy)
        {
            var itemDb = istgEbayItemRepository.Find(itemId);
            if (itemDb != null)
            {
                itemDb.AFASerial = afaSerial;
                itemDb.ModifiedBy = updatedBy;
                itemDb.ModifiedOn = DateTime.Now;

                istgEbayItemRepository.Update(itemDb);
                istgEbayItemRepository.SaveChanges();

                return true;
            }
            return false;
        }

        public void CreateStagingEbayItem(StagingEbayItem item, bool commit = false)
        {
            var newStagingEbayItem = istgEbayItemRepository.Create();
            istgEbayItemRepository.LoadStagingEbayItemToRepositoryObjectForCreate(item, ref newStagingEbayItem);
            istgEbayItemRepository.Add(newStagingEbayItem);
            if (commit)
            {
                istgEbayItemRepository.SaveChanges();
            }
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

        public Models.ResponseModels.EbayItemSearchResponse GetImports(Models.RequestModels.StagingEbayItemRequest oReq)
        {
            return istgEbayItemRepository.GetImports(oReq);
        }


        public StagingEbayItem GetEbayImportById(string Id)
        {
            return istgEbayItemRepository.GetEbayImportById(Id);
        }
    }
}
