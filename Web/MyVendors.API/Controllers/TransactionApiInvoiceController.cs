using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using MyVendors.BAL;
using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
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
    public class TransactionApiInvoiceController : ApiController
    {
        protected NpgsqlConnection conn = new NpgsqlConnection("Server=cipg01;Port=5432;Database=myvendors;User Id = postgres;Password=123456");

        Encrypt_Decrypt ende = new Encrypt_Decrypt();
        public int Post(int id)
        {
            try
            {
                string strhtml = string.Empty;

                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT t1.c_transaction_id, t1.c_order_id, t2.c_vname, t1.c_transaction_date,sub.c_sub_name,
                                                        t1.c_total_price ,qt.c_receiveditem,details.c_qauntity
                                                        FROM t_transaction t1 
                                                        JOIN t_vendormaster t2 
                                                        ON t1.c_vid=t2.c_vid 
                                                        JOIN t_orderqty qt
                                                        ON  t1.c_transaction_id=qt.c_transaction_id
                                                        JOIN t_orderdetails details
                                                        ON  t1.c_order_id=details.c_order_id
														JOIN t_subcategory sub
														ON  sub.c_sub_id=details.c_sub_id
                                                        WHERE t1.c_vid=t2.c_vid AND t1.c_isdeleted=false 
                                                        AND t1.c_transaction_id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (reader.Read())
                {
                    strhtml += @"<div style='width: 100%;border:1px solid'>"
                                + " <table style='width: 100%;'>"
                                + "<tr bgcolor='#9932cc' style='display:flex;justify-content:center;height:60px;text-align: center; width:100%;'>"
                                + "<th style='color:#fff;height: 60px;'><h1> Transaction </h1></th>"
                                + "</tr>"
                            + "</table>"

                            + "<table style = 'width: 100%; padding: 30px;border:2;'>"
                            + "<tr>"

                        + "<td style = 'width: 60%;'><b> Transaction Number: " + reader["c_transaction_id"].ToString() + "</b> </td>"
                        + "<td style = 'width: 40%;'><b> Vendor Name:" + reader["c_vname"].ToString() + "</b></td>"
                          + "</tr>"
                           + "<tr>"
                               + "<td style = 'width: 60%;' ><b> Date:  " + reader["c_transaction_date"].ToString() + "</b></td>"
                                       + "<td style = 'width: 40%;'><b> Order / Service: " + reader["c_order_id"].ToString() + "</b></td>"
                                          + "</tr>"
                                           + "<tr>"
                                              + "<td style = 'width: 60%;'></td>"
                                                + "<td style = 'width: 40%;' ><b> Items:" + reader["c_sub_name"].ToString() + "</b></td>"
                                                  + "</tr>"
                                          + "<tr>"
                                              + "<td style = 'width: 60%;'></td>"
                                                + "<td style = 'width: 40%;' ><b>Quantity:" + reader["c_qauntity"].ToString() + "</b></td>"
                                                  + "</tr>"
                                                + "<tr>"
                                              + "<td style = 'width: 60%;'></td>"
                                                + "<td style = 'width: 40%;' ><b> Received items: " + reader["c_receiveditem"].ToString() + " </b></td>"
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
                return 1;
            }
            catch (Exception ex)
            {
                string operation = "Send Mail";
                string pagename = "User Registration";
                DateTime date = DateTime.Now.Date;
                var path = HttpContext.Current.Server.MapPath("~/ErrorManagementfile/Errormanag.txt");
                FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
                StreamWriter switer = new StreamWriter(filestream);
                switer.WriteLine("\n" + operation + "\t\t|\t" + pagename + "\t\t|\t" + date + "\t|\t" + ex.Message);
                switer.Flush();
                switer.Close();
                return 0;
            }
        }
    }
}
