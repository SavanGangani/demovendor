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
    public class ResetPassword_logic : Helper
    {
        //Start:22/2/2022, Added By : Dhrupti Thesiya , Description : build logic for reset pasword

        Encrypt_Decrypt ende = new Encrypt_Decrypt();
        BALErrormanag er = new BALErrormanag();
        //#Region

        public int ResetPasswordUser(int id, ResetPass reset)
        {
            if (CheckOldPassword(id, reset.OldPassword))
            {

                if (reset.NewPassword == reset.ConfirmPassword)
                {
                    try
                    {
                        string _enpassword = ende.Encryptpassword(reset.NewPassword);
                        conn.Open();
                        NpgsqlCommand cmd = new NpgsqlCommand(@"Update t_user SET
                                                                         c_password=@password
                                                                         WHERE c_id=@id", conn);
                        cmd.Parameters.AddWithValue("@password", _enpassword);
                        cmd.Parameters.AddWithValue("@id", id);
                        int isrow = cmd.ExecuteNonQuery();
                        if (isrow > 0)
                        {
                            return 1;

                        }
                        else
                        {
                            return 3;
                        }
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        string operation = "Update";
                        string pagename = "Reset Password";
                        DateTime date = DateTime.Now.Date;
                        er.InsertErrorLog(operation, pagename, date, ex.Message);
                        return 0;
                    }
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool CheckOldPassword(int id, string _oldpassword)
        {
            bool _isOld = false;
            try
            {

                string _enoldpass = ende.Encryptpassword(_oldpassword);
                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT *
                                                    FROM t_user
                                                    WHERE  c_password=@password AND c_id=@id", conn);

                cmd.Parameters.AddWithValue("@password", _enoldpass);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    _isOld = true;
                }

                conn.Close();
                return _isOld;
            }
            catch (Exception ex)
            {
                string operation = "Select";
                string pagename = "Reset Password";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return _isOld;
            }
        }

        //#EndRegion
    }

}

