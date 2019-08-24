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

        public ActionResult Save_Update()
        {
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }
        [HttpPost]
        public ActionResult Save_Update(StockOut stockOut)
        {
            if (Session["OutList"] == null)
            {
                List<StockOut> s = new List<StockOut>();

                //stockOut.ItemId = iId;
                //stockOut.Quantity = quantity;
                //stockOut.OutAction = oAction;
                stockOut.ItemName = ItemManager.GetItemName(stockOut.ItemId);
                stockOut.CompanyName = CompanyManager.GetCompanyName(stockOut.ItemId);


                s.Add(stockOut);
                Session["OutList"] = s;
            }
            else
            {
                stockOut.ItemName = ItemManager.GetItemName(stockOut.ItemId);
                stockOut.CompanyName = CompanyManager.GetCompanyName(stockOut.ItemId);
                var userList = (List<StockOut>)Session["OutList"];
                userList.Add(stockOut);
                Session["OutList"] = userList;

            }
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }

        [HttpPost]
        public ActionResult ListSave()
        {
            var userList = (List<StockOut>)Session["OutList"];
            Session["OutList"] = null;
            foreach (var stockOut in userList)
            {
                stockOut.Msg = StockOutManager.Save(stockOut);
            }

            return RedirectToAction("Save_Update");
        }

        public JsonResult GetInfoByItemId_Update(int id)
        {
            Item item = ItemManager.GetInfoByItemId(id);
            if (Session["OutList"] != null)
            {
                List<StockOut> userList = (List<StockOut>)Session["OutList"];
                foreach (StockOut stockOut in userList)
                {
                    if (item.Id==stockOut.ItemId)
                    {
                        item.AvailableQuantity = item.AvailableQuantity - stockOut.Quantity;
                    }
                }
            }
            return Json(item);
        }
	}
}