using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class SubCategorisAuthHelper : Helper
    {
        //Start:10/03/2022, Added By : Henal Patel , Description : build  Logic for Sub_Category(Insert/Edit/Display)
        BALErrormanag er = new BALErrormanag();

        public List<Subcategories> GetAll()
        {
            List<Subcategories> subList = new List<Subcategories>();

            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand("select a.c_sub_name,a.c_sub_id,a.c_created_date,a.c_activated_date,a.c_status,a.c_id,b.c_id,b.c_name from t_subcategory a, t_category b where a.c_id=b.c_id  AND a.c_status=1", conn);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }
                subList = (from DataRow dr in dt.Rows
                               //where int.Parse(dr["c_id"].ToString()) == c_id
                           select new Subcategories()
                           {
                               c_sub_Id = Convert.ToInt32(dr["c_sub_id"]),
                               c_Id = Convert.ToInt32(dr["c_id"]),
                               Name = dr["c_sub_name"].ToString(),
                               CreatedDate = Convert.ToDateTime(dr["c_created_date"]),
                               ActivatedDate = Convert.ToDateTime(dr["c_activated_date"]),
                               CategoryName = dr["c_name"].ToString()

                           }).ToList();
                conn.Close();


            }
            catch (Exception ex)
            {
                string operation = "SelectALL";
                string pagename = "SubCategories";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);

            }
            return subList;
        }
        //Drop Down
        public List<Subcategories> GetEvents()
        {
            List<Subcategories> subList = new List<Subcategories>();

            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_id,c_name FROM t_category where status='true'", conn);
                conn.Open();
                NpgsqlDataReader dr1 = cmd.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                dt.Load(dr1);
                subList = (from DataRow dr in dt.Rows
                           select new Subcategories()
                           {
                               c_Id = Convert.ToInt32(dr["c_id"]),
                               Name = dr["c_name"].ToString(),

                           }).ToList();
                return subList;
                conn.Close();
            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return subList;

            }
        }


        public Subcategories GetOne(int c_sub_id)
        {
            Subcategories subList = null;

            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand("select c_id, c_sub_name,c_created_date,c_activated_date from t_subcategory", conn);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                if (datar.Read())
                {
                    dt.Load(datar);
                }


                subList = (from DataRow dr in dt.Rows
                           where int.Parse(dr["c_id"].ToString()) == c_sub_id
                           select new Subcategories()
                           {
                               c_sub_Id = Convert.ToInt32(dr["c_sub_id"]),
                               c_Id = int.Parse(dr["c_id"].ToString()),
                               Name = dr["c_sub_name"].ToString(),
                               CreatedDate = Convert.ToDateTime(dr["c_created_date"]),
                               ActivatedDate = Convert.ToDateTime(dr["c_activated_date"])


                           }).ToList().FirstOrDefault();

                conn.Close();
                return subList;
            }
            catch (Exception ex)
            {
                string operation = "GetOne";
                string pagename = "Sub Categories";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);

                return subList;
            }
        }

        public int Insert(Subcategories data)
        {
            try
            {
                if (CheckSubCategory(data.Name))
                {
                    var a = 1;
                    var b = 1;
                    //data.c_Id = int.Parse(HttpContext.Current.Session["id"].ToString());
                    NpgsqlCommand cm = new NpgsqlCommand(@"INSERT INTO t_subcategory(c_id,c_sub_name,c_created_date,c_activated_date,c_status)
                                                            VALUES(@c_Id,@Name,@CreatedDate,@ActivatedDate,@C_status)", conn);
                    cm.Parameters.AddWithValue("@c_Id", data.c_Id);
                    cm.Parameters.AddWithValue("@Name", data.Name);
                    cm.Parameters.AddWithValue("@CreatedDate", DateTime.Now.Date);
                    cm.Parameters.AddWithValue("@ActivatedDate", DateTime.Now.Date.AddDays(5));
                    cm.Parameters.AddWithValue("@C_status", b);

                    conn.Open();
                    int i = cm.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Insert";
                string pagename = "Sub Categories";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
            //conn.Close();
        }

        public bool CheckSubCategory(string name)
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(@"select * from t_subcategory where c_sub_name = @name and c_status = '1'", conn);
                cmd.Parameters.AddWithValue("@name", name);
                conn.Open();
                NpgsqlDataReader datar = cmd.ExecuteReader();
                if (datar.HasRows)
                {
                    conn.Close();
                    return false;
                }
                else
                {
                    conn.Close();
                    return true;
                }

            }
            catch (Exception ex)
            {
                string operation = "CheckSubCategory";
                string pagename = "Sub Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public int Update(Subcategories data, int c_sub_id)
        {
            try
            {
                if (CheckSubCategory(data.Name))
                {
                    //data.c_Id = int.Parse(HttpContext.Current.Session["id"].ToString());
                    NpgsqlCommand cm = new NpgsqlCommand("UPDATE t_subcategory SET c_sub_name = @pName WHERE c_sub_id = @pc_sub_Id", conn);
                    cm.Parameters.AddWithValue("@pc_sub_Id", c_sub_id);
                    cm.Parameters.AddWithValue("@pName", data.Name);
                    conn.Open();
                    int i = cm.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                    return 0;

                conn.Close();
            }
            catch (Exception ex)
            {
                string operation = "Update";
                string pagename = "Sub Categories";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);

                return 0;
            }
        }

        //Start:10/03/2022, Added By : Neha Makwana , Description : build  Logic for Sub_Category(Delete)

        public bool chechk_subcategory(int id)
        {
            try
            {
                NpgsqlCommand cmd_check = new NpgsqlCommand("select * from t_vendormaster where @Id =any(c_subcategory_id)", conn);
                cmd_check.Parameters.AddWithValue("@Id", id);
                conn.Open();
                NpgsqlDataReader datar = cmd_check.ExecuteReader();
                if (datar.HasRows)
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string operation = "Select id";
                string pagename = "Sub Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                throw;
            }

        }

        public bool Delete(int c_sub_Id)
        {
            try
            {
                if (chechk_subcategory(c_sub_Id))
                {
                    return false;
                }
                else
                {

                    NpgsqlCommand cm = new NpgsqlCommand(@"Update t_subcategory set c_status=0 where c_sub_id=@pc_sub_Id", conn);
                    cm.Parameters.AddWithValue("@pc_sub_Id", c_sub_Id);
                    conn.Open();
                    int status = cm.ExecuteNonQuery();
                    if (status > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                string operation = "Delete";
                string pagename = "Sub Categories";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }
    }
}
