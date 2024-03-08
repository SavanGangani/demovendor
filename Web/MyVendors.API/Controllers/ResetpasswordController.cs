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
    //Start:25/2/2022, Added By : Dhrupti Thesiya , Description : return result of reset password method
    public class ResetpasswordController : ApiController
    {
        ResetPassword_logic resetpass = new ResetPassword_logic();

        // PUT: api/Resetpassword/5
        public IHttpActionResult Put(int id, [FromBody] ResetPass value)
        {
            int isValid = resetpass.ResetPasswordUser(id, value);
            return Json(new { success = true, responseText = isValid });
        }

    }
}
