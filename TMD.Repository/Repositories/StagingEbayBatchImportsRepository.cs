using System.Collections.Generic;
using System.Data.Entity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace TMD.Repository.Repositories
{
    public sealed class StagingEbayBatchImportsRepository : BaseRepository<StagingEbayBatchImport>, IStagingEbayBatchImportsRepository
    {
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
    }
}
