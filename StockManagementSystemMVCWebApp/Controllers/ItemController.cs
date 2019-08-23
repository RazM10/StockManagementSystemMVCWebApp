using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystemMVCWebApp.Manager;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Controllers
{
    public class ItemController : Controller
    {
        public CategoryManager CategoryManager { get; set; }
        public CompanyManager CompanyManager { get; set; }
        public ItemManager ItemManager { get; set; }

        public ItemController()
        {
            CategoryManager=new CategoryManager();
            CompanyManager=new CompanyManager();
            ItemManager=new ItemManager();
        }
        //
        // GET: /Item/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.categoryList = CategoryManager.GetAllCategories();
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            ViewBag.itemList = ItemManager.GetAllItems();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Item item)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = ItemManager.Save(item);
            }
            else
            {
                ViewBag.message = "Model state is not valid";
            }
            

            ViewBag.categoryList = CategoryManager.GetAllCategories();
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            ViewBag.itemList = ItemManager.GetAllItems();
            return View();
        }

        [HttpGet]
        public ActionResult SockIn()
        {
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }

        [HttpPost]
        public ActionResult SockIn(Item item)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = ItemManager.UpdateQuantity(item);
            }
            else
            {
                ViewBag.message = "Model State is not Valid";
            }
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }

        //It is by Page Load
        [HttpGet]
        public ActionResult Report()
        {
            ViewBag.categoryList = CategoryManager.GetAllCategories();
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            ViewBag.itemList = null;
            return View();
        }

        [HttpPost]
        public ActionResult Report(Item item)
        {
            if (ModelState.IsValid)
            {

                ViewBag.itemList = ItemManager.GetAllItemsForReport(item);
            }
            else
            {
                ViewBag.itemList = null;
                ViewBag.message = "Model state is not valid";
            }

            ViewBag.categoryList = CategoryManager.GetAllCategories();
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            //ViewBag.itemList = ItemManager.GetAllItemsForReport(item);
            return View();
        }

        //It is by Ajax
        public ActionResult ReportTwo()
        {
            ViewBag.categoryList = CategoryManager.GetAllCategories();
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
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

        public JsonResult GetAllItemsForReportTwo(int comId, int cotId)
        {
            Item item =new Item();
            item.CompanyId = comId;
            item.CategoryId = cotId;
            List<Item> itemList = ItemManager.GetAllItemsForReport(item);
            foreach (Item i in itemList)
            {
                i.CompanyName = CompanyManager.GetCompanyNameTwo(i.CompanyId);
            }
            return Json(itemList);
        }
	}
}