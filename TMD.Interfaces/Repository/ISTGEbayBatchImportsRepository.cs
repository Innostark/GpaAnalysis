using System.Collections.Generic;
using TMD.Models.DomainModels;
namespace TMD.Interfaces.Repository
{
    public interface ISTGEbayBatchImportsRepository : IBaseRepository<STGEbayBatchImport, int>
    {
        IEnumerable<STGEbayBatchImport> GetAllImports();
    }
}