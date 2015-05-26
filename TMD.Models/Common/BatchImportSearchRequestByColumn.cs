using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMD.Models.Common
{
    public enum BatchImportSearchRequestByColumn
    {
        EbayBatchImportId =0,
        InProcess = 1,
        CreatedOn = 2,
        StartedOn = 3,
        CompletedOn = 4,
        Imported = 5,
        Failed = 6,
        Auctions = 7,
        FixedPrice = 8,
        EbayTimestamp = 9,
        EbayVersion = 10
    }
}
