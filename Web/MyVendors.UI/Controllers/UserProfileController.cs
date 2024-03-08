using MyVendors.BAL;
using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class UserProfileController : Controller
    {
        UserProfileAuthHelper uah = new UserProfileAuthHelper();

        // GET: UserProfile
        public ActionResult UserProfile(int id = 0)
        {
            UserProfile u = null;
            if (id > 0)
            {
                u = uah.GetOne(id);
            }
            return View();
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfile up, int id)
        {
            uah.Update(up, id);
            return View();
        }
    }
}