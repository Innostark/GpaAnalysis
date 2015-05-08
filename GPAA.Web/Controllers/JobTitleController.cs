using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobTitle;
using AreasModel=EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.Controllers
{
    /// <summary>
    /// Conttroller for Job TItles
    /// </summary>
    [Authorize]
    public class JobTitleController : BaseController
    {

        private readonly IJobTitleService JobTitleService;
        private readonly IDepartmentService DepartmentService;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="jobTitleService"></param>
        #region Constructor

        public JobTitleController(IDepartmentService departmentService, IJobTitleService jobTitleService)
        {
            DepartmentService = departmentService;
            JobTitleService = jobTitleService;
        }

        #endregion



        // GET: JobTitles ListView Action Method
        public ActionResult JobTitleLV()
        {
            JobTitleSearchRequest jobTitleSearchRequest = Session["PageMetaData"] as JobTitleSearchRequest;

            Session["PageMetaData"] = null;

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            IEnumerable<AreasModel.JobTitle> jobList = JobTitleService.LoadAll().Select(x => x.CreateFrom());

            return View(new JobTitleViewModel
            {
                DepartmentList = DepartmentService.LoadAll().Select(x=>x.CreateFrom()),
                JobTitleList = jobList,
                SearchRequest = jobTitleSearchRequest ?? new JobTitleSearchRequest()
            });
        }

        public ActionResult ComboBox()
        {
            return View();
        }

        /// <summary>
        /// Add or Update Job Title Action Method
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            JobTitleViewModel viewModel = new JobTitleViewModel();
            //if (id != null)
            //{
            //    viewModel.Employee = oEmployeeService.FindEmployeeById(id).CreateFrom();
            //}
            return View(viewModel);
        }
        public int AddData(JobTitleViewModel viewModel)
        {
            viewModel.JobTitle.RecCreatedDt = DateTime.Now;
            viewModel.JobTitle.RecCreatedBy = User.Identity.Name;
            var jobToSave = viewModel.JobTitle.CreateFrom();
            if (JobTitleService.AddJob(jobToSave))
            {
                TempData["message"] = new MessageViewModel { Message = "Employee has been Added", IsSaved = true };
                return 1;
            }
            return 0;
        }
    }
}