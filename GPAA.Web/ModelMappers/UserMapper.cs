using GPAA.Models.DomainModels;

namespace GPAA.Web.ModelMappers
{
    public static class UserMapper
    {
        public static void UpdateUserTo(this AspNetUser target, AspNetUser source)
        {
            target.Email = source.Email;
            target.EmailConfirmed = source.EmailConfirmed;
        }
    }
}