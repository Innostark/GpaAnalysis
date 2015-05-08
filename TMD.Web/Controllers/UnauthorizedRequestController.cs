using System.Web.Mvc;

namespace TMD.Web.Controllers
{
    public class UnauthorizedRequestController : Controller
    {
        //
        // GET: /UnauthorizedRequest/
        public ActionResult Index()
        {
            return View();
        }
	}
}