using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.DomainModels;

namespace TMD.Web.Models
{
    public class StagingEbayBatchImportModel
    {
        public string EbayBatchImportId { get; set; }
        public string InProcess { get; set; }
        public string CreatedOn { get; set; }
        public string StartedOn { get; set; }
        public string CompletedOn { get; set; }
        public int? Imported { get; set; }
        public int? Failed { get; set; }
        public int? Auctions { get; set; }
        public int? FixedPrice { get; set; }
        public string EbayTimestamp { get; set; }
        public string EbayVersion { get; set; }

        public bool? Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }

    //    public virtual ICollection<StagingEbayItem> StagingEbayItems { get; set; }
    }
}