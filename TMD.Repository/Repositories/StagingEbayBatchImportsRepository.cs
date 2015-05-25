using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TMD.Interfaces.Repository;
using TMD.Models.Common;
using TMD.Models.DomainModels;
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
                    {BatchImportSearchRequestByColumn.InProcess, c => c.InProcess},
                    {BatchImportSearchRequestByColumn.CompletedOn, c => c.CompletedOn},
                    {BatchImportSearchRequestByColumn.EbayBatchImportId, c => c.EbayBatchImportId}
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


        public IEnumerable<StagingEbayBatchImport> GetImports(Models.RequestModels.BatchImportSearchRequest searchRequest)
        {
            int fromRow = (searchRequest.PageNo - 1) * searchRequest.PageSize;
            int toRow = searchRequest.PageSize;
            Expression<Func<StagingEbayBatchImport, bool>> query =
                    s => (
                            (searchRequest.InProcess == 1 || searchRequest.InProcess == 2)
                            && (s.InProcess.Equals(searchRequest.InProcess == 1 ? true : false))
                            
                        );
            IEnumerable<StagingEbayBatchImport> oList =
                searchRequest.IsAsc
                    ? DbSet.Where(query)
                        .OrderBy(batchClause[searchRequest.BatchImportOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList()
                    : DbSet.Where(query)
                        .OrderByDescending(batchClause[searchRequest.BatchImportOrderBy])
                        .Skip(fromRow)
                        .Take(toRow)
                        .ToList();


            return oList;
        }
    }
}
