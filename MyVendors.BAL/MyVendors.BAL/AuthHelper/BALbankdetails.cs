using MyVendors.BAL.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class BALbankdetails : Helper
    {
        Encrypt_Decrypt ende = new Encrypt_Decrypt();
        BALErrormanag er = new BALErrormanag();
        #region
        public int AddBank(Modelbankdetails data)
        {
            try
            {
                Random n = new Random();
                string BillNumber = n.Next().ToString();
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand(@"UPDATE t_vendormaster SET
                                                    c_gst_no=@gstno,c_tax_preference=@txp,
                                                    c_bill_amount=@billamount,c_noofservices=@noservice,
                                                    c_billno=@billno WHERE c_vid=@cvid", conn);

                NpgsqlCommand cmd1 = new NpgsqlCommand(@"INSERT INTO t_bankdetails(c_vid,c_account_name,c_ifsc_code,c_account_number,c_bank_name) 
                                                       VALUES(@cvid,@aname,@code,@ano,@bname)", conn);

                cmd.Parameters.AddWithValue("@cvid", data.c_VendorId);
                cmd.Parameters.AddWithValue("@gstno", data.c_GstNo);
                cmd.Parameters.AddWithValue("@txp", data.c_TaxPreferences);
                cmd.Parameters.AddWithValue("@billamount", data.c_BillAmount);
                cmd.Parameters.AddWithValue("@noservice", data.c_NoOfServices);
                cmd.Parameters.AddWithValue("@billno", BillNumber.ToString());

                cmd1.Parameters.AddWithValue("@cvid", data.c_VendorId);
                cmd1.Parameters.AddWithValue("@aname", data.c_AccountHolderName);
                cmd1.Parameters.AddWithValue("@code", data.c_IFSCCode);
                cmd1.Parameters.AddWithValue("@ano", ende.Encryptpassword(data.c_AccountNo));
                cmd1.Parameters.AddWithValue("@bname", data.c_BankName);

                cmd1.ExecuteNonQuery();

                int updated = cmd.ExecuteNonQuery();
                conn.Close();
                if (updated > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception ex)
            {
                string operation = "Insert";
                string pagename = "Vendor";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return 0;
            }

        }
        #endregion
    }
}
