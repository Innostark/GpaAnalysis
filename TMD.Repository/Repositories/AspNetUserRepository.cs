using System.Collections.Generic;
using System.Data.Entity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace TMD.Repository.Repositories
{
    public class AspNetUserRepository : BaseRepository<AspNetUser>, IAspNetUserRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public AspNetUserRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<AspNetUser> DbSet
        {
            get { return db.AspNetUsers; }
        }

        #endregion

        public new IEnumerable<AspNetRole> Roles()
        {
            throw new System.NotImplementedException();
        }
    }
}
