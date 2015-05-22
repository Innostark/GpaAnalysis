using System.Collections.Generic;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.Repository
{
    public interface IStagingEbayBatchImportsRepository : IBaseRepository<StagingEbayBatchImport, int>
    {
        bool IsEbayLoadRunning();
        IEnumerable<StagingEbayBatchImport> GetAllImports();
    }
}