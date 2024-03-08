using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class ForgotHelper : Helper
    {
        string API_URL = ConfigurationManager.AppSettings["APIBase"];
        string UI_URL = ConfigurationManager.AppSettings["UIBase"];
        string mail = ConfigurationManager.AppSettings["Email"];
        string password = ConfigurationManager.AppSettings["Password"];
        string usermail = ConfigurationManager.AppSettings["UserEmail"];
        BALErrormanag er = new BALErrormanag();
        Encrypt_Decrypt ende = new Encrypt_Decrypt();
        public string ExistEmail(string forgotEmail)
        {
            string fname = "";
            try
            {
                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT c_email,c_fname FROM t_user WHERE c_email = @c_email ;", conn);
                comcheck.Parameters.AddWithValue("@c_email", forgotEmail);
                conn.Open();
                NpgsqlDataReader rdr = comcheck.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        fname = rdr["c_fname"].ToString();

                    }
                    conn.Close();
                    return fname;
                    //return 1;
                }

                else
                {
                    conn.Close();
                    return fname;
                }

            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Forgot Password";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return fname;
            }

        }

        public string GenerateToken(string email)
        {
            try
            {
                var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();
                var resultToken = new string(Enumerable.Repeat(allChar, 8).Select(token => token[random.Next(token.Length)]).ToArray());

                //string authToken = resultToken.ToString();
                updatetoken(resultToken.ToString(), email);

                return resultToken.ToString();
            }
            catch (Exception ex)
            {

                string operation = "Generate Token";
                string pagename = "Forgot Password";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return string.Empty;
            }

        }

        public void updatetoken(string token, string email)
        {
            try
            {
                NpgsqlCommand com = new NpgsqlCommand("UPDATE t_token SET c_token=@authToken , c_timestamp=@c_timestamp where c_registermailid=@c_registermailid", conn);
                Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                com.Parameters.AddWithValue("@authToken", token);
                com.Parameters.AddWithValue("@c_registermailid", email);
                com.Parameters.AddWithValue("@c_timestamp", unixTimestamp);
                conn.Open();
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                string operation = "Update";
                string pagename = "Forgot Password";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);

            }
        }

        public int Reset(dynamic value)
        {
            //if (data.NewPassword == data.ConfirmPassword)
            //{
            try
            {
                Int32 newUnixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                //int tokenExpireSeconds;

                string token = Convert.ToString(value["Token"]);

                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT * FROM t_token WHERE c_token = @c_token", conn);
                comcheck.Parameters.AddWithValue("@c_token", token);
                conn.Open();
                NpgsqlDataReader rdr = comcheck.ExecuteReader();
                string registermailid = "";
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        registermailid = rdr["c_registermailid"].ToString();
                        if (newUnixTimestamp - Convert.ToInt32(rdr["c_timestamp"].ToString()) > 500)
                        {
                            conn.Close();
                            return 0;
                        }
                        else
                        {
                            conn.Close();
                            NpgsqlCommand cm = new NpgsqlCommand("UPDATE t_user SET c_password=@c_password WHERE c_email=@c_email", conn);
                            cm.Parameters.AddWithValue("@c_password", ende.Encryptpassword(Convert.ToString(value["UpdateForgotPassword"])));
                            cm.Parameters.AddWithValue("@c_email", registermailid);
                            //cm.Parameters.AddWithValue("@c_token", Convert.ToString(value["Token"])); 
                            conn.Open();
                            int ans = cm.ExecuteNonQuery();
                            conn.Close();
                            return 1;
                        }
                    }
                    return 0;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Reset";
                string pagename = "Forgot Password";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }

        public int SendEmail(string email, string fname)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            try
            {
                //var uname = data.c_fname;
                using (MailMessage mm = new MailMessage(mail, usermail))
                {
                    mm.Subject = "Change Password";
                    string generatedToken = GenerateToken(email);
                    string htmlString = string.Empty;
                    //htmlString = "http://localhost:60164/Home/Reset?Token=" + generatedToken;
                    string filename = AppDomain.CurrentDomain.BaseDirectory + @"\Email_Template\Forget Password_Email.html";
                    using (StreamReader read = new StreamReader(filename))
                    {
                        htmlString = read.ReadToEnd();
                    }
                    htmlString = htmlString.Replace("#[{FirstName}]#", fname);
                    //htmlString = htmlString.Replace("#[{UserName}]#", data.c_email);
                    htmlString = htmlString.Replace("#[{url}]#", UI_URL + "Login/ForgotReset?Token=" + generatedToken);

                    mm.Body =
                      htmlString;
                    // mm.Body = "this is body";


                    mm.Body =
                      htmlString;

                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCred = new NetworkCredential(mail, password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {

                string operation = "Send Mail";
                string pagename = "Forgot Password";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }
    }
}
