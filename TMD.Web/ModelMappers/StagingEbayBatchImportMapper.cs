using System;
using System.Collections.Generic;
using System.Configuration;
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
            var hostURL = ConfigurationManager.AppSettings["HostURL"];
            var oModel = new StagingEbayBatchImportModel
            {
                Auctions = source.Auctions,
                CompletedOn = source.CompletedOn!=null ? source.CompletedOn.Value.ToShortDateString() : "",
                CreatedOn = source.CreatedOn != null ? source.CreatedOn.Value.ToShortDateString() : "",
                EbayBatchImportId = @"<a title='Click to open items imported for this batch' href='" + hostURL + "Admin/EbayItemImportLV?vpek=" + source.EbayBatchImportId + "' target='_blank'> " + source.EbayBatchImportId + "</a>",
                EbayTimestamp = source.EbayTimestamp != null ? source.EbayTimestamp.Value.ToShortDateString() : "",
                EbayVersion = source.EbayVersion,
                Failed = source.Failed,
                FixedPrice = source.FixedPrice,
                Imported = source.Imported,
                InProcess = source.InProcess ?"Yes":"No",
                StartedOn = source.StartedOn != null ? source.StartedOn.Value.ToShortDateString() : "",
                Deleted = source.Deleted,
                DeletedBy = source.DeletedBy,
                DeletedOn = source.DeletedOn
            };

            return oModel;
        }

    }
}