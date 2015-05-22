using System.Collections.Generic;
using System.Data.Entity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace TMD.Repository.Repositories
{
    public sealed class StagingEbayItemRepository : BaseRepository<StagingEbayItem>, IStagingEbayItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public StagingEbayItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<StagingEbayItem> DbSet
        {
            get { return db.StagingEbayItems; }
        }
        #endregion

        public IEnumerable<StagingEbayItem> GetAllEbayItems()
        {
            return DbSet;
        }

        public bool EbayItemExists(string itemId)
        {
            return this.db.EbayItemExists(itemId);
        }
    }
}
