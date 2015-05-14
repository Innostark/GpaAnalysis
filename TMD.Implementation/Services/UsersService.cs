using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Interfaces.IServices;
using TMD.Interfaces.Repository;

namespace TMD.Implementation.Services
{
     public   class UsersService : IUsersService
    {

          #region 'Private and Constructor'
        private readonly IUsersRepository usersRepository;


        public UsersService(IUsersRepository iUsersRepository)
        {
            this.usersRepository = iUsersRepository;
            
        }

       #endregion 'Private and Constructor'


        public IEnumerable<Models.DomainModels.AspNetUser> GetAllUsers()
        {
            return usersRepository.GetAllUsers();
        }

        public void Dispose()
        {
            
        }
    }
}
