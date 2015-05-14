using System.Collections.Generic;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.Repository
{
    public interface IAspNetUserRepository : IBaseRepository<AspNetUser, string>
    {
        IEnumerable<AspNetUser> GetAllUsers();
    }

}
