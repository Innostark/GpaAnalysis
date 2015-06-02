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
    public class AdminController : BaseController
    {

        private readonly IStagingEbayBatchImportsService STGEbayBatchImportsService;
        private readonly IStagingEbayLoadService StagingEbayLoadService;

        public AdminController(IStagingEbayBatchImportsService iSTGEbayBatchImportsService, IStagingEbayLoadService iStagingEbayLoadService)
        {
            STGEbayBatchImportsService = iSTGEbayBatchImportsService;
            StagingEbayLoadService = iStagingEbayLoadService;
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


        #region Batch Import
        public ActionResult BatchImportLV()
        {
            //BatchImportSearchRequest viewModel = Session["PageMetaData"] as BatchImportSearchRequest;

            //Session["PageMetaData"] = null;
            //ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            var oReturnModel = new BatchImportViewModel
            {
                SearchRequest = new BatchImportSearchRequest()
            };
            oReturnModel.SearchRequest.IsAsc = false;
            return View(oReturnModel);
         }
        [HttpHeaderAttribute("Access-Control-Allow-Origin", "*")]
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
        #endregion

        #region Ebay Item Import
        public ActionResult EbayItemImportLV()
        {
            StagingEbayItemRequest viewModel = Session["PageMetaData"] as StagingEbayItemRequest;
            Session["PageMetaData"] = null;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(new EbayItemViewModel
            {
                SearchRequest = viewModel ?? new StagingEbayItemRequest()
            });
        }

         [HttpHeaderAttribute("Access-Control-Allow-Origin", "*")]
       
        [HttpPost]
        public ActionResult EbayItemImportLV(StagingEbayItemRequest oRequest)
        {
            EbayItemSearchResponse oResponse = StagingEbayLoadService.GetImports(oRequest);
            List<StagingEbayItemModel> oList = oResponse.EbayItemImports.Select(x => x.CreateFrom()).ToList();
            EbayItemViewModel oVModel = new EbayItemViewModel();
            oVModel.data = oList;
            oVModel.recordsTotal = oResponse.TotalCount;
            oVModel.recordsFiltered = oResponse.FilteredCount;
            


            Session["PageMetaData"] = oRequest;
            var toReturn = Json(oVModel, JsonRequestBehavior.AllowGet);
            return toReturn;
        }

        public ActionResult EbayItemImportDetail(string vpek)
        {
         var Item=   StagingEbayLoadService.GetEbayImportById(vpek).CreateFrom();
            return View(Item);
        }
        #endregion

    }
}
public class HttpHeaderAttribute : ActionFilterAttribute
{
    public string Name { get; set; }
    public string Value { get; set; }
    public HttpHeaderAttribute(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        filterContext.HttpContext.Response.AppendHeader(Name, Value);
        base.OnResultExecuted(filterContext);
    }
}