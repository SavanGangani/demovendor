using MyVendors.BAL;
using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class UserProfileApiController : ApiController
    {
        UserProfileAuthHelper uah = new UserProfileAuthHelper();

        // GET: api/UserProfileApi/5

        public UserProfile Get(int id)
        {
            return uah.GetOne(id);
        }

        // POST: api/UserProfileApi
        public IHttpActionResult post(int id)
        {
            string path = HttpContext.Current.Server.MapPath("~/Images/");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            ImageModel data = new ImageModel()
            {
                id = id,
                path = path,
                request = HttpContext.Current.Request
            };
            int isValid = uah.AddImage(data);
            return Json(new { success = true, responseText = isValid });
        }

        // PUT: api/UserProfileApi/5
        public IHttpActionResult Put(int id, [FromBody]UserProfile value)
        {
            int isValid = uah.Update(value, id);
            return Json(new { success = true, responseText = isValid });
        }

        // DELETE: api/UserProfileApi/5
        public IHttpActionResult Delete(int id)
        {
            int isValid = uah.RemoveImage(id);
            return Json(new { success = true, responseText = isValid });
        }
    }
}
