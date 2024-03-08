using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.AuthHelper
{
    public class TransactionHelper : Helper
    {
        public List<Transaction> GetAll()
        {
            NpgsqlCommand com = new NpgsqlCommand(@"SELECT DISTINCT t1.c_transaction_id, t1.c_order_id, t2.c_vname, t1.c_transaction_date, t1.c_delivery_status, t1.c_total_price FROM t_transaction t1 FULL JOIN t_vendormaster t2 ON t1.c_vid=t2.c_vid INNER JOIN t_orderdetails t3 ON t1.c_order_id=t3.c_order_id WHERE t1.c_vid=t2.c_vid AND t1.c_isdeleted=false", conn);
            DataTable dt = new DataTable();
            conn.Open();
            NpgsqlDataReader datar = com.ExecuteReader();
            List<Transaction> Tran = new List<Transaction>();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            Tran = (from DataRow dr in dt.Rows
                    select new Transaction()
                    {
                        TransactionId = Convert.ToInt32(dr["c_transaction_id"]),
                        orderId = Convert.ToInt32(dr["c_order_id"]),
                        vname = dr["c_vname"].ToString(),
                        transactionDate = Convert.ToDateTime(dr["c_transaction_date"]),
                        delivaryStatus = dr["c_delivery_status"].ToString(),
                        TotalPrice = Convert.ToInt32(dr["c_total_price"]),
                    }).ToList();
            conn.Close();
            return Tran;
        }
        public int InsertService(Transaction data)
        {
            try
            {
                int getId = 0;
                NpgsqlCommand cmd2 = new NpgsqlCommand(@"SELECT c_vid FROM t_vendormaster WHERE c_vname=@vname;", conn);
                cmd2.Parameters.AddWithValue("@vname", data.vname);
                conn.Open();
                NpgsqlDataReader datar = cmd2.ExecuteReader();
                if (datar.Read())
                {
                    getId = (Convert.ToInt32(datar["c_vid"]));
                }

                conn.Close();

                NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO public.t_servicetransaction(
	                                                c_vid, c_transaction_date, c_delivery_status, c_total_price, c_isdeleted, c_service_id, c_isinvoicesent)
	                                                VALUES ( @vid, @transaction_date, @delivery_status, @total_price, @isdeleted, @c_service_id, @invoicesent);", conn);

                cmd.Parameters.AddWithValue("@c_service_id", data.serviceId);
                cmd.Parameters.AddWithValue("@vid", getId);
                cmd.Parameters.AddWithValue("@transaction_date", data.transactionDate);
                cmd.Parameters.AddWithValue("@delivery_status", data.deliveryStatus);
                cmd.Parameters.AddWithValue("@total_price", data.TotalPrice);
                cmd.Parameters.AddWithValue("@isdeleted", false);
                cmd.Parameters.AddWithValue("@invoicesent", false);
                conn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    //NpgsqlCommand cmd3 = new NpgsqlCommand(@"SELECT c_transaction_id FROM t_transaction WHERE c_order_id=@order_id AND c_vid=@vid;", conn);
                    //cmd3.Parameters.AddWithValue("@order_id", data.orderId);
                    //cmd3.Parameters.AddWithValue("@vid", getId);
                    //NpgsqlDataReader dr = cmd3.ExecuteReader();
                    //if (dr.Read())
                    //{
                    //    int Tid = (Convert.ToInt32(datar["c_transaction_id"]));
                    //    conn.Close();
                    //    return Tid;
                    //}
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
                throw ex;
            }
        }

        public List<Transaction> GetAllService()
        {
            NpgsqlCommand com = new NpgsqlCommand(@"SELECT DISTINCT t1.c_transaction_id, t1.c_order_id, t2.c_vname, t1.c_transaction_date, t1.c_delivery_status, t1.c_total_price FROM t_transaction t1 FULL JOIN t_vendormaster t2 ON t1.c_vid=t2.c_vid INNER JOIN t_orderdetails t3 ON t1.c_order_id=t3.c_order_id WHERE t1.c_vid=t2.c_vid AND t1.c_isdeleted=false;", conn);
            DataTable dt = new DataTable();
            conn.Open();
            NpgsqlDataReader datar = com.ExecuteReader();
            List<Transaction> Tran = new List<Transaction>();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            Tran = (from DataRow dr in dt.Rows
                    select new Transaction()
                    {
                        TransactionId = Convert.ToInt32(dr["c_transaction_id"]),
                        orderId = Convert.ToInt32(dr["c_order_id"]),
                        vname = dr["c_vname"].ToString(),
                        transactionDate = Convert.ToDateTime(dr["c_transaction_date"]),
                        deliveryStatus = dr["c_delivery_status"].ToString(),
                        TotalPrice = Convert.ToInt32(dr["c_total_price"]),
                    }).ToList();
            conn.Close();
            return Tran;
        }

        public List<Transaction> GetAllServiceTrasaction()
        {
            NpgsqlCommand com = new NpgsqlCommand(@"SELECT DISTINCT t1.c_transaction_id,t1.c_service_id , t2.c_vname, t1.c_transaction_date, t1.c_delivery_status, t1.c_total_price FROM t_servicetransaction t1 INNER JOIN t_vendormaster t2 ON t1.c_vid=t2.c_vid;", conn);
            DataTable dt = new DataTable();
            conn.Open();
            NpgsqlDataReader datar = com.ExecuteReader();
            List<Transaction> Tran = new List<Transaction>();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            Tran = (from DataRow dr in dt.Rows
                    select new Transaction()
                    {
                        TransactionId = Convert.ToInt32(dr["c_transaction_id"]),
                        serviceId = Convert.ToInt32(dr["c_service_id"]),
                        vname = dr["c_vname"].ToString(),
                        transactionDate = Convert.ToDateTime(dr["c_transaction_date"]),
                        deliveryStatus = dr["c_delivery_status"].ToString(),
                        TotalPrice = Convert.ToInt32(dr["c_total_price"]),
                    }).ToList();
            conn.Close();
            return Tran;
        }

        public List<Transaction> GetViewDetails(int id)
        {
            List<Transaction> ViewDetails = new List<Transaction>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"SELECT subcat.c_sub_name,vendor.c_vid,vendor.c_vname,vendor.c_email,vendor.c_mobile,details.c_qauntity,
                                                        orders.c_order_date,qt.c_receiveditem,trans.c_total_price,trans.c_transaction_id,
                                                        details.c_orderitemid,
                                                        details.c_sub_id,orders.c_order_id
                                                        FROM t_order orders
                                                        JOIN t_vendormaster vendor
                                                        ON  orders.c_vid = vendor.c_vid
                                                        JOIN  t_orderdetails details
                                                        ON  orders.c_order_id = details.c_order_id
                                                        JOIN t_subcategory subcat
                                                        ON subcat.c_sub_id = details.c_sub_id 
														JOIN t_transaction trans
														ON  orders.c_order_id=trans.c_order_id
														JOIN t_orderqty qt
														ON trans.c_transaction_id=qt.c_transaction_id 
                                                        WHERE trans.c_transaction_id=@id", conn);
                com.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                conn.Open();
                NpgsqlDataReader datar = com.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }

                ViewDetails = (from DataRow dr in dt.Rows
                               select new Transaction()
                               {
                                   TransactionId = Convert.ToInt32(dr["c_transaction_id"]),
                                   subCategory = dr["c_sub_name"].ToString(),
                                   vname = dr["c_vname"].ToString(),
                                   VendorEmail = dr["c_email"].ToString(),
                                   VendorMobileNo = dr["c_mobile"].ToString(),
                                   itemQty = Convert.ToInt32(dr["c_qauntity"]),
                                   ReceviedQauntity = Convert.ToInt32(dr["c_receiveditem"]),
                                   Orderdate = Convert.ToDateTime(dr["c_order_date"])

                               }).ToList();
                conn.Close();
                return ViewDetails;

            }
            catch (Exception ex)
            {

                return ViewDetails;
            }
        }

        public List<Transaction> GetOneTransaction(int id)
        {
            try
            {

                NpgsqlCommand com = new NpgsqlCommand(@"SELECT t1.c_sub_name,orders.c_order_date,t2.c_qauntity,orderqt.c_receiveditem,
                                                    trans.c_transaction_date,trans.c_transaction_id,orders.c_order_id,trans.c_delivery_status,
                                                    trans.c_total_price
                                                    FROM t_subcategory t1 
                                                    JOIN t_orderdetails t2 
                                                    ON t1.c_sub_id=t2.c_sub_id 
                                                    JOIN t_order orders
                                                    ON orders.c_order_id=t2.c_order_id 
													JOIN t_orderqty orderqt
													ON  orderqt.c_order_id=orders.c_order_id
													JOIN t_transaction trans
													ON trans.c_transaction_id=orderqt.c_transaction_id
													WHERE trans.c_transaction_id=@id", conn);
                com.Parameters.AddWithValue("@id", id);
                DataTable dt = new DataTable();
                conn.Open();
                NpgsqlDataReader datar = com.ExecuteReader();
                List<Transaction> TranOrder = new List<Transaction>();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }

                TranOrder = (from DataRow dr in dt.Rows
                             select new Transaction()
                             {
                                 TransactionId = Convert.ToInt32(dr["c_transaction_id"]),
                                 orderId = Convert.ToInt32(dr["c_order_id"]),
                                 itemQty = Convert.ToInt32(dr["c_qauntity"]),
                                 ReceviedQauntity = Convert.ToInt32(dr["c_receiveditem"]),
                                 subCategory = dr["c_sub_name"].ToString(),
                                 transactionDate = Convert.ToDateTime(dr["c_transaction_date"]),
                                 delivaryStatus = dr["c_delivery_status"].ToString(),
                                 TotalPrice = Convert.ToInt32(dr["c_total_price"]),
                             }).ToList();
                conn.Close();
                return TranOrder;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<Transaction> GetOneTransactionService(int id)
        {
            NpgsqlCommand com = new NpgsqlCommand(@"SELECT subcat.c_sub_name,vendor.c_vid,vendor.c_vname,vendor.c_email,vendor.c_mobile,details.c_service,details.c_repair,service.c_service_date,
                                                        service.c_payment_status,service.c_service_status,
														details.c_notes,details.t_sitemid,
                                                        trans.c_transaction_id,trans.c_transaction_date,
                                                        trans.c_transaction_date,trans.c_delivery_status,
                                                        details.c_sub_id,service.c_service_id
                                                        FROM t_service service
                                                        JOIN t_vendormaster vendor
                                                        ON  service.c_vid = vendor.c_vid
                                                        JOIN  t_servicedetails details
                                                        ON  service.c_service_id = details.c_service_id
                                                        JOIN t_subcategory subcat
                                                        ON subcat.c_sub_id = details.c_sub_id 
                                                        JOIN t_servicetransaction trans 
                                                        ON service=c_service_id=trans.c_service_id WHERE trans.c_transaction_id=@Id", conn);
            com.Parameters.AddWithValue("@id", id);
            DataTable dt = new DataTable();
            conn.Open();
            NpgsqlDataReader datar = com.ExecuteReader();
            List<Transaction> Tran = new List<Transaction>();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            Tran = (from DataRow dr in dt.Rows
                    select new Transaction()
                    {
                        TransactionId = Convert.ToInt32(dr["c_transaction_id"]),
                        serviceId = Convert.ToInt32(dr["c_service_id"]),
                        Service = dr["c_service"].ToString(),
                        Repair = dr["c_repair"].ToString(),
                        Notes = dr["c_notes"].ToString(),
                        vname = dr["c_vname"].ToString(),
                        transactionDate = Convert.ToDateTime(dr["c_transaction_date"]),
                        delivaryStatus = dr["c_delivery_status"].ToString(),
                        TotalPrice = Convert.ToInt32(dr["c_total_price"]),
                    }).ToList();
            conn.Close();
            return Tran;
        }




        public List<string> getAllOrderVendorName()
        {
            List<string> res = new List<string>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"select DISTINCT v.c_vname from t_order o,t_vendormaster v where o.c_vid = v.c_vid", conn);
            conn.Open();

            NpgsqlDataReader datar = cmd.ExecuteReader();

            while (datar.Read())
            {
                res.Add(datar["c_vname"].ToString());
            }

            conn.Close();

            return res;

        }
        public List<string> getAllServiceVendorName()
        {
            List<string> res = new List<string>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT DISTINCT t1.c_vname from t_vendormaster t1 INNER JOIN t_service t2 ON t1.c_vid=t2.c_vid INNER JOIN t_servicedetails t3 ON t2.c_service_id=t3.c_service_id;", conn);
            conn.Open();

            NpgsqlDataReader datar = cmd.ExecuteReader();

            while (datar.Read())
            {
                res.Add(datar["c_vname"].ToString());
            }

            conn.Close();

            return res;

        }
        public List<string> getAllVendorName(Transaction data)
        {
            if (data.transactionType == "Order")
            {
                return getAllOrderVendorName();
            }
            else
            {
                return getAllServiceVendorName();
            }
        }
        public List<int> getOrderID(Transaction data)
        {
            List<int> res = new List<int>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT DISTINCT c_order_id FROM t_orderdetails WHERE c_vid=(SELECT c_vid FROM t_vendormaster WHERE c_vname=@vname) ORDER BY c_order_id ASC", conn);
            cmd.Parameters.AddWithValue("@vname", data.vname);
            conn.Open();

            NpgsqlDataReader datar = cmd.ExecuteReader();

            while (datar.Read())
            {
                res.Add(Convert.ToInt32(datar["c_order_id"]));
            }

            conn.Close();

            return res;
        }
        public List<int> getServiceID(Transaction data)
        {
            List<int> res = new List<int>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT c_order_id FROM t_order WHERE c_vid=(SELECT c_vid FROM t_vendormaster WHERE c_vname=@vname) ORDER BY c_order_id ASC", conn);
            cmd.Parameters.AddWithValue("@vname", data.vname);
            conn.Open();

            NpgsqlDataReader datar = cmd.ExecuteReader();

            while (datar.Read())
            {
                res.Add(Convert.ToInt32(datar["c_order_id"]));
            }

            conn.Close();

            return res;
        }
        public List<int> getAllId(Transaction data)
        {
            if (data.transactionType == "Order")
            {
                return getOrderID(data);
            }
            else
            {
                return getServiceID(data);
            }
        }
        public List<Transaction> getOrderItemName(Transaction data)
        {
            List<Transaction> subCatTransList = new List<Transaction>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT t1.c_sub_name,orders.c_order_date,t2.c_qauntity
                                                    FROM t_subcategory t1 
                                                    JOIN t_orderdetails t2 
                                                    ON t1.c_sub_id=t2.c_sub_id 
                                                    JOIN t_order orders
                                                    ON orders.c_order_id=t2.c_order_id WHERE t2.c_order_id=@id;", conn);
            cmd.Parameters.AddWithValue("@id", data.orderId);
            conn.Open();
            NpgsqlDataReader datar = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }

            subCatTransList = (from DataRow dr in dt.Rows
                               select new Transaction()
                               {
                                   subCategory = dr["c_sub_name"].ToString(),
                                   itemQty = Convert.ToInt32(dr["c_qauntity"]),

                               }).ToList();


            conn.Close();
            return subCatTransList;
        }


        public List<string> getServiceName(Transaction data)
        {
            List<string> res = new List<string>();
            NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT t1.c_sub_name FROM t_subcategory t1 INNER JOIN t_orderdetails t2 ON t1.c_sub_id=t2.c_sub_id WHERE t2.c_order_id=@id;", conn);
            cmd.Parameters.AddWithValue("@id", data.orderId);
            conn.Open();

            NpgsqlDataReader datar = cmd.ExecuteReader();

            while (datar.Read())
            {
                res.Add(datar["c_order_id"].ToString());
            }

            conn.Close();

            return res;
        }

        public int GetInsertedOrderIdTrans()
        {
            int result = 0;
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT MAX(c_transaction_id) FROM t_transaction", conn);
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
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
        

        public bool InsertOrder(List<Transaction> data)
        {

            int getId = 0;
            string query = string.Empty;
            query = "INSERT INTO t_orderqty(c_receiveditem,c_transaction_id,c_order_id) VALUES";
            try
            {
                OrderInsertTransaction(data[0]);
                getId = GetInsertedOrderIdTrans();
                for (int i = 0; i < data.Count; i++)
                {
                    if (i != 0)
                        query += ",";
                    query += "(" + data[i].ReceviedQauntity + "," + getId + "," + data[i].orderId + ")";
                }
                NpgsqlCommand cmd = new NpgsqlCommand(query, conn);
                conn.Open();
                int Inserted = cmd.ExecuteNonQuery();
                if (Inserted > 0)
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
                return false;
            }
        }

        public bool OrderInsertTransaction(Transaction data)
        {
            try
            {
                NpgsqlCommand cmd1 = new NpgsqlCommand(@"SELECT c_vid from t_vendormaster WHERE c_vname='" + data.vname + "'", conn);
                conn.Open();
                NpgsqlDataReader reader = cmd1.ExecuteReader();
                int vendorid = 0;
                if (reader.Read())
                {
                    vendorid = Convert.ToInt32(reader["c_vid"]);
                }
                conn.Close();
                NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO t_transaction (c_order_id,c_vid,c_transaction_date,c_delivery_status,c_total_price,c_isdeleted,c_isinvoicesent) 
                                                        VALUES(@OrderId,@VendorId,@TransactioDate,@DeliveryStatus,@TotalPrice,@isdel,@isinvoicesent)", conn);
                cmd.Parameters.AddWithValue("@OrderId", data.orderId);
                cmd.Parameters.AddWithValue("@VendorId", vendorid);
                cmd.Parameters.AddWithValue("@TransactioDate", Convert.ToDateTime(data.transactionDate));
                cmd.Parameters.AddWithValue("@DeliveryStatus", data.deliveryStatus);
                cmd.Parameters.AddWithValue("@TotalPrice", data.TotalPrice);
                cmd.Parameters.AddWithValue("@isdel", false);
                cmd.Parameters.AddWithValue("@isinvoicesent", false);


                conn.Open();
                int id = cmd.ExecuteNonQuery();

                if (id > 0)
                {
                    conn.Close();
                    return true;

                }
                else
                {
                    conn.Close();

                    return false;

                }

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }

        //public int UpdateServiceTransaction(List<Transaction> ListOfdata)
        //{
        //    try
        //    {
        //        UpdateServiceTrans(ListOfdata[0]);
        //        NpgsqlCommand cmd = null;
        //        for (int i = 0; i < ListOfdata.Count(); i++)
        //        {
        //            cmd = new NpgsqlCommand(@"UPDATE t_orderqt
        //                                     SET  c_receiveditem=@item
        //                                       	WHERE c_transaction_id=@id;", conn);
        //            cmd.Parameters.AddWithValue("@@item", ListOfdata[i].ReceviedQauntity);
        //            cmd.Parameters.AddWithValue("@id", ListOfdata[i].TransactionId);


        //            conn.Open();
        //            int updated = cmd.ExecuteNonQuery();
        //            if (updated > 0)
        //            {
        //                return 1;
        //            }
        //            else
        //            {
        //                return 0;
        //            }
        //        }
        //        return 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        //string operation = "Update";
        //        //string pagename = "Service";

        //        return 0;

        //    }
        //}

        //public bool UpdateServiceTrans(Transaction data)
        //{

        //    try
        //    {

        //        NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE t_servicetransaction SET  
        //                                              c_transaction_date=@date, c_delivery_status=@status,c_total_price=@price WHERE c_transaction_id=@id", conn);
        //        cmd.Parameters.AddWithValue("@date", data.transactionDate);
        //        cmd.Parameters.AddWithValue("@status", data.deliveryStatus);
        //        cmd.Parameters.AddWithValue("@price", data.TotalPrice);
        //        cmd.Parameters.AddWithValue("@id", data.TransactionId);
        //        conn.Open();
        //        int i = cmd.ExecuteNonQuery();
        //        if (i > 0)
        //        {
        //            conn.Close();

        //            return true;
        //        }
        //        else
        //        {
        //            conn.Close();

        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        return false;
        //    }
        //}

        public void Delete(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE t_transaction SET c_isdeleted = @c_isdeleted WHERE c_transaction_id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@c_isdeleted", true);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

        }

        //public int InsertService(Transaction data)
        //{
        //    try
        //    {
        //        int getId = 0;
        //        NpgsqlCommand cmd2 = new NpgsqlCommand(@"SELECT c_vid FROM t_vendormaster WHERE c_vname=@vname;", conn);
        //        cmd2.Parameters.AddWithValue("@vname", data.vname);
        //        conn.Open();
        //        NpgsqlDataReader datar = cmd2.ExecuteReader();
        //        if (datar.Read())
        //        {
        //            getId = (Convert.ToInt32(datar["c_vid"]));
        //        }

        //        conn.Close();

        //        NpgsqlCommand cmd = new NpgsqlCommand(@"INSERT INTO public.t_servicetransaction(
	       //                                         c_vid, c_transaction_date, c_delivery_status, c_total_price, c_isdeleted, c_service_id, c_isinvoicesent)
	       //                                         VALUES ( @vid, @transaction_date, @delivery_status, @total_price, @isdeleted, @c_service_id, @invoicesent);", conn);

        //        cmd.Parameters.AddWithValue("@c_service_id", data.serviceId);
        //        cmd.Parameters.AddWithValue("@vid", getId);
        //        cmd.Parameters.AddWithValue("@transaction_date", data.transactionDate);
        //        cmd.Parameters.AddWithValue("@delivery_status", data.deliveryStatus);
        //        cmd.Parameters.AddWithValue("@total_price", data.TotalPrice);
        //        cmd.Parameters.AddWithValue("@isdeleted", false);
        //        cmd.Parameters.AddWithValue("@invoicesent", false);
        //        conn.Open();
        //        int i = cmd.ExecuteNonQuery();
        //        if (i > 0)
        //        {

        //            conn.Close();
        //            return 1;
        //        }
        //        else
        //        {
        //            conn.Close();
        //            return 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<ModelService> GetTransactionServiceData(int id)
        {
            List<ModelService> ServiceList = new List<ModelService>();

            DataTable dt = new DataTable();
            NpgsqlCommand cm = new NpgsqlCommand(@"SELECT DISTINCT t2.t_sitemid,t1.c_sub_name,t2.c_service,t2.c_repair FROM t_subcategory t1  JOIN t_servicedetails t2 ON t1.c_sub_id=t2.c_sub_id WHERE t2.c_service_id=@id;", conn);
            cm.Parameters.AddWithValue("@id",id);
            conn.Open();
            NpgsqlDataReader datar = cm.ExecuteReader();

            if (datar.HasRows)
            {
                dt.Load(datar);

                ServiceList = (from DataRow dr in dt.Rows
                               select new ModelService()
                               {
                                   //ServiceId = Convert.ToInt32(dr["c_service_id"]),
                                   SubCatName = dr["c_sub_name"].ToString(),
                                   Service = ((bool)dr["c_service"]),
                                   Repair = ((bool)dr["c_repair"]),
                               }).ToList();
            }

            conn.Close();
            return ServiceList;
            
        }

        public bool UpdateTransOrder(Transaction value)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"update t_order set c_payment_status = @PaymentStatus  ,c_order_type =@delivaryStatus where c_order_id = @orderId", conn);


                cmd.Parameters.AddWithValue("@delivaryStatus", value.delivaryStatus);
                cmd.Parameters.AddWithValue("@PaymentStatus", value.PaymentStatus);
                cmd.Parameters.AddWithValue("@orderId", value.orderId);


                conn.Open();
                int id = cmd.ExecuteNonQuery();
                conn.Close();

                if (id > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateTransService(Transaction value)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"update t_service set c_payment_status = @PaymentStatus  ,c_service_type =@delivaryStatus where c_service_id = @serviceId", conn);


                cmd.Parameters.AddWithValue("@delivaryStatus", value.delivaryStatus);
                cmd.Parameters.AddWithValue("@PaymentStatus", value.PaymentStatus);
                cmd.Parameters.AddWithValue("@serviceId", value.orderId);


                conn.Open();
                int id = cmd.ExecuteNonQuery();
                conn.Close();

                if (id > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
