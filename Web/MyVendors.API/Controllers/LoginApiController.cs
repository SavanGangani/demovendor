using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.API.Controllers
{
    public class LoginApiController : Controller
    {
        LoginAuthHelper LoginHelper = new LoginAuthHelper();


        //login post api
        [HttpPost]
        public JsonResult LogIn(Login lgn)
        {
            string Message = string.Empty;
            int Code = LoginHelper.Login(lgn);
            if (Code == -1)
            {
                Message = "Email Not Exits !!";
                return Json(new { Success = false, Message }, JsonRequestBehavior.AllowGet);
            }
            else if (Code == 0)
            {
                Message = "Password Not Match !!";
                return Json(new { Success = false, Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Success = true, Message }, JsonRequestBehavior.AllowGet);
        }
    }
}