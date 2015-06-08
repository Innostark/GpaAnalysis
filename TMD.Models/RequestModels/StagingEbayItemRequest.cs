using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.Common;

namespace TMD.Models.RequestModels
{
    public class StagingEbayItemRequest : GetPagedListRequest
    {
        public string Title { get; set; }
        public string BatchId { get; set; }
        public string ToyGraderID { get; set; }
        public string AFASerial { get; set; }
        public DateTime ? CreatedOn { get; set; }

        
        public StagingEbayItemRequestByColumn EbayItemOrderBy
        {
            get
            {
                return (StagingEbayItemRequestByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
