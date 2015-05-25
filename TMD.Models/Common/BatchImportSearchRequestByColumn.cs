using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMD.Models.Common
{
    public enum BatchImportSearchRequestByColumn
    {
        EbayBatchImportId =1,
        EbayTimestamp=2,

        InProcess = 3,
        StartedOn=4,
        CompletedOn =5,
        Imported=6,
        Failed=7




    }
}
