﻿using System;
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
                CompletedOn = source.CompletedOn!=null ? source.CompletedOn.Value.ToShortDateString() : "",
                CreatedOn = source.CreatedOn != null ? source.CreatedOn.Value.ToShortDateString() : "",
                EbayBatchImportId = source.EbayBatchImportId,
                EbayTimestamp = source.EbayTimestamp != null ? source.EbayTimestamp.Value.ToShortDateString() : "",
                EbayVersion = source.EbayVersion,
                Failed = source.Failed,
                FixedPrice = source.FixedPrice,
                Imported = source.Imported,
                InProcess = source.InProcess,
                StartedOn = source.StartedOn != null ? source.StartedOn.Value.ToShortDateString() : "",
            };

            return oModel;
        }

    }
}