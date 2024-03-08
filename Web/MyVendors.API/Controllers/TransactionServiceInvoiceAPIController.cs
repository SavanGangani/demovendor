using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MyVendors.BAL;
using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class TransactionServiceInvoiceAPIController : ApiController
    {
        protected NpgsqlConnection conn = new NpgsqlConnection("Server=cipg01;Port=5432;Database=myvendors;User Id = postgres;Password=123456");
        BALErrormanag er = new BALErrormanag();
        Encrypt_Decrypt ende = new Encrypt_Decrypt();
        public int Post([FromBody]Transaction data)
        {
            try
            {
                string strhtml = string.Empty;

                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT t1.c_transaction_id, t1.c_service_id, t2.c_vname, t1.c_transaction_date, t1.c_total_price 
                                                        FROM t_servicetransaction t1 
                                                        FULL JOIN t_vendormaster t2 
                                                        ON t1.c_vid=t2.c_vid 
                                                        WHERE t1.c_vid=t2.c_vid AND t1.c_isdeleted=false AND t1.c_transaction_id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", data.TransactionId);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (reader.Read())
                {
                    strhtml = @"<div style='width: 100%;border:1px solid'>"
                                + " <table style='width: 100%;'>"
                                + "<tr bgcolor='#9932cc' style='display:flex;justify-content:center;height:60px;text-align: center; width:100%;'>"
                                + "<th style='color:#fff;height: 60px;'><h1> Transaction </h1></th>"
                                + "</tr>"
                            + "</table>"

                            + "<table style = 'width: 100%; padding: 30px;'>"
                            + "<tr>"

                        + "<td style = 'width: 60%;'><b> Transaction Number: " + reader["c_transaction_id"].ToString() + "</b> </td>"
                        + "<td style = 'width: 40%;'><b> Vendor Name:" + reader["c_vname"].ToString() + "</b></td>"
                          + "</tr>"
                           + "<tr>"
                               + "<td style = 'width: 60%;' ><b> Date:  " + reader["c_transaction_date"].ToString() + "</b></td>"
                                       + "<td style = 'width: 40%;'><b>  Service No.: " + reader["c_service_id"].ToString() + "</b></td>"
                                          + "</tr>"
                                               + "</table>"
                                                + "<div style = 'padding:0 2%;'>"
                                                    + "</br>"
                                                     + "<table style = 'width: 100%; padding: 30px;'>"
                                                         + "<tr>"
                                                             + "<td style = 'width: 33.33%;'></td>"
                                                              + "<td style = 'width: 33.33%;text-align: center;'></td>"
                                                               + "<td style = 'width: 33.33%;'> <b> Total Amount:" + reader["c_total_price"].ToString() + "</b></td>"
                                                                  + "</tr>"
                                                              + "</table>"
                                                          + "</div>"
                                                      + "</div>";

                }

                conn.Close();
                Rectangle page = new Rectangle(PageSize.A4);
                var fileName = data.TransactionId + "_.pdf";
                var path = HttpContext.Current.Server.MapPath("~/Invoicepdf/" + fileName);
                //File.Delete(path);

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
                return 1;
            }
            catch (Exception ex)
            {
                string operation = "Send Mail";
                string pagename = "User Registration";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }
    }
}
