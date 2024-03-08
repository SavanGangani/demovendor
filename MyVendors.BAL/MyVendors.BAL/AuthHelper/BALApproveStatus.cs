using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MyVendors.BAL.AuthHelper
{
    public class BALApproveStatus : Helper
    {
        string mail = ConfigurationManager.AppSettings["Email"];
        string password = ConfigurationManager.AppSettings["Password"];
        string usermail = ConfigurationManager.AppSettings["UserEmail"];
        string UI_URL = ConfigurationManager.AppSettings["UIBase"];
        BALErrormanag er = new BALErrormanag();

        public void ApproveStatus(int id)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE t_vendormaster SET c_approval_status = @Approval WHERE c_vid = @Vid", conn);
                cmd.Parameters.AddWithValue("@Vid", id);
                cmd.Parameters.AddWithValue("@Approval", 1);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {

                string operation = "Approval Status";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }

        }

        public void GetData(int id)
        {
            try
            {
                conn.Open();
                ModelApprovalStatus data = new ModelApprovalStatus();
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand(@"select c_vid,c_name,c_sub_name,c_quotation from t_category c,t_vendormaster v,t_subcategory s where c.c_id <> v.c_id and v.c_sub_id = s.c_sub_id and c_vid = @Vid", conn);
                cmd.Parameters.AddWithValue("@Vid", id);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                }

                data.Vid = Int32.Parse(dr["c_vid"].ToString());
                data.Category = dr["c_name"].ToString();
                data.SubCategory = dr["c_sub_name"].ToString();
                data.PdfName = dr["c_quotation"].ToString();

                SendApproval(data);

                conn.Close();

            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = " Venodr";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }
        }

        public void SendApproval(ModelApprovalStatus data)
        {
            try
            {
                //string to = "jetpariyaparth@gmail.com";
                string to = usermail;
                string subject = "Accept Approval Status";


                string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Email_Template\Vendors_Email.html";
                var path = HttpContext.Current.Server.MapPath("~/quotetionfiles/" + data.PdfName);

                StreamReader str = new StreamReader(FilePath);
                string body = str.ReadToEnd();
                str.Close();
                body = body.Replace("#[{category}]#", data.Category);
                body = body.Replace("#[{sub-category}]#", data.SubCategory);
                body = body.Replace("#[{url}]#", UI_URL + "approval/Update/" + data.Vid);

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(mail);
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = body;
                mm.Attachments.Add(new Attachment(path));
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

                string operation = "Send Approval";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);

            }

        }

        public void SendInvoice(string fileName)
        {

            try
            {

                string to = usermail;
                string subject = "Bill Invoice";

                var path = HttpContext.Current.Server.MapPath("~/Invoicepdf/" + fileName);

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(mail);
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = "";
                mm.Attachments.Add(new Attachment(path));
                //mm.Attachments.Add(new Attachment(path));
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

                string operation = "Send Invoice";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }
        }
    }
}
