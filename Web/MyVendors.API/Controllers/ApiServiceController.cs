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
    public class ApiServiceController : ApiController
    {
        BALServiceAuthHelper helper = new BALServiceAuthHelper();

        public IEnumerable<ModelService> Get()
        {
            return helper.GetAllService();
        }

        public List<ModelService> GetOne(int id)
        {
            return helper.GetOneService(id);
        }

        public bool Post(List<ModelService> data)
        {
            helper.InsertService(data);
            return true;
        }
        public int Put(List<ModelService> data)
        {
            int st = helper.UpdateService(data);
            return st;
        }

        public void Delete(int id)
        {
            helper.DeleteService(id);
        }

    }
}
