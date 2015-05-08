using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Employee;
using Microsoft.AspNet.Identity;
using AreasModel=EPMS.Web.Areas.HR.Models;

namespace EPMS.Web.Controllers
{
    /// <summary>
    /// Controller for Employee
    /// </summary>
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService oEmployeeService;
        private readonly IJobTitleService oJobTitleService;
        private readonly IDepartmentService oDepartmentService;


        #region Constructor

        public EmployeeController(IEmployeeService oEmployeeService, IDepartmentService oDepartmentService, IJobTitleService oJobTitleService)
        {
            this.oEmployeeService = oEmployeeService;
            this.oDepartmentService = oDepartmentService;
            this.oJobTitleService = oJobTitleService;
        }

        #endregionTT
        
        /// <summary>
        /// Employee ListView Action Method
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeLV()
        {
            EmployeeSearchRequset employeeSearchRequest = Session["PageMetaData"] as EmployeeSearchRequset;

            Session["PageMetaData"] = null;

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;
            
            return View(new EmployeeViewModel
            {
                DepartmentList = oDepartmentService.LoadAll(),
                JobTitleList = oJobTitleService.GetJobTitlesByDepartmentId(0),
                SearchRequest = employeeSearchRequest ?? new EmployeeSearchRequset()
            });
        }
        /// <summary>
        /// Get All Employees and return to View
        /// </summary>
        /// <param name="employeeSearchRequest">Employee Search Requset</param>
        /// <returns>IEnumerable<Employee> of All Employees</returns>
        [HttpPost]
        public ActionResult EmployeeLV(EmployeeSearchRequset employeeSearchRequest)
        {
            employeeSearchRequest.UserId = Guid.Parse(User.Identity.GetUserId());//Guid.Parse(Session["LoginID"] as string);
            var employees = oEmployeeService.GetAllEmployees(employeeSearchRequest);
            IEnumerable<AreasModel.Employee> employeeList = employees.Employeess.Select(x => x.CreateFromWithImage(User.Identity.Name)).ToList();
            EmployeeViewModel employeeViewModel = new EmployeeViewModel
            {
                FilePath = (ConfigurationManager.AppSettings["EmployeeImage"] + User.Identity.Name + "/"),
                //data = employeeList,
                recordsTotal = employees.TotalCount,
                recordsFiltered = employees.TotalCount
            };

            // Keep Search Request in Session
            Session["PageMetaData"] = employeeSearchRequest;
            
            return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get all job titles from DB
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <returns>List<JobTitle> in JsonResult fromat</returns>
        [HttpGet]
        public JsonResult GetJobTitles(long deptId)
        {
            var jobTitles = oJobTitleService.GetJobTitlesByDepartmentId(deptId).Select(j => j.CreateFromDropDown());
            return Json(jobTitles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add or Update Employee Action Method
        /// </summary>
        /// <param name="id">Employee ID</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            EmployeeViewModel viewModel = new EmployeeViewModel();
            JobTitleSearchRequest jobTitleSearchRequest = new JobTitleSearchRequest();
            viewModel.JobTitleList = oJobTitleService.GetAllJobTitle(jobTitleSearchRequest).JobTitles;
            viewModel.JobTitleDeptList = viewModel.JobTitleList.Select(x => x.CreateFromJob());
            if (id != null)
            {
                viewModel.Employee = oEmployeeService.FindEmployeeById(id).CreateFrom();
            }
            return View(viewModel);
        }

        /// <summary>
        /// Add or Update Employee record
        /// </summary>
        /// <param name="viewModel">Employee View Model</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult AddEdit(EmployeeViewModel viewModel)
        {
            var filePath = Server.MapPath(ConfigurationManager.AppSettings["EmployeeImage"] + User.Identity.Name + "/");
            if (ModelState.IsValid)
            {
                try
                {
                    //Create Folder for the current user/sponsor
                    if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
                    //Save image to Folder if Posting to Fb
                    if (viewModel.Employee.UploadImage != null)
                    {
                        var fileOldName = (viewModel.Employee.UploadImage.FileName);
                        //Rename Image file with time stamp
                        var filename = (DateTime.Now.ToString().Replace(".", "") + fileOldName).Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");

                        viewModel.Employee.EmployeeImagePath = filename;
                        var savedFileName = Path.Combine(filePath, filename);
                        viewModel.Employee.UploadImage.SaveAs(savedFileName);
                    }
                    #region Update

                    if (viewModel.Employee.EmployeeId > 0)
                    {
                        viewModel.Employee.RecLastUpdatedDt = DateTime.Now;
                        viewModel.Employee.RecLastUpdatedBy = User.Identity.GetUserId();
                        var employeeToUpdate = viewModel.Employee.CreateFrom();
                        if (oEmployeeService.UpdateEmployee(employeeToUpdate))
                        {
                            TempData["message"] = new MessageViewModel { Message = "Employee has been Updated", IsUpdated = true };
                            return RedirectToAction("EmployeeLV");
                        }
                    }
                    #endregion

                    #region Add

                    else
                    {
                        viewModel.Employee.RecCreatedDt = DateTime.Now;
                        viewModel.Employee.RecCreatedBy = User.Identity.GetUserId();
                        var employeeToSave = viewModel.Employee.CreateFrom();

                        if (oEmployeeService.AddEmployee(employeeToSave))
                        {
                            TempData["message"] = new MessageViewModel { Message = "Employee has been Added", IsSaved = true };
                            viewModel.Employee.EmployeeId = employeeToSave.EmployeeId;
                            return RedirectToAction("EmployeeLV");
                        }
                    }

                    #endregion

                }
                catch (Exception e)
                {
                    return View(viewModel);
                }
            }
            
            return View(viewModel);
        }

        /// <summary>
        /// Delete Employee Data from DB
        /// </summary>
        /// <param name="employeeId">Employee ID</param>
        /// <returns>Json for toast message</returns>
        public ActionResult Delete(int employeeId)
        {
            var employeeToBeDeleted = oEmployeeService.FindEmployeeById(employeeId);
            try
            {
                oEmployeeService.DeleteEmployee(employeeToBeDeleted);
                return Json(new
                {
                    response = "Employee has been deleted",
                    status = (int)HttpStatusCode.OK
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to delete employee. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult DDL()
        //{
        //    return View();
        //}
    }
}