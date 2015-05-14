using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.Repository
{
    public interface IUsersRepository : IBaseRepository<AspNetUser,int>
    {
        IEnumerable<AspNetUser> GetAllUsers();
    }
}
