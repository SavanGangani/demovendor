using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class BALOrder : Helper
    {
        BALErrormanag er = new BALErrormanag();
        BALServiceAuthHelper helper = new BALServiceAuthHelper();
        public List<VendorDropDownModel> GetVendorName()
        {
            List<VendorDropDownModel> subList = new List<VendorDropDownModel>();
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand(@"select c_vid,c_vname,c_email,c_mobile
                                                      from t_vendormaster", conn);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }
                subList = (from DataRow dr in dt.Rows
                               //where int.Parse(dr["c_id"].ToString()) == c_id
                           select new VendorDropDownModel()
                           {
                               c_vid = Convert.ToInt32(dr["c_vid"]),
                               c_vname = dr["c_vname"].ToString(),

                           }).ToList();
                conn.Close();
                return subList;

            }
            catch (Exception ex)
            {
                string operation = "Get";
                string pagename = "Venodr";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return subList;

            }
        }

        public List<VendorDropDownModel> GetVendorData(int id)
        {
            List<VendorDropDownModel> subList = new List<VendorDropDownModel>();
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand(@"select c_email,c_mobile
                                                      from t_vendormaster where c_vid=@Id", conn);
                cm.Parameters.AddWithValue("@Id", id);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }
                subList = (from DataRow dr in dt.Rows
                           select new VendorDropDownModel()
                           {
                               VendorEmail = dr["c_email"].ToString(),
                               VendorMobile = dr["c_mobile"].ToString()

                           }).ToList();
                conn.Close();
                return subList;

            }
            catch (Exception ex)
            {
                string operation = "Get";
                string pagename = "Venodr";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return subList;
            }
        }

        public List<ModelOrder> GetSubCategory(int id)
        {
            List<ModelOrder> subCatList = new List<ModelOrder>();

            try
            {
                NpgsqlCommand cm = new NpgsqlCommand(@"SELECT subcat.c_sub_name,vendor.c_vid,vendor.c_vname,vendor.c_email,vendor.c_mobile,details.c_qauntity,orders.c_order_date,
                                                        orders.c_payment_status,orders.c_order_status,orders.c_last_payment_date,details.c_orderitemid,
                                                        details.c_sub_id,orders.c_order_id
                                                        FROM t_order orders
                                                        JOIN t_vendormaster vendor
                                                        ON  orders.c_vid = vendor.c_vid
                                                        JOIN  t_orderdetails details
                                                        ON  orders.c_order_id = details.c_order_id
                                                        JOIN t_subcategory subcat
                                                        ON subcat.c_sub_id = details.c_sub_id  WHERE orders.c_order_id=@Id", conn);
                cm.Parameters.AddWithValue("@Id", id);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                DataTable dt = new DataTable();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }

                subCatList = (from DataRow dr in dt.Rows
                              select new ModelOrder()
                              {
                                  OrderId = Convert.ToInt32(dr["c_order_id"]),
                                  SubCatName = dr["c_sub_name"].ToString(),
                                  VendorName = dr["c_vname"].ToString(),
                                  Quantity = Convert.ToInt32(dr["c_qauntity"]),
                                  OrderDate = Convert.ToDateTime(dr["c_order_date"]),
                                  OrderType = dr["c_order_status"].ToString(),
                                  VendorEmail = dr["c_email"].ToString(),
                                  VendorNo = dr["c_mobile"].ToString(),
                                  SubCatId = Convert.ToInt32(dr["c_sub_id"]),

                              }).ToList();


                conn.Close();
                return subCatList;
            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Venodr";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return subCatList;
            }

        }

        public string ArrToString(int[] data)
        {
            string id_string = string.Empty;


            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    id_string += data[i];
                }
                else
                {
                    id_string += "," + data[i];
                }
            }

            return id_string;
        }

        public int GetInsertedOrderId()
        {
            int result = 0;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT MAX(c_order_id) FROM t_order", conn);
                conn.Open();
                result = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
            }

            return result;
        }



        public bool InsertOrder(List<ModelOrder> listOfData)
        {
            string query = string.Empty;
            string message = string.Empty;
            int OrderID = 0;
            query = "INSERT INTO t_orderdetails(c_order_id,c_sub_id,c_vid,c_qauntity) VALUES";
            try
            {
                OrderInsert(listOfData[0]);
                OrderID = GetInsertedOrderId();
                for (int i = 0; i < listOfData.Count; i++)
                {
                    if (i != 0)
                        query += ",";
                    query += "(" + OrderID + "," + listOfData[i].SubCatId + "," + listOfData[i].VendorId + "," + listOfData[i].Quantity + ")";
                }
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                List<ModelOrder> mailList = new List<ModelOrder>();

                if (listOfData.Count() > 0)
                {
                    mailList = (List<ModelOrder>)listOfData;
                    var tableHtml = GenerateHTMLTable(mailList);
                    message = "You have recevied an order from";
                    helper.SendMail(tableHtml, mailList[0].VendorName, mailList[0].VendorEmail, mailList[0].VendorNo, mailList[0].OrderDate, message);
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
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public List<ModelOrder> GetAllOrder()
        {
            List<ModelOrder> OrderList = new List<ModelOrder>();
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand cm = new NpgsqlCommand(@"SELECT vendor.c_vid,vendor.c_vname,orders.c_order_date,orders.c_payment_status,orders.c_order_status,orders.c_last_payment_date,orders.c_order_id
                                                        FROM t_order orders 
                                                        JOIN t_vendormaster vendor  
                                                        ON  orders.c_vid=vendor.c_vid
                                                        WHERE orders.c_status=1", conn);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();

                if (datar.HasRows)
                {
                    dt.Load(datar);

                    OrderList = (from DataRow dr in dt.Rows
                                 select new ModelOrder()
                                 {
                                     OrderId = Convert.ToInt32(dr["c_order_id"]),
                                     VendorName = dr["c_vname"].ToString(),
                                     PaymentStatus = dr["c_payment_status"].ToString(),
                                     OrderDate = Convert.ToDateTime(dr["c_order_date"]),
                                     LastPaymentDate = Convert.ToDateTime(dr["c_last_payment_date"]),
                                     OrderType = dr["c_order_status"].ToString()
                                 }).ToList();
                }

                conn.Close();
                return OrderList;

            }
            catch (Exception ex)
            {

                string operation = "Select ALL";
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return OrderList;
            }

        }

        public bool OrderInsert(ModelOrder data)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"insert into t_order (c_vid,c_order_type,c_order_date,c_payment_status,c_order_status,c_last_payment_date,c_status) values
                                                                    (@VendorId,@OrderType,@OrderDate,@Payment,@OrderStatus,@lastpaydate,@status)", conn);
                cmd.Parameters.AddWithValue("@OrderType", "delevered");
                cmd.Parameters.AddWithValue("@OrderDate", Convert.ToDateTime(data.OrderDate));
                cmd.Parameters.AddWithValue("@Payment", "Pendding");
                cmd.Parameters.AddWithValue("@OrderStatus", "Active");
                cmd.Parameters.AddWithValue("@lastpaydate", Convert.ToDateTime(data.OrderDate.AddDays(5)));
                cmd.Parameters.AddWithValue("@VendorId", data.VendorId);
                cmd.Parameters.AddWithValue("@status", 1);


                conn.Open();
                int id = cmd.ExecuteNonQuery();
                conn.Close();

                if (id > 0)
                    return true;
                else
                    return false;
                return true;
            }
            catch (Exception ex)
            {

                string operation = "Insert";
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }



        public string GenerateHTMLTable(List<ModelOrder> mailList)
        {
            string tableHtml = "";
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("FirstTable");
            dt.Columns.Add(new DataColumn("Items", typeof(string)));
            dt.Columns.Add(new DataColumn("Qauntity", typeof(int)));

            for (int i = 0; i < mailList.Count(); i++)
            {
                DataRow dr = dt.NewRow();
                dr["Items"] = mailList[i].SubCatName;
                dr["Qauntity"] = mailList[i].Quantity;
                dt.Rows.Add(dr);
            }
            ds.Tables.Add(dt);

            tableHtml += "<table>";
            tableHtml += "<tr><th>Items</th>&nbsp;&nbsp;&nbsp;&nbsp;<th>Qauntity</th></tr>";

            foreach (DataRow dr in dt.Rows)
            {
                tableHtml += "<tr><td>" + dr["Items"] + "</td>&nbsp;&nbsp;&nbsp;&nbsp;<td style='text-align:end'>" + dr["Qauntity"] + "</td></tr>";
            }
            tableHtml += "</table>";
            return tableHtml;
        }

        public int UpdateOrder(List<ModelOrder> ListOfdata)
        {
            string message = string.Empty;
            try
            {
                UpdateOrderItems(ListOfdata[0]);
                NpgsqlCommand cmd = null;
                int modified = 0;
                for (int i = 0; i < ListOfdata.Count; i++)
                {
                    cmd = new NpgsqlCommand(@"UPDATE t_orderdetails
	                                            SET  c_qauntity=@quantity
                                               	WHERE c_order_id=@id and c_sub_id = @subid;", conn);
                    cmd.Parameters.AddWithValue("@quantity", ListOfdata[i].Quantity);
                    cmd.Parameters.AddWithValue("@id", ListOfdata[i].OrderId);
                    cmd.Parameters.AddWithValue("@subid", ListOfdata[i].SubCatId);
                    conn.Open();
                    modified = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                if (modified > 0)
                {

                    return 1;
                }
                else
                {
                    return 0;
                }

                List<ModelOrder> mailList = new List<ModelOrder>();
                if (ListOfdata.Count() > 0)
                {
                    mailList = (List<ModelOrder>)ListOfdata;
                    var tableHtml = GenerateHTMLTable(mailList);
                    message = "You have recevied updated order from";
                    helper.SendMail(tableHtml, mailList[0].VendorName, mailList[0].VendorEmail, mailList[0].VendorNo, mailList[0].OrderDate, message);
                    return 1;

                }
                else
                {
                    conn.Close();
                    return 0;
                }

            }
            catch (Exception ex)
            {
                string operation = "Update";
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }

        public int UpdateOrderItems(ModelOrder data)
        {

            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE public.t_order
	                                            SET  c_order_date=@orderdate
                                               	WHERE c_order_id=@id;", conn);
                cmd.Parameters.AddWithValue("@orderdate", data.OrderDate);
                cmd.Parameters.AddWithValue("@id", data.OrderId);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    conn.Close();

                    return 1;
                }
                else
                {
                    conn.Close();

                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Update";
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }
        public bool DeleteOrder(int id)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE public.t_order
	                                                SET  c_status='0'
	                                                WHERE c_order_id=@id;", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                int status = cmd.ExecuteNonQuery();

                if (status > 0)
                    return true;
                else
                    return false;
                conn.Close();
            }
            catch (Exception ex)
            {

                string operation = "Delete";
                string pagename = "Order";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public List<ModelOrder> OrderSearch(string data)
        {

            List<ModelOrder> SearchList = new List<ModelOrder>();
            ModelSearchOrderService searchdata = new ModelSearchOrderService();
            searchdata.SearchName = data;
            try
            {
                NpgsqlCommand cm = new NpgsqlCommand(@"SELECT DISTINCT sub.c_sub_name,sub.c_sub_id 
                                                        FROM t_subcategory sub 
                                                        JOIN t_orderdetails details
                                                        ON  sub.c_sub_id=details.c_sub_id
                                                        where sub.c_sub_name LIKE '" + searchdata.SearchName + "%'", conn);
                //cm.Parameters.AddWithValue("@name", searchdata.SearchName);
                conn.Open();
                NpgsqlDataReader datar = cm.ExecuteReader();
                DataTable dt = new DataTable();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }

                SearchList = (from DataRow dr in dt.Rows
                              select new ModelOrder()
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


