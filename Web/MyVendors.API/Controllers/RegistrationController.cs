using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class RegistrationController : ApiController
    {
        RegistrationAuthHelper Registration_Helper = new RegistrationAuthHelper();

        public IHttpActionResult Post([FromBody] Registration value)
        {

            if (Registration_Helper.UserRegistration(value))
            {
                var response = new
                {
                    Success = true,
                    Message = "You Are Successfully Registered Please kindly Check Your Mail For Login"
                };
                return Ok(response);
            }
            else
            {
                var response = new
                {
                    Success = false,
                    Message = "User already exists !!"
                };
                return Ok(response);
            }
        }
    }
}
