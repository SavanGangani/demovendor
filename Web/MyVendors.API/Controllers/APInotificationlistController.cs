using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    // Added By :Dhrupti Thesiya
    public class APInotificationlistController : ApiController
    {
        BALNotification noti = new BALNotification();
        // GET: api/APInotificationlist
        #region
        public IHttpActionResult Get()
        {
            IEnumerable<Modelnotification> RegiNotification = noti.RegisterNotification();
            int noticounter = noti.GetCounter();
            return Json(new { success = true, responsetext = RegiNotification, noticounter });
        }

        //public IHttpActionResult Get()
        //{
        //    IEnumerable<string> RegiNotification = noti.RegisterNotification();
        //    //int noticounter = noti.GetCounter();
        //    //return Json(new { success = true, responsetext = RegiNotification, noticounter });

        //}
        #endregion
        #region
        // GET: api/APInotificationlist
        //public IHttpActionResult Get(string id)
        //{
        //    noti.Delq(id);
        //    int decount = noti.Decrementcounter();
        //    return Json(new { success = true, responsetext = decount });

        //}
        #endregion

    }
}
