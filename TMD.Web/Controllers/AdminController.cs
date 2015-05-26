using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TMD.Implementation.Services;
using TMD.Interfaces.IServices;
using TMD.Models.RequestModels;
using TMD.Models.ResponseModels;
using TMD.Web.ModelMappers;
using TMD.Web.Models;
using TMD.Web.ViewModels;
using TMD.Web.ViewModels.Common;

namespace TMD.Web.Controllers
{
    public class AdminController : Controller
    {

        private readonly ISTGEbayBatchImportsService STGEbayBatchImportsService;

        public AdminController(ISTGEbayBatchImportsService iSTGEbayBatchImportsService)
        {
            STGEbayBatchImportsService = iSTGEbayBatchImportsService;
        }

        // GET: Admin
        public ActionResult Home()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BatchImportLV()
        {
            BatchImportSearchRequest viewModel = Session["PageMetaData"] as BatchImportSearchRequest;
            Session["PageMetaData"] = null;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(new BatchImportViewModel
            {
                SearchRequest = viewModel ?? new BatchImportSearchRequest()
            });
         }
        [HttpPost]
        public ActionResult BatchImportLV(BatchImportSearchRequest oRequest)
        {
            BatchImportSearchResponse oResponse =  STGEbayBatchImportsService.GetImports(oRequest);
            List<StagingEbayBatchImportModel> oList = oResponse.EbayBatchImports.Select(x => x.CreateFrom()).ToList();
            BatchImportViewModel oVModel = new BatchImportViewModel();
            oVModel.data = oList;

            oVModel.recordsTotal = oResponse.TotalCount;
            oVModel.recordsFiltered = oResponse.FilteredCount;


            Session["PageMetaData"] = oRequest;
           var toReturn = Json(oVModel, JsonRequestBehavior.AllowGet);
            return toReturn;
        }



    }
}
