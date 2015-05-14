using System.Collections.Generic;
using System.Data.Entity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace TMD.Repository.Repositories
{
    public sealed class STGEbayItemRepository : BaseRepository<STGEbayItem>, ISTGEbayItemRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public STGEbayItemRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<STGEbayItem> DbSet
        {
            get { return db.STGEbayItems; }
        }
        #endregion

        public IEnumerable<STGEbayItem> GetAllEbayItems()
        {
            return DbSet;
        }

        public bool EbayItemExists(string itemId)
        {
            return this.db.EbayItemExists(itemId);
        }
    }
}
