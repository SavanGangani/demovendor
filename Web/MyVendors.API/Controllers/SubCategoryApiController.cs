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
    public class SubCategoryApiController : ApiController
    {
        SubCategorisAuthHelper ch = new SubCategorisAuthHelper();

        // GET: api/SubCategoryApi

        public IEnumerable<Subcategories> Get()
        {
            var allUrlKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            return ch.GetAll();

        }


        // GET: api/SubCategoryApi/5
        public Subcategories Get(int c_sub_id)
        {
            return ch.GetOne(c_sub_id);
        }

        // POST: api/SubCategoryApi
        public bool Post([FromBody]Subcategories value)
        {
            var IsInserted = ch.Insert(value);
            //return Json(new { success = true, responsetext = IsInserted });
            return true;
        }

        // PUT: api/SubCategoryApi/5
        public IHttpActionResult Put(int c_sub_id, [FromBody]Subcategories value)
        {
            var IsUpdated = ch.Update(value, c_sub_id);
            return Json(new { success = true, responsetext = IsUpdated });
        }

        //DELETE: api/SubCategoryApi/5
        public bool Delete(int c_sub_id)
        {
            return ch.Delete(c_sub_id);
        }

    }
}
