using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Order()
        {
            if (Session["user_id"] != null)
            {
                ViewBag.id = Convert.ToInt32(Session["user_id"].ToString());
            }
            else
            {
                return RedirectToAction("/Login/Login");
            }
            return View();

        }

        public ActionResult Service()
        {
            if (Session["user_id"] != null)
            {
                ViewBag.id = Convert.ToInt32(Session["user_id"].ToString());
            }
            else
            {
                return RedirectToAction("/Login/Login");
            }
            return View();

        }
    }
}