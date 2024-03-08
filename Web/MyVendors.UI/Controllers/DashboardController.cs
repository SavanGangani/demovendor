using MyVendors.BAL.AuthHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class DashboardController : Controller
    {
        //[Auth]
        // GET: Home
        //Start:22/2/2022, Added By : Dhrupti Thesiya , Description : for view
        DashBoardHelper dbh = new DashBoardHelper();
        // GET: Dashboard
        public ActionResult Dashboard()
        {
            if(Session["user_id"]!=null)
            {
                ViewBag.id = Convert.ToInt32(Session["user_id"].ToString());
                ViewBag.data2 = dbh.getdeshboard();
                ViewBag.data = dbh.gettotalorder();
                ViewBag.data1 = dbh.gettotalService();
            }
            else
            {
                return RedirectToAction("/Login/Login");
            }
            return View();
        }

        public ActionResult VendorDetails()
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


        public ActionResult Category()
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

        public ActionResult SubCategory()
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

        public ActionResult OrderTransaction()
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

        public ActionResult ServiceTransaction()
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