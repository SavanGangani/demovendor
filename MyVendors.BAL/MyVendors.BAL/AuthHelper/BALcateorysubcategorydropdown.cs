using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class BALcateorysubcategorydropdown : Helper
    {
        BALErrormanag er = new BALErrormanag();

        public List<BAL_Category> GetCategoryNames()
        {
            List<BAL_Category> vnames = new List<BAL_Category>();

            try
            {
                string query = "SELECT c_id,c_name FROM t_category where status='true'";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    using (NpgsqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            vnames.Add(new BAL_Category
                            {
                                Name = sdr["c_name"].ToString(),
                                Id = Convert.ToInt32(sdr["c_Id"])
                            });
                        }
                    }
                    conn.Close();
                }
                return vnames;
            }
            catch (Exception ex)
            {
                string operation = "Select";
                string pagename = "Dropdown CatSub";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return vnames;
            }
        }


        public List<Modelsubcategory> GetSubCategoryNames(string id)
        {
            List<Modelsubcategory> vnames = new List<Modelsubcategory>();

            try
            {
                string query = @"SELECT c_sub_id,c_sub_name 
                            FROM t_subcategory
                            WHERE c_id in (" + id + ")";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    //cmd.Parameters.AddWithValue("@cid", id);
                    conn.Open();
                    NpgsqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            vnames.Add(new Modelsubcategory
                            {
                                c_SubName = sdr["c_sub_name"].ToString(),
                                c_SubId = Convert.ToInt32(sdr["c_sub_id"])
                            });
                        }
                        conn.Close();
                    }

                }
                return vnames;
            }
            catch (Exception ex)
            {
                string operation = "Select";
                string pagename = "Dropdown CatSub";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return vnames;
            }
        }
    }

}
