using System.Collections.Generic;
using System.Data.Entity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace TMD.Repository.Repositories
{
    public sealed class STGEbayBatchImportsRepository : BaseRepository<STGEbayBatchImport>, ISTGEbayBatchImportsRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public STGEbayBatchImportsRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<STGEbayBatchImport> DbSet
        {
            get { return db.STGEbayBatchImports; }
        }
        #endregion

        public bool IsEbayLoadRunning()
        {
            return this.db.IsEbayLoadRunning();
        }

        public IEnumerable<STGEbayBatchImport> GetAllImports()
        {
            return DbSet;
        }
    }
}
