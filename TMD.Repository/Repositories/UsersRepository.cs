using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;
using TMD.Repository.BaseRepository;

namespace TMD.Repository.Repositories
{
    public sealed class UsersRepository : BaseRepository<AspNetUser>, IUsersRepository
    {
        #region Constructor
        public UsersRepository(IUnityContainer container)
            : base(container)
        {

        }
        protected override System.Data.Entity.IDbSet<AspNetUser> DbSet
        {
            get { return db.AspNetUsers; }
        }
        #endregion
        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return DbSet.Where(x => x.AspNetRoles.Any(y => y.Name != "Admin"));//TEmperory check
        }
    }
}
