using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult RegView()
        {
            return View();
        }
    }
}