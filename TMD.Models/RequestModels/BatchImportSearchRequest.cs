using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.Common;

namespace TMD.Models.RequestModels
{
    public class BatchImportSearchRequest : GetPagedListRequest
    {
        public int InProcess { get; set; }
        public BatchImportSearchRequestByColumn BatchImportOrderBy
        {
            get
            {
                return (BatchImportSearchRequestByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }

        
    }
}
