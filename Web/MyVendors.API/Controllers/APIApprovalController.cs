using MyVendors.BAL.AuthHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class APIApprovalController : ApiController
    {
        BALApproveStatus helper = new BALApproveStatus();

        [HttpPost]
        public int Post(int id)
        {
            helper.GetData(id);
            return 0;
        }

    }
}
