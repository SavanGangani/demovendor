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
    public class BAL_CategoryHelper : Helper
    {
        //Start:9/3/2022, Added By : Yashvi Naik , Description : Logic for Category(Insert/Edit/Display). 
        BALErrormanag er = new BALErrormanag();

        public int Insert(BAL_Category data)
        {
            try
            {
                if (CheckCategory(data.Name))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO t_category(c_name,c_status,c_created_date,c_activated_date,status) VALUES(@Name,@c_status,@CreatedDate,@ActivetedDate,true)", conn);
                    cmd.Parameters.AddWithValue("@Name", data.Name);
                    cmd.Parameters.AddWithValue("@c_status", "Active");
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@ActivetedDate", DateTime.Now.Date.AddDays(5));
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
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
            }
            catch (Exception ex)
            {
                string operation = "Insert";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }

        public bool CheckCategory(string name)
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(@"select * from t_category where c_name = @name and status = 'true'", conn);
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
                string operation = "Select";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        //display
        public List<BAL_Category> GetAll()
        {
            List<BAL_Category> contactList = new List<BAL_Category>();
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM t_category where status='true'", conn);
                conn.Open();
                NpgsqlDataReader datar = cmd.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }
                contactList = (from DataRow dr in dt.Rows
                                   //where int.Parse(dr["cid"].ToString()) == Id
                               select new BAL_Category()
                               {
                                   Id = Convert.ToInt32(dr["c_id"]),
                                   Name = dr["c_name"].ToString(),
                                   //Status = Convert.ToBoolean(dr["c_status"].ToString()),
                                   Status = dr["c_status"].ToString(),
                                   CreatedDate = Convert.ToDateTime(dr["c_created_date"]),
                                   ActivatedDate = Convert.ToDateTime(dr["c_activated_date"])
                               }).ToList();
                conn.Close();
                return contactList;
            }
            catch (Exception ex)
            {
                string operation = "Select";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return contactList;
            }
        }

        public int Edit(BAL_Category c, int id)
        {
            try
            {
                if (CheckCategory(c.Name))
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE t_category SET c_name = @Name WHERE c_id = @Id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Name", c.Name);
                    conn.Open();
                    int i = cmd.ExecuteNonQuery();
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
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }

        public BAL_Category GetOne(int id)
        {
            BAL_Category category = null;
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM t_category", conn);
                conn.Open();
                NpgsqlDataReader datar = cmd.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }
                conn.Close();

                category = (from DataRow dr in dt.Rows
                                         where int.Parse(dr["id"].ToString()) == id
                                         select new BAL_Category()
                                         {
                                             Name = dr["c_name"].ToString(),
                                             //Status = Convert.ToBoolean(dr["c_status"].ToString()),
                                             Status = dr["c_status"].ToString(),
                                             CreatedDate = Convert.ToDateTime(dr["c_created_date"]),
                                             ActivatedDate = Convert.ToDateTime(dr["c_activated_date"])
                                         }).ToList().FirstOrDefault();

                return category;
            }
            catch (Exception ex)
            {
                string operation = "Select One";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return category;
            }
        }

        public bool Delete(int id)
        {

            try
            {
                if (chechk_category(id))
                {
                    return false;
                }

                else
                {
                    NpgsqlCommand cmd = new NpgsqlCommand("UPDATE t_category set status='false' where c_id = @Idd", conn);
                    cmd.Parameters.AddWithValue("@Idd", id);
                    conn.Open();
                    int status = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (status > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                string operation = "Delete";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public bool chechk_category(int id)
        {
            try
            {
                NpgsqlCommand cmd_check = new NpgsqlCommand("select * from t_subcategory where c_id = @Id", conn);
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
                string operation = "Select Check";
                string pagename = "Category";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }

        }
    }
}
