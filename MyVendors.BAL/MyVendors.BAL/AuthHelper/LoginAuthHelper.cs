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
    public class LoginAuthHelper : Helper
    {
        Encrypt_Decrypt EDHelper = new Encrypt_Decrypt();
        BALErrormanag er = new BALErrormanag();

        public int Login(Login login)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select * from t_user where c_email = @Email", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@Email", login.Email);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    string _password = EDHelper.Decryptpassword(dr["c_password"].ToString());

                    if (_password == login.Password)
                    {
                        HttpContext.Current.Session["user_id"] = dr["c_id"].ToString();
                        var _UserName = dr["c_username"].ToString();

                        if (login.RememberMe)
                        {

                            HttpCookie mycookie = HttpContext.Current.Request.Cookies["userid"] ?? new HttpCookie("user");
                            mycookie.Values["useremail"] = dr["c_email"].ToString();
                            mycookie.Values["userpassword"] = _password;
                            HttpContext.Current.Response.Cookies.Add(mycookie);
                        }
                        conn.Close();
                        //logEntry(_UserName);
                        // LastLoginTime(_UserName);

                        return Convert.ToInt32(Convert.ToInt32(HttpContext.Current.Session["user_id"]));
                    }
                    else
                        conn.Close();
                    return 0;
                }
                conn.Close();
                return -1;
            }
            catch (Exception ex)
            {
                string operation = "Login";
                string pagename = "Login";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return -1;
            }
        }

        public void logEntry(string UserName)
        {
            try
            {
                NpgsqlCommand cmd_log = new NpgsqlCommand("insert into t_log (c_username,c_operation,c_logindatetime,c_logoutdatetime) values (@UserName,@Operation,@Logindatetime,@Logoutdatetime)", conn);
                conn.Open();
                cmd_log.Parameters.AddWithValue("@UserName", UserName);
                cmd_log.Parameters.AddWithValue("@Operation", "LogIn");
                cmd_log.Parameters.AddWithValue("@Logindatetime", DateTime.Now);
                cmd_log.Parameters.AddWithValue("@Logoutdatetime", DateTime.Now);
                int id = cmd_log.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LastLoginTime(string UserName)
        {
            try
            {
                NpgsqlCommand cmd_log = new NpgsqlCommand("update t_user set c_lastlogintime=@LastLoginTime where c_username=@UserName", conn);
                conn.Open();
                cmd_log.Parameters.AddWithValue("@UserName", UserName);
                cmd_log.Parameters.AddWithValue("@LastLoginTime", DateTime.Now.ToString());
                int id = cmd_log.ExecuteNonQuery();

                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
