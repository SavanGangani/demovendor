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
    public class APIOrderViewDetailsController : ApiController
    {
        TransactionHelper th = new TransactionHelper();

        //[Route("api/ItemDataTransactionApi/GetView")]
        public List<Transaction> Get(int id)
        {
            return th.GetViewDetails(id);
        }

        public bool Put([FromBody]Transaction value)
        {

            return th.UpdateTransOrder(value);
        }
    }
}
