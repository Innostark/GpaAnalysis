using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace GPAA.WebBase.UnityConfiguration
{
    /// <summary>
    /// Unity dependency resolver
    /// </summary>
    public class UnityDependencyResolver : UnityDependencyScope, IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container"></param>
        public UnityDependencyResolver(IUnityContainer container)
            : base(container)
        {
        }

        #endregion

        #region Public

        /// <summary>
        /// Begin a scope
        /// </summary>
        /// <returns></returns>
        public IDependencyScope BeginScope()
        {
            IUnityContainer child = container.CreateChildContainer();
            return new UnityDependencyScope(child);
        }

        #endregion
    }
}