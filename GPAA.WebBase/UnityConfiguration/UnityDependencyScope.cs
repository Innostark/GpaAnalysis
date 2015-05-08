using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace GPAA.WebBase.UnityConfiguration
{
    /// <summary>
    /// Unity dependency scope
    /// </summary>
    public class UnityDependencyScope : IDependencyScope
    {
        #region Protected

        protected IUnityContainer container;

        #endregion
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public UnityDependencyScope(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        /// <summary>
        /// Resolve a type
        /// </summary>
        public object GetService(Type serviceType)
        {
            if (!UnityRegistrationCache.IsRegistered(container, serviceType))
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                {
                    return null;
                }
            }
            return container.Resolve(serviceType);
        }

        /// <summary>
        /// Resolve multiple types
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (UnityRegistrationCache.IsRegistered(container, serviceType))
            {
                return container.ResolveAll(serviceType);
            }
            return new List<object>();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
        }

        #endregion
    }
}