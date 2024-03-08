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
    public class APIsubcategorydropdownController : ApiController
    {
        BALcateorysubcategorydropdown subcategory = new BALcateorysubcategorydropdown();
        // GET: api/APIsubcategorydropdown
        public IEnumerable<Modelsubcategory> Get(string id)
        {
            return subcategory.GetSubCategoryNames(id);
        }

    }
}
