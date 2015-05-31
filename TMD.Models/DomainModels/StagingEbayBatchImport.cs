using System;
using System.Collections.Generic;
using System.Dynamic;

namespace TMD.Models.DomainModels
{
    public class StagingEbayBatchImport
    {
        public int EbayBatchImportId { get; set; }
        public bool InProcess { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? StartedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public int? TotalKeywordMatched { get; set; }
        public int? ToBeProcessed { get; set; }
        public int? Imported { get; set; }
        public int? Failed { get; set; }
        public int? Duplicates { get; set; }
        public int? NoListingType { get; set; }
        public int? Auctions { get; set; }
        public int? AuctionsWithBIN { get; set; }
        public int? Classified { get; set; }
        public int? FixedPrice { get; set; }
        public int? StoreInventory { get; set; }
        public DateTime? EbayTimestamp { get; set; }        
        public string EbayVersion { get; set; }
        public bool ? Deleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string DeletedBy { get; set; }

        public virtual ICollection<StagingEbayItem> StagingEbayItems { get; set; }
        public virtual AspNetUser AspNetUserCreatedBy { get; set; }
        public virtual AspNetUser AspNetUserDeletedBy { get; set; }

    }
}
