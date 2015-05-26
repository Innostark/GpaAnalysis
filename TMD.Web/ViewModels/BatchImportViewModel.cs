using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.RequestModels;
using TMD.Web.Models;
using TMD.Web.Models.Common;

namespace TMD.Web.ViewModels
{
    public class BatchImportViewModel
    {
        public BatchImportSearchRequest SearchRequest { get; set; }

        public List<BoolDropDownModel> BoolDropDownModels { get; set; }

        public List<StagingEbayBatchImportModel> data { get; set; }

        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int recordsTotal;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int recordsFiltered;

        public BatchImportViewModel()
        {
            var obj = new BoolDropDownModel();
            obj.InitilizeList();
            BoolDropDownModels = obj.oList;
        }
    }
}