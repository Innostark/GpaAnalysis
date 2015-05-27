using System.Collections.Generic;
using TMD.Models.DomainModels;
using TMD.Models.RequestModels;
using TMD.Models.ResponseModels;

namespace TMD.Interfaces.Repository
{
    public interface IStagingEbayBatchImportsRepository : IBaseRepository<StagingEbayBatchImport, int>
    {
        bool IsEbayLoadRunning();
        IEnumerable<StagingEbayBatchImport> GetAllImports();
        BatchImportSearchResponse GetImports(BatchImportSearchRequest oReq);
        
    }
}