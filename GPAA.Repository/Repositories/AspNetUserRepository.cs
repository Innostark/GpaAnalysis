using System.Collections.Generic;
using System.Data.Entity;
using GPAA.Interfaces.Repository;
using GPAA.Models.DomainModels;
using GPAA.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace GPAA.Repository.Repositories
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
