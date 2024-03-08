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
    public class CategoryAPIController : ApiController
    {
        //Start:9/3/2022, Added By : Yashvi Naik , Description : Logic for Category(Insert/Edit/Display). 

        BAL_CategoryHelper ch = new BAL_CategoryHelper();
        // GET: api/CategoryAPI
        public IEnumerable<BAL_Category> Get()
        {
            return ch.GetAll();
        }

        // GET: api/CategoryAPI/5
        public BAL_Category Get(int id)
        {
            return ch.GetOne(id);
        }

        // POST: api/CategoryAPI
        public int Post([FromBody]BAL_Category value)
        {
            return ch.Insert(value);
        }

        //public IHttpActionResult Post([FromBody]BAL_Category value)
        //{
        //    var IsInserted = ch.Insert(value);
        //    return Json(new { success = true, responseText = IsInserted });
        //}

        // PUT: api/CategoryAPI/5
        public int Put(int id, [FromBody]BAL_Category value)
        {
            return ch.Edit(value, id);
        }

        // DELETE: api/CategoryAPI/5
        public bool Delete(int id)
        {
            return ch.Delete(id);

        }
    }
}
