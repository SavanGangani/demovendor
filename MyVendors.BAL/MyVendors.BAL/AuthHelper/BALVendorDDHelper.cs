using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class BALVendorDDHelper : Helper
    {
        BALServiceAuthHelper helper = new BALServiceAuthHelper();
        BALErrormanag er = new BALErrormanag();
        public List<VendorDropDownModel> GetVendorName()
        {
            List<VendorDropDownModel> subList = new List<VendorDropDownModel>();
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand("select c_vid,c_vname from t_vendormaster", conn);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }
                subList = (from DataRow dr in dt.Rows
                               //where int.Parse(dr["c_id"].ToString()) == c_id
                           select new VendorDropDownModel()
                           {
                               c_vid = Convert.ToInt32(dr["c_vid"]),
                               c_vname = dr["c_vname"].ToString()


                           }).ToList();
                conn.Close();
                return subList;

            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Venodr";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return subList;

            }
        }

        public List<Modelsubcategory> GetSubCategory(int id)
        {
            List<Modelsubcategory> subCatList = new List<Modelsubcategory>();

            try
            {
                int[] subcat;
                NpgsqlCommand cm = new NpgsqlCommand("select c_subcategory_id from t_vendormaster where c_vid = @Id", conn);
                cm.Parameters.AddWithValue("@Id", id);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                if (datar.HasRows)
                {
                    datar.Read();
                }
                subcat = ((int[])datar["c_subcategory_id"]);
                conn.Close();
                string subCatId = ArrToString(subcat);

                //Data Add in list
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand("select c_sub_id,c_sub_name from t_subcategory where c_sub_id in (" + subCatId + ")", conn);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                subCatList = (from DataRow dr in dt.Rows
                              select new Modelsubcategory()
                              {
                                  c_SubId = Convert.ToInt32(dr["c_sub_id"]),
                                  c_SubName = dr["c_sub_name"].ToString()

                              }).ToList();
                conn.Close();
                return subCatList;

            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Venodr";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return subCatList;
            }

        }

        public string ArrToString(int[] data)
        {
            string id_string = string.Empty;


            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    id_string += data[i];
                }
                else
                {
                    id_string += "," + data[i];
                }
            }

            return id_string;

        }

    }
}
