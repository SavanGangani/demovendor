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
    public class DashBoardHelper : Helper
    {
        BALErrormanag er = new BALErrormanag();

        public List<Dashboard> GetOrderGrid()
        {
            List<Dashboard> OrderList = new List<Dashboard>();
            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"select v.c_vname,s.c_sub_name,od.c_qauntity,c_payment_status 
                from t_vendormaster v,t_subcategory s,t_order o, t_orderdetails od
                where od.c_sub_id = s.c_sub_id and od.c_vid = v.c_vid and od.c_order_id = o.c_order_id and o.c_payment_status='Pending'", conn);

                conn.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                OrderList = (from DataRow dr in dt.Rows
                             select new Dashboard()
                             {
                                 Vname = dr["c_vname"].ToString(),
                                 Sname = dr["c_sub_name"].ToString(),
                                 Quanlity = Convert.ToInt32(dr["c_qauntity"]),
                                 Payment_status = dr["c_payment_status"].ToString()

                             }).ToList();
                return OrderList;
            }
            catch (Exception ex)
            {
                string operation = "Order Grid";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return OrderList;
            }
        }


        public List<Dashboard> GetServiceGrid()
        {
            List<Dashboard> ServiceList = new List<Dashboard>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"select v.c_vname,s.c_sub_name,c_payment_status,c_service_status,c_repair
				from t_vendormaster v,t_subcategory s, t_service se, t_servicedetails sd
				where sd.c_sub_id=s.c_sub_id and se.c_vid=v.c_vid and sd.c_order_id = se.c_order_id and se.c_payment_status='Pending'", conn);

                conn.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                ServiceList = (from DataRow dr in dt.Rows
                               select new Dashboard()
                               {
                                   Vname = dr["c_vname"].ToString(),
                                   Sname = dr["c_sub_name"].ToString(),
                                   Payment_status = dr["c_payment_status"].ToString(),
                                   Servicestatus = dr["c_service_status"].ToString(),
                                   TypeRepair = dr["c_repair"].ToString(),
                               }).ToList();
                return ServiceList;
            }
            catch (Exception ex)
            {
                string operation = "Service Grid";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return ServiceList;
            }
        }

        public int getdeshboard()
        {
            try
            {
                NpgsqlCommand com = new NpgsqlCommand("select count(c_vid)  as id FROM public.t_vendormaster", conn);
                conn.Open();
                NpgsqlDataReader datar = com.ExecuteReader();
                if (datar.Read())
                {
                    int count = Convert.ToInt32(datar["id"].ToString());
                    conn.Close();
                    return count;

                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Total Vendor";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }

        }

        public int gettotalorder()
        {
            try
            {
                NpgsqlCommand com = new NpgsqlCommand("select count(c_order_id) as oid FROM public.t_order", conn);
                conn.Open();
                NpgsqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    int count = Convert.ToInt32(dr["oid"].ToString());
                    conn.Close();
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Total Order";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }

        public int gettotalService()
        {
            try
            {
                NpgsqlCommand com = new NpgsqlCommand("select count(c_service_id) as sid FROM public.t_service", conn);
                conn.Open();
                NpgsqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    int count = Convert.ToInt32(dr["sid"].ToString());
                    conn.Close();
                    return count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Total Service";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }
        }

        public List<Dashboard> GetOrderChart()
        {
            List<Dashboard> OrderList = new List<Dashboard>();
            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"select to_char(c_order_date,'DD-MM-YYYY')as month_year, count(*) as c_count 
                                                    from t_order group by month_year;", conn);

                conn.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt.Load(dr1);
                da.Fill(ds, "DashBoard");

                OrderList = (from DataRow dr in dt.Rows
                             select new Dashboard()
                             {
                                 month_year = dr["month_year"].ToString(),
                                 count = Convert.ToInt32(dr["c_count"])


                             }).ToList();
                return OrderList;
            }
            catch (Exception ex)
            {
                string operation = "Order Chart";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return OrderList;
            }

        }

        public List<Dashboard> GetServiceChart()
        {
            List<Dashboard> OrderList = new List<Dashboard>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"select to_char(c_service_date,'DD-MM-YYYY')as month_year, count(*) as c_count
                                                    from t_service group by month_year;", conn);

                conn.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                OrderList = (from DataRow dr in dt.Rows
                             select new Dashboard()
                             {
                                 month_year = dr["month_year"].ToString(),
                                 count = Convert.ToInt32(dr["c_count"])


                             }).ToList();
                return OrderList;
            }
            catch (Exception ex)
            {
                string operation = "Service Chart";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return OrderList;
            }

        }

        public List<Dashboard> GetVendorsChart()
        {
            List<Dashboard> OrderList = new List<Dashboard>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"select to_char(c_createddate,'DD-MM-YYYY')as month_year, count(*) as c_count
                                                    from t_vendormaster group by month_year;", conn);

                conn.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                OrderList = (from DataRow dr in dt.Rows
                             select new Dashboard()
                             {
                                 month_year = dr["month_year"].ToString(),
                                 count = Convert.ToInt32(dr["c_count"])


                             }).ToList();
                return OrderList;
            }
            catch (Exception ex)
            {
                string operation = "Vendor Chart";
                string pagename = "Dashboard";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return OrderList;
            }

        }

    }
}
