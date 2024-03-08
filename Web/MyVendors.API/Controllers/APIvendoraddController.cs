using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyVendors.API.Controllers
{//Start:8/3/2022, Added By : Dhrupti Thesiya , Description : Api for pdf upload vendor details add 
    public class APIvendoraddController : ApiController
    {
        BALvendordetails vendor = new BALvendordetails();
        protected NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyVendorConnection"].ConnectionString);
        // GET: api/APIvendoradd
        public IEnumerable<Modelvendoradd> Get()
        {
            //var allURLKeyValues = ControllerContext.Request.GetQueryNameValuePairs();
            return vendor.GetAll();
        }


        //[HttpPost]
        //public IHttpActionResult Post()
        //{
        //    try
        //    {
        //        Modelvendoradd vendordata = new Modelvendoradd();
        //        var form = HttpContext.Current.Request;
        //        vendordata.c_VendorName = form.Form["c_VendorName"];
        //        vendordata.c_Mobile = form.Form["c_Mobile"];
        //        vendordata.c_Email = form.Form["c_Email"];
        //        vendordata.c_Address = form.Form["c_Address"];
        //        vendordata.c_Website = form.Form["c_Website"];
        //        vendordata.c_Id = Convert.ToInt32(form.Form["c_Id"]);
        //        vendordata.c_SubId = Convert.ToInt32(form.Form["c_SubId"]);
        //        var IsNotValid = vendor.IsVendor(vendordata);
        //        if (IsNotValid)
        //        {
        //            return Json(new { success = false, responsetext = IsNotValid });
        //        }
        //        else
        //        {

        //            string folder = HttpContext.Current.Server.MapPath("~/quotetionfiles/");
        //            if (!Directory.Exists(folder))
        //            {
        //                Directory.CreateDirectory(folder);
        //            }
        //            var savefile = "";
        //            var fileName = string.Empty;
        //            if (HttpContext.Current.Request.Files.AllKeys.Any())
        //            {
        //                var fileupload = HttpContext.Current.Request.Files["FilePathSave"];
        //                if (fileupload != null)
        //                {
        //                    fileName = fileupload.FileName;
        //                    savefile = System.IO.Path.Combine(folder, fileName);
        //                    fileupload.SaveAs(savefile);
        //                }
        //            }
        //            NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO t_vendormaster
        //                                             (c_id,c_sub_id,c_status,c_vname,c_mobile,c_email,
        //                                              c_address,c_website,c_quotation,c_gst_no,c_tax_preference,c_bill_amount,
        //                                              c_approval_status,c_noofservices,c_billno,c_createdate,status) 
        //                                              VALUES(@cid,@subid,@vstatus,@vname,@vmobile,@vemail,
        //                                              @vaddress,@vwebsite,@vquotation,0,0,0,0,0,0,@vcreateddate,'true')", conn);
        //            cmd.Parameters.AddWithValue("@cid", vendordata.c_Id);
        //            cmd.Parameters.AddWithValue("@subid", vendordata.c_SubId);
        //            cmd.Parameters.AddWithValue("@vstatus", "Active");
        //            cmd.Parameters.AddWithValue("@vname", vendordata.c_VendorName);
        //            cmd.Parameters.AddWithValue("@vmobile", vendordata.c_Mobile);
        //            cmd.Parameters.AddWithValue("@vemail", vendordata.c_Email);
        //            cmd.Parameters.AddWithValue("@vaddress", vendordata.c_Address);
        //            cmd.Parameters.AddWithValue("@vwebsite", vendordata.c_Website);
        //            cmd.Parameters.AddWithValue("@vquotation", fileName);
        //            cmd.Parameters.AddWithValue("@vcreateddate", DateTime.Now.Date);

        //            conn.Open();
        //            cmd.ExecuteNonQuery();
        //            conn.Close();

        //            return Json(new { sucess = true, responsetext = IsNotValid });
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        // PUT: api/APIvendoradd/5

        [HttpPost]
        public bool Post([FromBody]Modelvendoradd value)
        {
            return vendor.InsertVendors(value);
        }

        public bool Put([FromBody]Modelvendoradd value)
        {
            return vendor.UpdateVendors(value);
        }

        // DELETE: api/CourseAPI/5
        public bool Delete(int id)
        {
            return vendor.DeleteVenodr(id);
        }
    }
}
