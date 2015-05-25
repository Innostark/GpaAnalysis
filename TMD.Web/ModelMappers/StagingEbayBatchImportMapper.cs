using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMD.Models.DomainModels;
using TMD.Web.Models;

namespace TMD.Web.ModelMappers
{
    public static class StagingEbayBatchImportMapper
    {
        public static StagingEbayBatchImportModel CreateFrom(this StagingEbayBatchImport source)
        {
            var oModel = new StagingEbayBatchImportModel
            {
                Auctions = source.Auctions,
                CompletedOn = source.CompletedOn,
                CreatedOn = source.CreatedOn,
                EbayBatchImportId = source.EbayBatchImportId,
                EbayTimestamp = source.EbayTimestamp,
                EbayVersion = source.EbayVersion,
                Failed = source.Failed,
                FixedPrice = source.FixedPrice,
                Imported = source.Imported,
                InProcess = source.InProcess,
                StartedOn = source.StartedOn
            };

            return oModel;
        }

    }
}