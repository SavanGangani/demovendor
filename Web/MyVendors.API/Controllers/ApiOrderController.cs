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
    public class ApiOrderController : ApiController
    {

        BALOrder helper = new BALOrder();

        public IEnumerable<ModelOrder> Get()
        {
            return helper.GetAllOrder();
        }

        public List<ModelOrder> GetOne(int id)
        {
            return helper.GetSubCategory(id);
        }

        public bool Post(List<ModelOrder> data)
        {
            helper.InsertOrder(data);
            return true;
        }
        // Update Api
        public int Put(List<ModelOrder> data)
        {
            int st = helper.UpdateOrder(data);
            return st;
        }
        public bool Delete(int id)
        {
            return helper.DeleteOrder(id);
        }


        

    }
}
