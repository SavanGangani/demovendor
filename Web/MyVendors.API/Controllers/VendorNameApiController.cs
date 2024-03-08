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
    public class VendorNameApiController : ApiController
    {
        TransactionHelper th = new TransactionHelper();
        // GET: api/GetVendorNameApi
        public List<string> Get([FromBody]Transaction data)
        {
            return null;
        }

        // GET: api/GetVendorNameApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GetVendorNameApi
        public List<string> Post([FromBody]Transaction data)
        {
            return th.getAllVendorName(data);
        }

        // PUT: api/GetVendorNameApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GetVendorNameApi/5
        public void Delete(int id)
        {
        }
    }
}
