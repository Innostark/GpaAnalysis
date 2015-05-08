using System.Web.Mvc;
using GPAA.Web.Controllers;

namespace IdentitySample.Controllers
{
    public class HomeController : BaseController
    {
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
