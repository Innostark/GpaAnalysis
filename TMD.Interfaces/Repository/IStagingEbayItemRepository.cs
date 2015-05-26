using System.Collections.Generic;
using TMD.Models.DomainModels;
namespace TMD.Interfaces.Repository
{
    public interface IStagingEbayItemRepository : IBaseRepository<StagingEbayItem, int>
    {
        IEnumerable<StagingEbayItem> GetAllEbayItems();
        bool EbayItemExists(string itemId);
        void LoadStagingEbayItemToRepositoryObjectForCreate(StagingEbayItem item, ref  StagingEbayItem repositoryItem);
    }
}
