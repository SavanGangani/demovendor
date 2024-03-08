using MyVendors.BAL.AuthHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class ForgotPasswordApiController : ApiController
    {
        ForgotHelper fh = new ForgotHelper();
        // GET: api/ForgotPasswoordAPI
        public IEnumerable<string> Get()
        {
            return new string[] { "value1ForgotPasswoordAPI", "value2" };
        }

        // GET: api/ForgotPasswoordAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ForgotPasswoordAPI
        public object Post([FromBody]dynamic value)
        {
            string val = fh.ExistEmail(Convert.ToString(value["ForgotByEmail"]));
            if (val != "")
                return val;
            //return new BAL.Models.TblLogin();
            else
                return "";

        }

        [Route("api/ForgotPasswordAPI/UpdatePassword")]
        [HttpPost]
        public int UpdatePassword([FromBody]dynamic value)
        {
            return fh.Reset(value);
        }

        //[Route("api/register/SendEmail")]
        [Route("api/ForgotPasswordAPI/SendEmail")]
        [HttpPost]
        public int SendEmail([FromBody]dynamic value)
        {
            return fh.SendEmail(Convert.ToString(value["ForgotByEmail"]), Convert.ToString(value["fname"]));
            //return fh.SendEmail(Convert.ToString(value["ForgotByEmail"]), Convert.ToString(value["fname"]));
        }

    }
}
