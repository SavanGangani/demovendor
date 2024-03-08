using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult LogIn()
        {
            if (Request.Cookies["user"] != null)
            {
                ViewBag.email = Request.Cookies["user"].Values["useremail"].ToString();
                ViewBag.password = Request.Cookies["user"].Values["userpassword"].ToString();
                ViewBag.rememeberme = true;
                return View();
            }
            else
            {
                ViewBag.rememeberme = false;
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult ForgotReset()
        {
            return View();

        }
    }
}