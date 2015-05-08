using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TMD.WebBase.Mvc
{
    /// <summary>
    /// Site Authorize Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class SiteAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Check if user is authorized on a given permissionKey
        /// </summary>
        private bool IsAuthorized()
        {
            object userPermissionSet = HttpContext.Current.Session["UserPermissionSet"];
            if (userPermissionSet != null)
            {
                string[] userPermissionsSet = (string[]) userPermissionSet;
                return (userPermissionsSet.Contains(PermissionKey));
            }
            return false;
        }
        /// <summary>
        /// Perform the authorization
        /// </summary>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }
            
            return IsAuthorized();
        }
        /// <summary>
        /// Redirects request to unauthroized request page
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                filterContext.Result =
                    new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new { area = "", controller = "UnauthorizedRequest", action = "Index" }));
            }
        }        
        public string PermissionKey { get; set; }        
    }
}