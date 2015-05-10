using System;
using System.Collections.Generic;

namespace TMD.Models.DomainModels
{
    public class STGEbayBatchImport
    {
        public int EbayBatchImportId {get; set;}
        public bool InProcess {get; set;}
        public DateTime? CreatedOn { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int? Imported { get; set; }
        public int? Failed { get; set; }
        public int? Auctions { get; set; }
        public int? FixedPrice { get; set; }
        public DateTime? EbayTimestamp { get; set; }        
        public string EbayVersion { get; set; }

        public ICollection<STGEbayItem> STGEbayItems { get; set; }
    }
}
