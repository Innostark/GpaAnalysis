using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;
using TMD.Models.Common;
using TMD.Models.DomainModels;

namespace TMD.Implementation.Services
{
    public class STGEbayBatchImportsService : ISTGEbayBatchImportsService
    {
        private readonly IStagingEbayBatchImportsRepository oRepository;

        public STGEbayBatchImportsService(IStagingEbayBatchImportsRepository iRepository)
            {
                oRepository = iRepository;
                
            }


     

        public IEnumerable<Models.DomainModels.StagingEbayBatchImport> GetImports(Models.RequestModels.BatchImportSearchRequest oReq)
        {

           return oRepository.GetImports(oReq);
            //throw new NotImplementedException();
        }
    }
}
