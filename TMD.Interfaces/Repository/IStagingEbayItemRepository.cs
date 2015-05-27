using System.Collections.Generic;
using TMD.Models.DomainModels;
using TMD.Models.RequestModels;
using TMD.Models.ResponseModels;

namespace TMD.Interfaces.Repository
{
    public interface IStagingEbayItemRepository : IBaseRepository<StagingEbayItem, int>
    {
        IEnumerable<StagingEbayItem> GetAllEbayItems();
        bool EbayItemExists(string itemId);
        void LoadStagingEbayItemToRepositoryObjectForCreate(StagingEbayItem item, ref  StagingEbayItem repositoryItem);
        EbayItemSearchResponse GetImports(StagingEbayItemRequest oReq);
        StagingEbayItem GetEbayImportById(string Id);
    
    }
}
