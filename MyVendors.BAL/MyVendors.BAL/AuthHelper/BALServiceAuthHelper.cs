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
    public class BALServiceAuthHelper : Helper
    {
        string mail = ConfigurationManager.AppSettings["Email"];
        string password = ConfigurationManager.AppSettings["Password"];
        string usermail = ConfigurationManager.AppSettings["UserEmail"];
        string UI_URL = ConfigurationManager.AppSettings["UIBase"];
        BALErrormanag er = new BALErrormanag();

        public List<ModelService> GetAllService()
        {
            List<ModelService> OrderList = new List<ModelService>();
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand(@"SELECT vendor.c_vid,vendor.c_vname,service.c_service_date,
                                                        service.c_service_status,service.c_service_id,service.c_payment_status
                                                        FROM t_service service 
                                                        JOIN t_vendormaster vendor  
                                                        ON  service.c_vid=vendor.c_vid 
                                                        WHERE service.c_status=1", conn);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();

                if (datar.HasRows)
                {
                    dt.Load(datar);

                    OrderList = (from DataRow dr in dt.Rows
                                 select new ModelService()
                                 {
                                     VendorId = Convert.ToInt32(dr["c_vid"].ToString()),
                                     ServiceId = Convert.ToInt32(dr["c_service_id"]),
                                     VendorName = dr["c_vname"].ToString(),
                                     ServiceStatus = dr["c_service_status"].ToString(),
                                     PaymentStatus = dr["c_payment_status"].ToString(),
                                     ServiceDate = Convert.ToDateTime(dr["c_service_date"].ToString())
                                 }).ToList();
                }

                conn.Close();
                return OrderList;

            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return OrderList;
            }

        }

        public List<ModelService> GetOneService(int id)
        {
            List<ModelService> serviceList = new List<ModelService>();

            try
            {
                NpgsqlCommand cm = new NpgsqlCommand(@"SELECT subcat.c_sub_name,vendor.c_vid,vendor.c_vname,vendor.c_email,vendor.c_mobile,details.c_service,details.c_repair,service.c_service_date,
                                                        service.c_payment_status,service.c_service_status,
														details.c_notes,details.t_sitemid,
                                                        details.c_sub_id,service.c_service_id
                                                        FROM t_service service
                                                        JOIN t_vendormaster vendor
                                                        ON  service.c_vid = vendor.c_vid
                                                        JOIN  t_servicedetails details
                                                        ON  service.c_service_id = details.c_service_id
                                                        JOIN t_subcategory subcat
                                                        ON subcat.c_sub_id = details.c_sub_id WHERE service.c_service_id=@Id", conn);
                cm.Parameters.AddWithValue("@Id", id);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                DataTable dt = new DataTable();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }

                serviceList = (from DataRow dr in dt.Rows
                               select new ModelService()
                               {
                                   ServiceId = Convert.ToInt32(dr["c_service_id"]),
                                   SubCatName = dr["c_sub_name"].ToString(),
                                   VendorName = dr["c_vname"].ToString(),
                                   Service = Convert.ToBoolean(dr["c_service"]),
                                   Repair = Convert.ToBoolean(dr["c_repair"]),
                                   ServiceDate = Convert.ToDateTime(dr["c_service_date"]),
                                   ServiceStatus = dr["c_service_status"].ToString(),
                                   VendorEmail = dr["c_email"].ToString(),
                                   VendorMobile = dr["c_mobile"].ToString(),
                                   Notes = dr["c_notes"].ToString(),
                                   SubCatId = Convert.ToInt32(dr["c_sub_id"])
                               }).ToList();


                conn.Close();
                return serviceList;
            }
            catch (Exception ex)
            {
                string operation = "Service Record";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return serviceList;
            }

        }

        internal void SendMail(string tableHtml, string vendorName)
        {
            throw new NotImplementedException();
        }

        public int GetInsertedServiceId()
        {
            int result = 0;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT MAX(c_service_id) FROM t_service", conn);
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ex)
            {
                string operation = "Service ID";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                conn.Close();
            }

            return result;
        }

        public bool InsertService(List<ModelService> listOfData)
        {
            string query = string.Empty;
            string message = string.Empty;

            int ServiceId = 0;

            query = "INSERT INTO t_servicedetails(c_service_id,c_sub_id,c_service,c_repair,c_notes) VALUES";

            try
            {
                OrderInsertService(listOfData[0]);
                ServiceId = GetInsertedServiceId();
                for (int i = 0; i < listOfData.Count; i++)
                {
                    if (i != 0)
                        query += ",";
                    query += "(" + ServiceId + "," + listOfData[i].SubCatId + "," + listOfData[i].Service + "," + listOfData[i].Repair + ",'" + listOfData[i].Notes + "')";
                }
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                List<ModelService> mailList = new List<ModelService>();

                if (listOfData.Count() > 0)
                {
                    mailList = (List<ModelService>)listOfData;
                    message = "You have recevied serviced order from";
                    var tableHtml = GenerateHTMLTable(mailList);
                    SendMail(tableHtml, mailList[0].VendorName, mailList[0].VendorEmail, mailList[0].VendorMobile, mailList[0].ServiceDate, message);
                    return true;

                }
                else
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {

                string operation = "Insert";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public int OrderInsertService(ModelService data)
        {
            NpgsqlCommand cmd = new NpgsqlCommand(@"insert into t_service (c_vid,c_service_date,c_payment_status,c_service_status,c_status) values
                                                                            (@VendorId,@ServiceDate,@Payment,@ServiceStatus,@status)", conn);
            cmd.Parameters.AddWithValue("@VendorId", data.VendorId);
            cmd.Parameters.AddWithValue("@ServiceDate", Convert.ToDateTime(data.ServiceDate));
            cmd.Parameters.AddWithValue("@Payment", "Pending");
            cmd.Parameters.AddWithValue("@ServiceStatus", "Active");
            cmd.Parameters.AddWithValue("@status", 1);

            conn.Open();
            int modified = cmd.ExecuteNonQuery();
            conn.Close();

            return modified;
        }

        public void SendMail(string tableHtml, string vandorName, string vendoremail, string vendorno, DateTime Odate, string message)
        {
            //string to = "jetpariyaparth@gmail.com";
            try
            {
                string to = usermail;
                string subject = "Order Recevied";

                string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\Email_Template\Service_Email.html";

                StreamReader str = new StreamReader(FilePath);
                string body = str.ReadToEnd();
                str.Close();
                body = body.Replace("#[{VendorName}]#", vandorName);
                body = body.Replace("#[{EmailVendor}]#", vendoremail);
                body = body.Replace("#[{NoVendor}]#", vendorno);
                body = body.Replace("#[{Odate}]#", Convert.ToDateTime(Odate).ToShortDateString());
                body = body.Replace("#[{Message}]#", message);
                body = body.Replace("<span class='sample_class' id='sample_id'></span>", tableHtml);

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
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }
        }

        public string GenerateHTMLTable(List<ModelService> mailList)
        {
            string tableHtml = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("FirstTable");
            dt.Columns.Add(new DataColumn("Items", typeof(string)));
            dt.Columns.Add(new DataColumn("Service", typeof(string)));
            dt.Columns.Add(new DataColumn("Repair", typeof(string)));
            dt.Columns.Add(new DataColumn("Notes", typeof(string)));

            for (int i = 0; i < mailList.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Items"] = mailList[i].SubCatName;
                dr["Service"] = mailList[i].Service ? "Yes" : "No";
                dr["Repair"] = mailList[i].Repair ? "Yes" : "No";
                dr["Notes"] = mailList[i].Notes;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);

            tableHtml += "<table  border='1' style='width:50%'>";
            tableHtml += "<tr><th>Items</th><th>Service</th><th>Repair</th><th>Notes</th></tr>";

            foreach (DataRow dr in dt.Rows)
            {
                tableHtml += "<tr><td style='text-align:center'>" + dr["Items"] + "</td><td>" + dr["Service"] + "</td><td>" + dr["Repair"] + "</td><td>" + dr["Notes"] + "</td></tr>";
            }
            tableHtml += "</table>";
            return tableHtml;
        }



        public int UpdateService(List<ModelService> listOfData)
        {
            string updatemessage = string.Empty;
            int updated = 0;
            try
            {
                UpdateServiceItems(listOfData[0]);
                NpgsqlCommand cmd = null;
                for (int i = 0; i < listOfData.Count(); i++)
                {
                    cmd = new NpgsqlCommand(@"UPDATE t_servicedetails
	                                            SET  c_service=@service , c_repair=@repair,c_notes=@notes
                                               	WHERE c_service_id=@id and c_sub_id = @subid;", conn);
                    cmd.Parameters.AddWithValue("@service", listOfData[i].Service);
                    cmd.Parameters.AddWithValue("@repair", listOfData[i].Repair);
                    cmd.Parameters.AddWithValue("@notes", listOfData[i].Notes);
                    cmd.Parameters.AddWithValue("@id", listOfData[i].ServiceId);
                    cmd.Parameters.AddWithValue("@subid", listOfData[i].SubCatId);

                    conn.Open();
                    updated = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                List<ModelService> mailList = new List<ModelService>();
                if (listOfData.Count() > 0)
                {
                    updatemessage = "You have recevied updated order from";
                    mailList = (List<ModelService>)listOfData;
                    var tableHtml = GenerateHTMLTable(mailList);
                    SendMail(tableHtml, mailList[0].VendorName, mailList[0].VendorEmail, mailList[0].VendorMobile, mailList[0].ServiceDate, updatemessage);
                    return updated;

                }
                else
                {
                    return updated;
                }
            }
            catch (Exception ex)
            {
                string operation = "Update";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;

            }
        }

        public bool UpdateServiceItems(ModelService data)
        {

            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE t_service SET  c_service_date=@date  WHERE c_service_id=@id", conn);
                cmd.Parameters.AddWithValue("@date", data.ServiceDate);
                cmd.Parameters.AddWithValue("@id", data.ServiceId);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
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
                string operation = "Update";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }



        public void DeleteService(int id)
        {
            try
            {
                NpgsqlCommand cm = new NpgsqlCommand(@"Update t_service set c_status=0 where c_service_id = @Id", conn);
                cm.Parameters.AddWithValue("@Id", id);
                conn.Open();
                cm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

                string operation = "Delete";
                string pagename = "Service";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }
        }


        public List<ModelService> ServiceSearch(string data)
        {
            List<ModelService> SearchList = new List<ModelService>();
            ModelSearchOrderService searchdata = new ModelSearchOrderService();
            searchdata.SearchService = data;
            try
            {
                NpgsqlCommand cm = new NpgsqlCommand(@"SELECT DISTINCT sub.c_sub_name,sub.c_sub_id 
                                                        FROM t_subcategory sub 
                                                        JOIN t_servicedetails details
                                                        ON  sub.c_sub_id=details.c_sub_id
                                                        where sub.c_sub_name LIKE '" + searchdata.SearchService + "%'", conn);
                //cm.Parameters.AddWithValue("@name", searchdata.SearchService);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                DataTable dt = new DataTable();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }

                SearchList = (from DataRow dr in dt.Rows
                              select new ModelService()
                              {
                                  SubCatName = dr["c_sub_name"].ToString(),
                                  SubCatId = Convert.ToInt32(dr["c_sub_id"]),
                              }).ToList();


                conn.Close();
                return SearchList;
            }
            catch (Exception ex)
            {

                string operation = "Search";
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return SearchList;
            }

        }


    }
}


