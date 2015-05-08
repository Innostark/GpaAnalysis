using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using GPAA.Models.DomainModels;
using GPAA.Repository.BaseRepository;
using Microsoft.AspNet.Identity;

namespace GPAA.Repository.Repositories
{
    public class UserRoleStore : IQueryableRoleStore<AspNetRole, string>
    {
        private readonly BaseDbContext _db;

        public UserRoleStore(BaseDbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("db");
            }

            _db = db;
        }

        // IQueryableRoleStore<UserRole, TKey>

        public IQueryable<AspNetRole> Roles
        {
            get { return _db.UserRoles; }
        }

        // IRoleStore<UserRole, TKey>

        public virtual Task CreateAsync(AspNetRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.UserRoles.Add(role);
            return _db.SaveChangesAsync();
        }

        public Task DeleteAsync(AspNetRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.UserRoles.Remove(role);
            return _db.SaveChangesAsync();
        }

        public Task<AspNetRole> FindByIdAsync(string roleId)
        {
            return _db.UserRoles.FindAsync(new object[] { roleId });
        }

        public Task<AspNetRole> FindByNameAsync(string roleName)
        {
            return _db.UserRoles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public Task UpdateAsync(AspNetRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            _db.Entry(role).State = EntityState.Modified;
            return _db.SaveChangesAsync();
        }

        // IDisposable

        public void Dispose()
        {
        }
    }
}