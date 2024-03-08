using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;

namespace MyVendors.API.Controllers
{
    public class GetOrderIdApiController : ApiController
    {
        TransactionHelper th = new TransactionHelper();
        // GET: api/GetOrderIdApi
        public List<int> Get([FromBody]Transaction data)
        {
            return th.getOrderID(data);
        }

        //public List<int> Get([FromBody]Transaction data)
        //{
        //    return th.getAllId(data);
        //}

        //// GET: api/GetOrderIdApi/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/GetOrderIdApi
        public List<int> Post([FromBody]Transaction data)
        {
            return th.getOrderID(data);
        }
        // POST: api/GetOrderIdApi
        //public List<int> Post([FromBody]Transaction data)
        //{
        //    return th.getAllId(data);
        //}

        //// PUT: api/GetOrderIdApi/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/GetOrderIdApi/5
        //public void Delete(int id)
        //{
        //}
    }
}
