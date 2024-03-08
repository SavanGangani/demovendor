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
    public class APIbankdetailsController : ApiController
    {
        BALbankdetails details = new BALbankdetails();
        // GET: api/APIbankdetails
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/APIbankdetails/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/APIbankdetails
        public IHttpActionResult Post([FromBody]Modelbankdetails value)
        {
            int updated = details.AddBank(value);
            return Json(new { success = true, responsetext = updated });
        }



        // DELETE: api/APIbankdetails/5
        public void Delete(int id)
        {
        }

    }
}
