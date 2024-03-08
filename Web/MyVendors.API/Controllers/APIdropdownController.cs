using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class APIdropdownController : ApiController
    {
        protected NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyVendorConnection"].ConnectionString);
        public IEnumerable<Modelbankdetails> Get()
        {
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_vid,c_vname FROM t_vendormaster", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt.Load(dr);

                }

                List<Modelbankdetails> vnames = new List<Modelbankdetails>();
                vnames = (from DataRow drData in dt.Rows
                          select new Modelbankdetails()
                          {
                              c_VendorName = drData["c_vname"].ToString(),
                              c_VendorId = Convert.ToInt32(drData["c_vid"])

                          }).ToList();

                conn.Close();
                return vnames;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
