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
    public class APIvendorcategoryController : ApiController
    {
        BALcateorysubcategorydropdown catsub = new BALcateorysubcategorydropdown();
        // GET: api/APIvendorcategory
        public IEnumerable<BAL_Category> Get()
        {
            return catsub.GetCategoryNames();
        }


    }
}
