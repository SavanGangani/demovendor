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
    public class OrderSearchApiController : ApiController
    {
        BALOrder order = new BALOrder();
        BALServiceAuthHelper service = new BALServiceAuthHelper();
        [HttpPost]
        public List<ModelOrder> POST([FromBody]string data)
        {
            return order.OrderSearch(data);
        }

        [HttpPut]
        public List<ModelService> PUT([FromBody]string data)
        {
            return service.ServiceSearch(data);
        }
    }
}
