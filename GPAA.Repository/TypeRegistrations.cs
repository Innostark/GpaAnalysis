using System.Data.Entity;
using GPAA.Interfaces.Repository;
using GPAA.Repository.BaseRepository;
using GPAA.Repository.Repositories;
using Microsoft.Practices.Unity;

namespace GPAA.Repository
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IAspNetUserRepository, AspNetUserRepository>();
            unityContainer.RegisterType<DbContext, BaseDbContext>(new PerRequestLifetimeManager());
        }
    }
}
