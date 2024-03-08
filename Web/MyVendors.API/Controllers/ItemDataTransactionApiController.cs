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
    public class ItemDataTransactionApiController : ApiController
    {
        TransactionHelper th = new TransactionHelper();
        // GET: api/ItemDataTransactionApi
        public List<Transaction> Get(Transaction data)
        {
            return th.getOrderItemName(data);
        }

        // GET: api/ItemDataTransactionApi/5
        //[Route("api/ItemDataTransactionApi/GetView")]
        //public List<Transaction> Get(int id)
        //{
        //    return th.GetViewDetails(id);
        //}

        // POST: api/ItemDataTransactionApi
        public List<Transaction> Post([FromBody]Transaction data)
        {
            return th.getOrderItemName(data);
        }

        //// PUT: api/ItemDataTransactionApi/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ItemDataTransactionApi/5
        //public void Delete(int id)
        //{
        //}
    }
}
