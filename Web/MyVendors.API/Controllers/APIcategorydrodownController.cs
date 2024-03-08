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
    public class APIcategorydrodownController : ApiController
    {
        SubCategorisAuthHelper helper = new SubCategorisAuthHelper();
        // GET: api/APIcategorydrodown
        public IEnumerable<Subcategories> Get()
        {
            return helper.GetEvents();

        }

    }
}
