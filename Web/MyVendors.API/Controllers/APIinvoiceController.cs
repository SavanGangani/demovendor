using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MyVendors.BAL;
using MyVendors.BAL.AuthHelper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.UI;

namespace MyVendors.API.Controllers
{
    public class APIinvoiceController : ApiController
    {
        protected NpgsqlConnection conn = new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyVendorConnection"].ConnectionString);

        Encrypt_Decrypt ende = new Encrypt_Decrypt();
        public int Get(int id = 0)
        {
            string strhtml = string.Empty;

            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("select * from t_vendormaster vendor,t_bankdetails bank where vendor.c_vid =bank.c_vid AND vendor.c_vid=" + id + "", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (reader.Read())
                {
                    string account_number = ende.Decryptpassword(reader["c_account_number"].ToString());

                    strhtml = "<div style='width: 100%;border:1px solid'>"
                              + "<table style='width: 100%;'>"
                                   + "<tr bgcolor='#9932cc' style= 'display:flex;justify-content:center;height:60px;text-align: center; width:100%;'>"
                                   + "<th style='color:#fff;height: 60px;'><h1> Invoice </h1></th>"
                                 + "</tr>"
                                + "</table>"

                                  + "<table style='width: 100%; padding: 30px;'>"
                                    + "<tr>"
                                      + "<td style='width: 60%;'>Bill Number:" + reader["c_billno"].ToString() + "</ td > "

                                      + "<td style='width: 40%;float:right;'>Vendor Name:" + reader["c_vname"].ToString() + "</ td > "
                                    + "</tr>"
                                    + "<tr>"
                                      + "<td style='width: 60%;'>Date:" + DateTime.Now.Date.ToShortDateString() + "</ td >"

                                      + "<td style='width: 40%;float:right;'>Vendor Contact:" + reader["c_mobile"].ToString() + "</ td > "
                                    + "</tr>"
                                    + "<tr>"
                                      + "<td style='width: 60%;'></td>"

                                      + "<td style='width: 40%;float:right;' >Vendor Email:" + reader["c_email"].ToString() + "</ td > "
                                    + "</tr>"
                                    + "<tr>"
                                      + "<td style='width: 60%;'></td>"

                                      + "<td style='width: 40%;float:right;'>Vendor Address:" + reader["c_address"].ToString() + "</ td > "
                                    + "</tr>"
                                  + "</table>"
                                   + "<div style='padding:0 2%;'><br>"
                                    + "<table style='width: 100%; padding: 20px;border:1px solid;border-radius: 10px;'>"
                                      + "<tr>"
                                          + "<td style='width: 30%;'>Gst Number :</td>"
                                          + "<td style='width: 30%;'>" + reader["c_gst_no"].ToString() + "</td>"
                                          + "<td style='width: 40%;'></ td > "
                                      + "</tr>"
                                       + "<tr>"
                                          + "<td style='width: 30%;'>Tax Preference :</td>"
                                          + "<td style='width: 30%;'>" + reader["c_tax_preference"].ToString() + "</td>"
                                          + "<td style='width: 40%;'></ td > "
                                      + "</tr>"
                                      + "<tr>"
                                          + "<td style='width: 30%;'>No Of AMC Provided :</td>"
                                          + "<td style='width: 30%;'>" + reader["c_noofservices"].ToString() + "</td>"
                                          + "<td style='width: 40%;'></ td > "
                                      + "</tr>"
                                      + "<tr>"
                                          + "<td style='width: 30%;'>Bank Name :</td>"
                                          + "<td style='width: 30%;'>" + reader["c_bank_name"].ToString() + "</td>"
                                          + "<td style='width: 40%;'></ td > "
                                      + "</tr>"
                                      + "<tr>"
                                          + "<td style='width: 30%;'>Account Number :</td>"
                                          + "<td style='width: 30%;'>XXXXXXXX" + ((Convert.ToInt64(account_number) % 100).ToString().PadLeft(2, '0')).ToString() + "</td>"
                                          + "<td style='width: 40%;'></ td > "
                                      + "</tr>"
                                      + "<tr>"
                                          + "<td style='width: 30%;'>Account Holder Name:</td>"
                                          + "<td style='width: 30%;'>" + reader["c_account_name"].ToString() + "</td>"
                                          + "<td style='width: 40%;'></ td > "
                                      + "</tr>"
                                    + "</table>"
                                    + "<table style='width: 100%; padding: 30px;'>"
                                      + "<tr>"
                                          + "<td style='width: 33.33%;'></td>"
                                          + "<td style='width: 33.33%;text-align: center;'></td>"
                                          + "<td style='width: 33.33%;'>Bill Amount:" + reader["c_bill_amount"].ToString() + "</ td > "
                                      + "</tr>"
                                    + "</table>"
                                     + "</div>"
                                + "</div>";

                }

                conn.Close();
                Rectangle page = new Rectangle(PageSize.A4);
                var fileName = id + "_.pdf";
                var path = HttpContext.Current.Server.MapPath("~/Invoicepdf/" + fileName);
                File.Delete(path);

                using (FileStream file = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                {

                    Document pdf = new Document(page, 36, 72, 108, 180);
                    PdfWriter writer = PdfWriter.GetInstance(pdf, file);

                    pdf.Open();
                    HTMLWorker hw = new HTMLWorker(pdf);
                    hw.Parse(new StringReader(strhtml));
                    writer.CloseStream = false;
                    pdf.Close();

                }

                BALApproveStatus helper = new BALApproveStatus();
                helper.SendInvoice(fileName);

            }
            catch (Exception ex)
            {
                return 1;
            }
            return 0;
        }

    }
}
