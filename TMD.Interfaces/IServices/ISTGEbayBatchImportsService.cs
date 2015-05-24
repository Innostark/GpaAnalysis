using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.IServices
{
    public interface ISTGEbayBatchImportsService
    {
        IEnumerable<STGEbayBatchImport> GetAllImports();
    }
}
