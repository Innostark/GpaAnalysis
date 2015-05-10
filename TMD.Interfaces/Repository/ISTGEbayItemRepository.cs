using System.Collections.Generic;
using TMD.Models.DomainModels;
namespace TMD.Interfaces.Repository
{
    public interface ISTGEbayItemRepository : IBaseRepository<STGEbayItem, int>
    {
        IEnumerable<STGEbayItem> GetAllEbayItems();
    }
}
