using TMD.Implementation.Identity;
using TMD.Implementation.Services;
using TMD.Interfaces.IServices;
using TMD.Models.IdentityModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace TMD.Implementation
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<ILogger, LoggerService>();
            unityContainer.RegisterType<IAspNetUserService, AspNetUserService>();
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            unityContainer.RegisterType<IStagingEbayLoadService, EbayStagingLoadService>();
            unityContainer.RegisterType<IStagingEbayBatchImportsService, StagingEbayBatchImportsService>();
        }
    }
}
