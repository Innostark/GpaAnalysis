using System.Web.Mvc;
using TMD.Web.Controllers;

namespace IdentitySample.Controllers

{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OneColumn()
        {
            return View();
        }
        public ActionResult TwoColumnOne()
        {
            return View();
        }
        public ActionResult TwoColumnTwo()
        {
            return View();
        }
        public ActionResult ThreeColumn()
        {
            return View();
        }
    }
}
