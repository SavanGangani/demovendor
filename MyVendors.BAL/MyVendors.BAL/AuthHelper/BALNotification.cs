//using MyVendors.BAL.CacheLayer;
using MyVendors.BAL.Models;
//using MyVendors.BAL.RabbitMqfile;
//using MyVendors.BAL.RabbitMqfile;
using Npgsql;
using RabbitMQ.Client;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class BALNotification : Helper
    {
        BALErrormanag er = new BALErrormanag();

        // Added By : Dhrupti Thesiya, Descritption: logic for send and recive notification using Redis and rabbitmq
        #region
        //private readonly NotificationKeyValue _cacheNotificationStack;
        //public BALNotification()
        //{
        //    _cacheNotificationStack = new NotificationKeyValue();
        //}
        #endregion
        // add function for notification register


        #region
        public List<Modelnotification> RegisterNotification()
        {
            List<Modelnotification> notificationList = new List<Modelnotification>();
            try
            {
                // List<string> MessageList = new List<string>();


                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT o.c_order_date,o.c_payment_status,v.c_vid,v.c_vname
                                                        FROM t_order o,t_vendormaster v  WHERE o.c_vid=v.c_vid
                                                        AND o.c_payment_status='Pending' AND o.c_last_payment_date <'" + DateTime.Now.ToString("yyyy-MM-dd") + "'", conn);
                conn.Open();
                NpgsqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                notificationList = (from DataRow row in dt.Rows
                                    select new Modelnotification()
                                    {

                                        VendoreName = row["c_vname"].ToString(),
                                        Date = Convert.ToDateTime(row["c_order_date"]).Date
                                    }).ToList();


                // sendmsg(notificationList);


                //MessageList = receive(notificationList.Count);

                // SetCounter(notificationList.Count);
                conn.Close();
                return notificationList;


            }
            catch (Exception ex)
            {

                string operation = "Select";
                string pagename = "Notification";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return notificationList;
            }

        }
        #endregion


        #region
        public int GetCounter()
        {
            int counter;
            List<Modelnotification> countdata = new List<Modelnotification>();
            try
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT o.c_order_id,o.c_order_date,o.c_payment_status,v.c_vid,v.c_vname 
                                                        FROM t_order o,t_vendormaster v WHERE o.c_vid=v.c_vid
                                                        AND o.c_payment_status='Pendding' AND o.c_last_payment_date <'" + DateTime.Now.ToString("yyyy-MM-dd") + "'", conn);

                NpgsqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    dt.Load(dr);
                }
                countdata = (from DataRow row in dt.Rows
                             select new Modelnotification()
                             {

                                 VendoreName = row["c_vname"].ToString(),

                             }).ToList();
                counter = countdata.Count();
                conn.Close();
                return counter;

            }
            catch (Exception ex)
            {
                string operation = "Counter";
                string pagename = "Notification";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }
        #endregion

        //#region
        //public int GetCounter()
        //{
        //    return GetCounterRedis();

        //}
        //#endregion

        //#region
        //public int sendmsg(List<Modelnotification> data)
        //{
        //    int count = data.Count;

        //    try
        //    {
        //        RabbitMQBll obj = new RabbitMQBll();
        //        IConnection con = obj.GetConnection();

        //        for (int i = 0; i < data.Count; i++)
        //        {
        //            string message = data[i].VendoreName + "<br/>&nbsp;Payment is pending for order<br/>&nbsp;Date :&nbsp;<strong>" + data[i].Date + "</strong>";
        //            obj.send(con, message, "vendor" + i);
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return count;

        //}
        //#endregion

        //#region
        //public List<string> receive(int count)
        //{
        //    try
        //    {
        //        List<string> MessageList = new List<string>();
        //        RabbitMQBll obj = new RabbitMQBll();
        //        IConnection con = obj.GetConnection();

        //        for (int i = 0; i < count; i++)
        //        {
        //            MessageList.Add(obj.receive(con, "vendor" + i));
        //        }

        //        return MessageList;
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }


        //}
        //#endregion

        //#region
        //public void SetCounter(int count)
        //{

        //    try
        //    {
        //        //var host = ConfigurationManager.AppSettings["host"].ToString();
        //        //var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]); 

        //        var host = "localhost";
        //        var port = Convert.ToInt32(6379);
        //        RedisEndpoint redisendpoint = new RedisEndpoint(host, port);

        //        using (var client = new RedisClient(redisendpoint))
        //        {
        //            _cacheNotificationStack.SetStrings("VendorCounter", count.ToString());
        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}
        //#endregion


        //#region
        //public int Decrementcounter()
        //{
        //    int count;
        //    try
        //    {
        //        //var host = ConfigurationManager.AppSettings["host"].ToString();
        //        //var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]); 

        //        var host = "localhost";
        //        var port = Convert.ToInt32(6379);
        //        RedisEndpoint redisendpoint = new RedisEndpoint(host, port);

        //        using (var client = new RedisClient(redisendpoint))
        //        {

        //            count = Convert.ToInt32(client.Decrement("VendorCounter", 1));

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }

        //    return count;

        //}
        //#endregion




        //#region
        //public int GetCounterRedis()
        //{
        //    int counter;
        //    try
        //    {
        //        //var host = ConfigurationManager.AppSettings["host"].ToString();
        //        //var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]); 

        //        var host = "localhost";
        //        var port = Convert.ToInt32(6379);
        //        RedisEndpoint redisendpoint = new RedisEndpoint(host, port);

        //        using (var client = new RedisClient(redisendpoint))
        //        {
        //            counter = Convert.ToInt32(_cacheNotificationStack.GetStrings("VendorCounter"));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        counter = 0;
        //    }
        //    return counter;
        //}
        //#endregion

        //#region
        //public void Delq(string id)
        //{
        //    int count = GetCounterRedis();
        //    try
        //    {

        //        RabbitMQBll obj = new RabbitMQBll();
        //        IConnection con = obj.GetConnection();

        //        obj.deleteq(con, id);
        //        // SetCounter(0); 
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //#endregion
    }
}
