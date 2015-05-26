using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.RequestModels;
using TMD.Models.ResponseModels;

namespace TMD.Interfaces.IServices
{
    public interface IStagingEbayBatchImportsService
    {
        BatchImportSearchResponse GetImports(BatchImportSearchRequest oReq);
    }
}
