using TMD.Models.DomainModels;
using TMD.Web.Models;

namespace TMD.Web.ModelMappers
{
    public static class UserMapper
    {
        public static void UpdateUserTo(this AspNetUser target, AspNetUserModel source)
        {
            target.Address = source.Address;
            target.Telephone = source.Telephone;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;
            

        }
    }
}