using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystemMVCWebApp.Manager;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Controllers
{
    public class CompanyController : Controller
    {
        public CompanyManager CompanyManager { get; set; }

        public CompanyController()
        {
            CompanyManager=new CompanyManager();
        }
        //
        // GET: /Company/
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
        public ActionResult Save(Company company)
        {
            ViewBag.message = CompanyManager.Save(company);
            ViewBag.companyList = CompanyManager.GetAllCompanys();
            return View();
        }
        public ActionResult Update(int id, string name)
        {
            Company company = new Company();
            company.Id = id;
            company.Name = name;
            return View(company);
        }

        [HttpPost]
        public ActionResult Update(Company company)
        {
            string msg = CompanyManager.UpdateName(company);
            if (msg != null)
            {
                return RedirectToAction("Save", "Company");
            }
            return View();
        }
	}
}