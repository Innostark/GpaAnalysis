using System.Collections.Generic;
using TMD.Models.DomainModels;

namespace TMD.Interfaces.IServices
{
    public interface IAspNetUserService
    {
        AspNetUser FindById(string id);
        IEnumerable<AspNetUser> GetAllUsers();
        bool UpdateUser(AspNetUser user);
    }
}
