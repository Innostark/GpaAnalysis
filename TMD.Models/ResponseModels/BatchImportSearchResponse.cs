using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.DomainModels;

namespace TMD.Models.ResponseModels
{
    public sealed class BatchImportSearchResponse
    {
        public BatchImportSearchResponse()
        {
            EbayBatchImports = new List<StagingEbayBatchImport>();
        }

        /// <summary>
        /// Activities
        /// </summary>
        public IEnumerable<StagingEbayBatchImport> EbayBatchImports { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }


    }
}
