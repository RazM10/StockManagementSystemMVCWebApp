using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystemMVCWebApp.Manager;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Controllers
{
    public class StockOutController : Controller
    {
        public CompanyManager  CompanyManager { get; set; }
        public ItemManager ItemManager { get; set; }
        public StockOutManager StockOutManager { get; set; }

        public StockOutController()
        {
            CompanyManager = new CompanyManager();
            ItemManager=new ItemManager();
            StockOutManager=new StockOutManager();
        }
        //
        // GET: /StockOut/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }

        [HttpPost]
        public ActionResult Save(List<Item> items)
        {
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }

        public ActionResult SaveNew()
        {
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }

        public JsonResult SaveStockOut(int iId, int quantity, string oAction)
        {
            StockOut stockOut=new StockOut();
            stockOut.ItemId = iId;
            stockOut.Quantity = quantity;
            stockOut.OutAction = oAction;
            stockOut.ItemName = ItemManager.GetItemName(iId);
            stockOut.CompanyName = CompanyManager.GetCompanyName(iId);
            stockOut.Msg = StockOutManager.Save(stockOut);
            return Json(stockOut);
        }
        public ActionResult OutReport()
        {
            return View();
        }

        public JsonResult GetReportByDates(string fodate, string todate)
        {
            List<StockOut> stockoutList = StockOutManager.GetReportDates(fodate, todate);
            foreach (StockOut i in stockoutList)
            {
                i.ItemName = ItemManager.GetItemName(i.ItemId);
            }
            return Json(stockoutList);
        }
        public JsonResult GetItemsByCompanyId(int comId)
        {
            List<Item> itemList = ItemManager.GetItemsByCompanyId(comId);
            return Json(itemList);
        }
        public JsonResult GetInfoByItemId(int id)
        {
            Item item = ItemManager.GetInfoByItemId(id);
            return Json(item);
        }
	}
}