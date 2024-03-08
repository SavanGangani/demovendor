using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class LoggerController : Controller
    {
        ModelLogger logger = new ModelLogger();

        //Post
        public bool Post(Login data)
        {
            int uid = Convert.ToInt32(HttpContext.Session["user_id"]);
            logger.WrieLog(data.Email, data.Password, uid);
            return true;
        }
    }
}