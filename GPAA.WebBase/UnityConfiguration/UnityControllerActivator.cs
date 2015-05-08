using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace GPAA.WebBase.UnityConfiguration
{
    public class UnityControllerActivator : IControllerActivator
    {
        #region Public

        /// <summary>
        /// Creates a controller.
        /// </summary>
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }

        #endregion
    }
}
