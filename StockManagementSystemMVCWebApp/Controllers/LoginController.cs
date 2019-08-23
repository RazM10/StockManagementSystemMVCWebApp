using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockManagementSystemMVCWebApp.Models;

namespace StockManagementSystemMVCWebApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(Login login)
        {
            if (login.Email == "asv@gmail.com" && login.Password == "12345")
            {
                return RedirectToAction("Save", "Category");
            }
            return View();
        }
	}
}