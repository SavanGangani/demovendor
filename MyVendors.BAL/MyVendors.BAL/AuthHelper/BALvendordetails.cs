using MyVendors.BAL.Models;
using Npgsql;
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
    public class BALvendordetails : Helper
    {
        //Start:8/3/2022, Added By : Dhrupti Thesiya , Description : build logic for add vendor details and pdf upload
        Encrypt_Decrypt end = new Encrypt_Decrypt();
        BALErrormanag er = new BALErrormanag();

        #region
        public List<Modelvendoradd> GetAll()
        {
            List<Modelvendoradd> VendorList = new List<Modelvendoradd>();
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT c_vid,c_status,c_vname,c_mobile,c_email,c_address,
                                                    c_website,c_gst_no,c_createddate,c_bank_name,c_ifsc_code,c_account_no,c_account_holder_name,c_subcategory_id,c_cat_id 
                                                    FROM t_vendormaster where c_status = '1'", conn);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dt.Load(dr);

                }





                VendorList = (from DataRow drData in dt.Rows
                              select new Modelvendoradd()
                              {
                                  c_VendorId = Convert.ToInt32(drData["c_vid"].ToString()),
                                  //c_CatName = drData["c_name"].ToString(),
                                  //c_SubId = new int[drData["c_sub_id"].ToString().Length],
                                  c_IdArray = ((int[])drData["c_cat_id"]),
                                  c_SubId = ((int[])drData["c_subcategory_id"]),
                                  //c_SubId = new int[drData["c_subcategory_id"].ToString().Length],
                                  c_VendorName = drData["c_vname"].ToString(),
                                  c_Mobile = drData["c_mobile"].ToString(),
                                  c_Email = drData["c_email"].ToString(),
                                  c_Address = drData["c_address"].ToString(),
                                  c_Website = drData["c_website"].ToString(),
                                  c_GstNo = drData["c_gst_no"].ToString(),
                                  c_BankName = drData["c_bank_name"].ToString(),
                                  c_IFSCCode = drData["c_ifsc_code"].ToString(),
                                  c_AccountNo = drData["c_account_no"].ToString(),
                                  c_AccountHolderName = drData["c_account_holder_name"].ToString()
                              }).ToList();

                conn.Close();
                return VendorList;

            }
            catch (Exception ex)
            {

                string operation = "Select ALL";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return VendorList;
            }

        }
        #endregion

        #region
        public bool IsVendor(Modelvendoradd vendor)
        {
            try
            {

                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT c_email 
                                                       FROM t_vendormaster 
                                                       WHERE c_email=@email", conn);
                cmd.Parameters.AddWithValue("@email", vendor.c_Email);
                conn.Open();
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
                string operation = "Select";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }

        }

        public bool InsertVendors(Modelvendoradd vendordata)
        {
            try
            {
                if (IsVendor(vendordata))
                {
                    return false;
                }
                else
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(@"insert into t_vendormaster(c_status,c_vname,c_mobile,c_email,c_address,
                                                        c_website,c_gst_no,c_createddate,c_bank_name,c_ifsc_code,c_account_no,c_account_holder_name,c_subcategory_id,c_cat_id) 
                                                        VALUES('1', @c_VendorName, @c_Mobile, @c_Email, @c_Address, @c_Website,
                                                        @c_GstNo, @vcreateddate, @c_BankName, @c_IFSCCode, @c_AccountNo, @c_AccountHolderName,@c_SubcatId,@c_catId); ", conn);
                    cmd.Parameters.AddWithValue("@c_VendorName", vendordata.c_VendorName);
                    cmd.Parameters.AddWithValue("@c_Mobile", vendordata.c_Mobile);
                    cmd.Parameters.AddWithValue("@c_Email", vendordata.c_Email);
                    cmd.Parameters.AddWithValue("@c_Address", vendordata.c_Address);
                    cmd.Parameters.AddWithValue("@c_Website", vendordata.c_Website);
                    cmd.Parameters.AddWithValue("@c_GstNo", vendordata.c_GstNo);
                    cmd.Parameters.AddWithValue("@vcreateddate", DateTime.Now.Date);
                    cmd.Parameters.AddWithValue("@c_BankName", vendordata.c_BankName);
                    cmd.Parameters.AddWithValue("@c_IFSCCode", vendordata.c_IFSCCode);
                    cmd.Parameters.AddWithValue("@c_AccountNo", vendordata.c_AccountNo);
                    cmd.Parameters.AddWithValue("@c_AccountHolderName", vendordata.c_AccountHolderName);
                    cmd.Parameters.AddWithValue("@c_SubcatId", vendordata.c_SubId);
                    cmd.Parameters.AddWithValue("@c_catId", vendordata.c_IdArray);


                    conn.Open();
                    int id = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (id > 0)
                        return true;
                    else
                        return false;
                }

                return true;

            }
            catch (Exception ex)
            {
                string operation = "Insert";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }

        public bool DeleteVenodr(int id)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE t_vendormaster SET c_status='0' WHERE c_vid = @id;", conn);
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
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }
        #endregion

        public bool UpdateVendors(Modelvendoradd vendordata)
        {
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"update t_vendormaster set c_vname=@c_VendorName,c_mobile=@c_Mobile,c_email=@c_Email,c_address=@c_Address,c_website=@c_Website,
                                        c_gst_no=@c_GstNo,c_bank_name=@c_BankName,c_ifsc_code=@c_IFSCCode,c_account_no=@c_AccountNo,c_account_holder_name=@c_AccountHolderName ,c_subcategory_id=@c_SubcatId,c_cat_id=@c_catId where c_vid=@c_VendorId", conn);
                //NpgsqlCommand cmd = new NpgsqlCommand(@"update t_vendormaster v,t_bankdetails b set v.c_vname=@c_VendorName,v.c_mobile=@c_Mobile,v.c_email=@c_Email,
                //                                        v.c_address=@c_Address,v.c_website=@c_Website,b.c_account_name=@c_AccountHolderName,b.c_ifsc_code=@c_IFSCCode,
                //                                        b.c_account_number=@c_AccountNo,b.c_bank_name=@c_BankName where v.c_vid=@c_VendorId", conn);


                cmd.Parameters.AddWithValue("@c_VendorName", vendordata.c_VendorName);
                cmd.Parameters.AddWithValue("@c_Mobile", vendordata.c_Mobile);
                cmd.Parameters.AddWithValue("@c_Email", vendordata.c_Email);
                cmd.Parameters.AddWithValue("@c_Address", vendordata.c_Address);
                cmd.Parameters.AddWithValue("@c_Website", vendordata.c_Website);
                cmd.Parameters.AddWithValue("@c_GstNo", vendordata.c_GstNo);
                cmd.Parameters.AddWithValue("@c_BankName", vendordata.c_BankName);
                cmd.Parameters.AddWithValue("@c_IFSCCode", vendordata.c_IFSCCode);
                cmd.Parameters.AddWithValue("@c_AccountNo", vendordata.c_AccountNo);
                cmd.Parameters.AddWithValue("@c_AccountHolderName", vendordata.c_AccountHolderName);
                cmd.Parameters.AddWithValue("@c_VendorId", vendordata.c_VendorId);
                cmd.Parameters.AddWithValue("@c_SubcatId", vendordata.c_SubId);
                cmd.Parameters.AddWithValue("@c_catId", vendordata.c_IdArray);

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
                string operation = "Update";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return false;
            }
        }
    }
}
