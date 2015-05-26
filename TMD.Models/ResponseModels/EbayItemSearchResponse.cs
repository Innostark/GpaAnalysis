using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.EntitySql;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.DomainModels;

namespace TMD.Models.ResponseModels
{
    public sealed class EbayItemSearchResponse
    {
        public EbayItemSearchResponse()
        {
            EbayItemImports = new List<StagingEbayItem>();
        }

        /// <summary>
        /// Activities
        /// </summary>
        public IEnumerable<StagingEbayItem> EbayItemImports { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        
        public int TotalCount { get; set; }

        public int FilteredCount { get; set; }

    }
}
