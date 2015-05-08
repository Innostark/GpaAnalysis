using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using GPAA.Implementation.Identity;
using GPAA.Models.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace GPAA.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        #region Private

        private ApplicationUserManager _userManager;
        

        
        #endregion

        #region Protected
        // GET: Base
        //protected override async void Initialize(RequestContext requestContext)
        //{
        //    base.Initialize(requestContext);
        //    if (Session["FullName"] == null || Session["FullName"].ToString() == string.Empty)
        //        SetUserDetail();
        //    SetCultureInfo();
        //}
        #endregion

        #region Public

        //when isForce =  true it sets the value, no matter session has or not
        public void SetUserDetail()
        {
            Session["FullName"] = Session["UserID"] = string.Empty;

            if (!User.Identity.IsAuthenticated) return;
            AspNetUser result =
                HttpContext.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(User.Identity.GetUserId());
            string role =
                HttpContext.GetOwinContext()
                    .Get<ApplicationRoleManager>()
                    .FindById(result.AspNetRoles.ToList()[0].Id)
                    .Name;
            Session["FullName"] = result.UserName;
            Session["UserID"] = result.Id;
            Session["RoleName"] = role;
            
            

            AspNetUser userResult = UserManager.FindById(User.Identity.GetUserId());
            List<AspNetRole> roles = userResult.AspNetRoles.ToList();
        }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        #endregion

        public void SetCultureInfo()
        {
            CultureInfo info;
            if (Session["Culture"] != null)
            {
                info = new CultureInfo(Session["Culture"].ToString());
            }
            else
            {
                info = new CultureInfo("en");
                Session["Culture"] = info.Name;
            }
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
            System.Threading.Thread.CurrentThread.CurrentUICulture = info;
        } 
    }
}