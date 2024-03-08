using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Configuration;
using MyVendors.BAL.AuthHelper;

namespace MyVendors.BAL.Models
{
    public class RegistrationAuthHelper : Helper
    {
        Encrypt_Decrypt EDHelper = new Encrypt_Decrypt();

        string mail = ConfigurationManager.AppSettings["Email"];
        string password = ConfigurationManager.AppSettings["Password"];
        string usermail = ConfigurationManager.AppSettings["UserEmail"];
        string UI_URL = ConfigurationManager.AppSettings["UIBase"];
        BALErrormanag er = new BALErrormanag();

        public bool UserRegistration(Registration data)
        {

            if (EmailCheck(data))
            {
                return false;
            }
            else
            {
                try
                {
                    string _Password = EDHelper.Encryptpassword(data.Password);
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO public.t_user(c_fname, c_lname,c_username,c_email, c_password,c_createddate) VALUES(@Fname,@Lname,@Email,@Email,@Password,@datecurrent);", conn);
                    NpgsqlCommand cmd_email = new NpgsqlCommand("INSERT INTO public.t_token(c_registermailid) VALUES(@Email_token);", conn);
                    cmd.Parameters.AddWithValue("@fname", data.Fname);
                    cmd.Parameters.AddWithValue("@lname", data.Lname);
                    cmd.Parameters.AddWithValue("@email", data.Email);
                    cmd.Parameters.AddWithValue("@password", _Password);
                    cmd.Parameters.AddWithValue("@datecurrent", DateTime.Now.ToString("dd/MM/yyyy"));
                    cmd.ExecuteNonQuery();
                    cmd_email.Parameters.AddWithValue("@Email_token", data.Email);
                    cmd_email.ExecuteNonQuery();
                    SendMail(data);
                    conn.Close();
                    return true;
                }
                catch (Exception ex)
                {
                    string operation = "Insert";
                    string pagename = "User Registration";
                    DateTime date = DateTime.Now.Date;
                    er.InsertErrorLog(operation, pagename, date, ex.Message); ;
                    return false;
                }
            }
        }

        public bool EmailCheck(Registration data)
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_email FROM  public.t_user WHERE c_email=@Email", conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@Email", data.Email);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    conn.Close();
                    return true;
                }
                conn.Close();
                return false;
            }
            catch (Exception ex)
            {
                string operation = "Check Email";
                string pagename = "Registration";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public void SendMail(Registration data)
        {
            try
            {
                string to = usermail;
                string subject = "Successfully Registered";


                string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Email_Template\Registration_Email.html";

                StreamReader str = new StreamReader(FilePath);
                string body = str.ReadToEnd();
                str.Close();
                body = body.Replace("#[{UserName}]#", data.Email);
                body = body.Replace("#[{FirstName}]#", data.Fname);
                body = body.Replace("#[{url}]#", UI_URL + "/Login/Login");

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(mail);
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential(mail, password);
                smtp.Send(mm);
            }
            catch (Exception ex)
            {
                string operation = "Send Mail";
                string pagename = "User Registration";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }
        }
    }
}
