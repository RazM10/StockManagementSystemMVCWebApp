using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystemMVCWebApp.Manager;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Controllers
{
    public class CategoryController : Controller
    {
        public CategoryManager CategoryManager { get; set; }

        public CategoryController()
        {
            CategoryManager = new CategoryManager();
        }

        //
        // GET: /Category/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Save()
        {
            ViewBag.categories = CategoryManager.GetAllCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Category category)
        {
            if (ModelState.IsValid)
            {
                ViewBag.message = CategoryManager.Save(category);
            }
            else
            {
                ViewBag.message = "Model is not Valid";
            }

            ViewBag.categories = CategoryManager.GetAllCategories();
            return View();

        }

        public ActionResult Update(int id, string name)
        {
            Category category=new Category();
            category.Id = id;
            category.Name = name;
            return View(category);
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            string msg = CategoryManager.UpdateName(category);
            if (msg!=null)
            {
                return RedirectToAction("Save", "Category");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details()
        {
            ViewBag.categories = CategoryManager.GetAllCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Details(Category c)
        {
            ViewBag.categories = CategoryManager.GetAllCategories();
            if (c.Id>-1)
            {
                int id = c.Id;
                string name = c.Name;
                return RedirectToAction("Updateategory", "Category",new{id,name});
            }
            return View();
        }

        public ActionResult Updateategory(int id,string name)
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}