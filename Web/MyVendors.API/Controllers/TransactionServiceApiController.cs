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
    public class TransactionServiceApiController : ApiController
    {
        TransactionHelper th = new TransactionHelper();
        // GET: api/TransactionServiceApi
        public IEnumerable<Transaction> Get()
        {
            return th.GetAllServiceTrasaction();
        }

       

        //POST: api/TransactionServiceApi

       
        public int Post([FromBody]Transaction value)
        {
            return th.InsertService(value);
        }
        // PUT: api/TransactionServiceApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TransactionServiceApi/5
        public void Delete(int id)
        {
        }
    }
}
