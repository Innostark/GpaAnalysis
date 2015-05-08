using System.Collections.Generic;
using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;
using TMD.Models.DomainModels;

namespace TMD.Implementation.Services
{
    public class AspNetUserService : IAspNetUserService
    {
        private readonly IAspNetUserRepository repository;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public AspNetUserService(IAspNetUserRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion
        public AspNetUser FindById(string id)
        {
            return repository.Find(id);
        }

        public IEnumerable<AspNetUser> GetAllUsers()
        {
            return repository.GetAll();
        }
        public bool UpdateUser(AspNetUser user)
        {
            repository.Update(user);
            repository.SaveChanges();
            return true;
        }
    }
}
