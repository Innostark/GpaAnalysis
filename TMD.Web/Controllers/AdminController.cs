using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.util;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TMD.Implementation.Services;
using TMD.Interfaces.IServices;
using TMD.Models.RequestModels;
using TMD.Models.ResponseModels;
//using TMD.Web.LocalEbayIntegrationWS;
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

        [AllowAnonymous]
        public ActionResult Home()
        {
            return View();
        }

        public AdminController(IStagingEbayBatchImportsService iSTGEbayBatchImportsService, IStagingEbayLoadService iStagingEbayLoadService)
        {
            STGEbayBatchImportsService = iSTGEbayBatchImportsService;
            StagingEbayLoadService = iStagingEbayLoadService;
        }


        #region Batch Import
        public ActionResult BatchImportLV()
        {
            BatchImportSearchRequest viewModel = Session["PageMetaData"] as BatchImportSearchRequest;
              Session["PageMetaData"] = null;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            
            var oReturnModel = new BatchImportViewModel
            {

                 SearchRequest = viewModel==null ? new BatchImportSearchRequest() : viewModel
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

        //public ActionResult StarEbayBatchLoad()
        //{
        //    try
        //    {
        //        var wsClient = new TmdEbayIntegrationServiceClient();
        //        wsClient.StartEbayLoadAsync("usman@innostark.com", "123456");
        //    }
        //    catch (FaultException fex)
        //    {

        //    }
        //    catch (Exception ex)
        //    {
                
        //    }

        //    var toReturn = Json(oVModel, JsonRequestBehavior.AllowGet);
        //    return toReturn; ;
        //}
        #endregion

        #region Ebay Item Import
        public ActionResult EbayItemImportLV()
        {
            if (Request.UrlReferrer == null || Request.UrlReferrer.AbsolutePath == "/Admin/EbayItemImportLV")
            {
                Session["PageMetaData"] = null;
            }
            
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
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EbayItemImportDetail(StagingEbayItemModel item)
        {
            try
            {
                if (StagingEbayLoadService.UpdateEbayItemImportDetail(item.EbayItemtId, item.AFASerial, User.Identity.GetUserId()))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        IsUpdated = true,
                        Message = "Item has been updated."
                    };
                    return RedirectToAction("EbayItemImportLV");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return View(item);
        }
        #endregion

          [AllowAnonymous]
        public ActionResult PerformEbayService()
        {
              try
              {

              if (this.User.Identity.IsAuthenticated)
              {
                  var user = UserManager.FindByEmail(User.Identity.Name);

                  if (Utility.AdminRoleId == user.AspNetRoles.FirstOrDefault().Id)
                  {

                      TMD.Web.LocalEbayIntegrationWS.TmdEbayIntegrationServiceClient oClient =
                          new TMD.Web.LocalEbayIntegrationWS.TmdEbayIntegrationServiceClient();
                     // oClient.StartEbayLoadByToken(user.Id);
                      oClient.StartEbayLoad("usman@innostark.com","123456");
                      return new HttpStatusCodeResult(HttpStatusCode.OK);
                  }
                  throw new Exception("User Role is not Admin");
              }
              throw new Exception("User is not Authenticated");
              }
              catch (Exception e)
              {

                  return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed,e.Message.ToString());
              }

        }

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