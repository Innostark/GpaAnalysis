using System.Collections.Generic;
using TMD.Models.DomainModels;
using TMD.Models.RequestModels;

namespace TMD.Interfaces.Repository
{
    public interface IStagingEbayBatchImportsRepository : IBaseRepository<StagingEbayBatchImport, int>
    {
        bool IsEbayLoadRunning();
        IEnumerable<StagingEbayBatchImport> GetAllImports();
        IEnumerable<StagingEbayBatchImport> GetImports(BatchImportSearchRequest oReq);
    }
}