using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;

namespace EPMS.Web.Controllers
{
    /// <summary>
    /// Controller for Department
    /// </summary>
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService DepartmentService;

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="departmentService"></param>
        #region Constructor

        public DepartmentController(IDepartmentService departmentService)
        {
            DepartmentService = departmentService;
        }

        #endregion


        // GET: Departments ListView Action Method
        public ActionResult DepartmentLV()
        {
            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            return View(new DepartmentViewModel
            {
                DepartmentList = DepartmentService.LoadAll().Select(x=>x.CreateFrom())
            });
        }
    }
}