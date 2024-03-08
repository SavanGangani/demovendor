using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class ServiceGetApiForTableController : ApiController
    {
        TransactionHelper th = new TransactionHelper();

        public List<ModelService> Get(int id)
        {
            return th.GetTransactionServiceData(id);
        }
    }
}
