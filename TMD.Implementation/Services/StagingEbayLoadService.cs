using System;
using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;

namespace TMD.Implementation.Services
{
    public class StagingEbayLoadService : IStagingEbayLoadService
    {
       #region 'Private and Constructor'
        private readonly ISTGEbayBatchImportsRepository istgEbayBatchImportsRepository;
        private readonly ISTGEbayItemRepository istgEbayItemRepository;
        

        public StagingEbayLoadService(ISTGEbayBatchImportsRepository istgEbayBatchImportsRepository, ISTGEbayItemRepository istgEbayItemRepository)
        {
            this.istgEbayBatchImportsRepository = istgEbayBatchImportsRepository;
            this.istgEbayItemRepository = istgEbayItemRepository;
        }

       #endregion 'Private and Constructor'


        public void LoadEbayData()
        {
            throw new System.NotImplementedException();
        }

        public bool CreateSTGEbayBatchImport(STGEbayBatchImport toBeSave)
        {
            toBeSave = istgEbayBatchImportsRepository.Create();

            toBeSave.CompletedOn =  DateTime.Now;
            toBeSave.EbayVersion = "V1.0.0.1";
                istgEbayBatchImportsRepository.Add(toBeSave);
            istgEbayBatchImportsRepository.SaveChanges();

            return true;

        }
    }
}
