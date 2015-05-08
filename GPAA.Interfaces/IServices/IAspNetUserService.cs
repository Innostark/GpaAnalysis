using System.Collections.Generic;
using GPAA.Models.DomainModels;

namespace GPAA.Interfaces.IServices
{
    public interface IAspNetUserService
    {
        AspNetUser FindById(string id);
        IEnumerable<AspNetUser> GetAllUsers();
        bool UpdateUser(AspNetUser user);
    }
}
