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
    public class ApiVendorDDController : ApiController
    {

        BALVendorDDHelper helper = new BALVendorDDHelper();
        BALOrder helperorder = new BALOrder();

        public IEnumerable<VendorDropDownModel> Get()
        {
            return helper.GetVendorName();
        }

        public List<Modelsubcategory> Get(int id)
        {
            return helper.GetSubCategory(id);
        }

        [Route("api/ApiVendorDD/GetVendorOne")]
        public List<VendorDropDownModel> GetVendorOne(int id)
        {
            return helperorder.GetVendorData(id);
        }


    }
}
