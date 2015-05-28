using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TMD.Interfaces.Repository;
using TMD.Models.Common;
using TMD.Models.DomainModels;
using TMD.Models.ResponseModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;
using System.Linq.Expressions;

namespace TMD.Repository.Repositories
{
    public sealed class StagingEbayBatchImportsRepository : BaseRepository<StagingEbayBatchImport>, IStagingEbayBatchImportsRepository
    {
        private readonly Dictionary<BatchImportSearchRequestByColumn, Func<StagingEbayBatchImport, object>> batchClause =
             new Dictionary<BatchImportSearchRequestByColumn, Func<StagingEbayBatchImport, object>>
                {
                    {BatchImportSearchRequestByColumn.EbayBatchImportId, c => c.EbayBatchImportId},
                    {BatchImportSearchRequestByColumn.InProcess, c => c.InProcess},
                    {BatchImportSearchRequestByColumn.CreatedOn, c => c.CreatedOn},
                    {BatchImportSearchRequestByColumn.StartedOn, c => c.StartedOn},
                    {BatchImportSearchRequestByColumn.CompletedOn, c => c.CompletedOn},
                    {BatchImportSearchRequestByColumn.Imported, c => c.Imported},
                    {BatchImportSearchRequestByColumn.Failed, c => c.Failed},
                    {BatchImportSearchRequestByColumn.Auctions, c => c.Auctions},
                    {BatchImportSearchRequestByColumn.FixedPrice, c => c.FixedPrice},
                    {BatchImportSearchRequestByColumn.EbayTimestamp, c => c.EbayTimestamp},
                    {BatchImportSearchRequestByColumn.EbayVersion, c => c.EbayVersion}
                };
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StagingEbayBatchImportsRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StagingEbayBatchImport> DbSet
        {
            get { return db.StagingEbayBatchImports; }
        }
        #endregion

        public bool IsEbayLoadRunning()
        {
            return this.db.IsEbayLoadRunning();
        }

        public IEnumerable<StagingEbayBatchImport> GetAllImports()
        {
            return DbSet;
        }


        public BatchImportSearchResponse GetImports(Models.RequestModels.BatchImportSearchRequest searchRequest)
        {
            bool flag = false;
            if (searchRequest.InProcess == 1)
                flag = true;
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<StagingEbayBatchImport, bool>> query =
                    s => (
                            (searchRequest.InProcess == 0 || s.InProcess.Equals(flag))
                            
                            
                        );
            IEnumerable<StagingEbayBatchImport> oList =
                searchRequest.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(batchClause[searchRequest.BatchImportOrderBy] )
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(batchClause[searchRequest.BatchImportOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();

            return new BatchImportSearchResponse {EbayBatchImports = oList, TotalCount = DbSet.Count(),FilteredCount = DbSet.Count(query)};
            
        }


      
    }
}
