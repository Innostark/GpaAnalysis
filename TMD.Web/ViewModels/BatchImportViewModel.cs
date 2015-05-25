using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.RequestModels;
using TMD.Web.Models.Common;

namespace TMD.Web.ViewModels
{
    public class BatchImportViewModel
    {
        public BatchImportSearchRequest SearchRequest { get; set; }

        public List<BoolDropDownModel> BoolDropDownModels { get; set; }

        public BatchImportViewModel()
        {
            var obj = new BoolDropDownModel();
            BoolDropDownModels = obj.oList;
        }
    }
}