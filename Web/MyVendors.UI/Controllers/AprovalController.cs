using MyVendors.BAL.AuthHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyVendors.UI.Controllers
{
    public class AprovalController : Controller
    {
        // GET: Approval
        BALApproveStatus helper = new BALApproveStatus();


        public ActionResult Update(int id)
        {
            helper.ApproveStatus(id);
            return RedirectToAction("VendorDetails", "Dashboard");
        }

    }
}