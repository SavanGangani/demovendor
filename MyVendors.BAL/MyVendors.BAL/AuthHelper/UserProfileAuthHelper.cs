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
    public class UserProfileAuthHelper : Helper
    {
        BALErrormanag er = new BALErrormanag();
        UserProfile data = new UserProfile();

        public UserProfile GetOne(int id)
        {
            UserProfile users = null;
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM t_user where c_id="+id+"", conn);
                NpgsqlDataReader dar = cmd.ExecuteReader();
                if (dar.HasRows)
                {
                    dt.Load(dar);
                }
                string path = HttpContext.Current.Server.MapPath("~/Images/");
                string FileName = null;
                if (Directory.Exists(path))
                {
                    DirectoryInfo hd = new DirectoryInfo(path);

                    FileInfo[] filesInDir = hd.GetFiles(id.ToString() + "*.*");
                    if (!(filesInDir.Count() == 0))
                    {
                        FileName = id.ToString() + filesInDir[0].Extension;
                        path = Path.Combine(path, FileName);
                    }

                    if (!File.Exists(path))
                    {
                        FileName = null;
                    }
                }
                else
                {
                    FileName = null;
                }
                users = (from DataRow dr in dt.Rows
                         where int.Parse(dr["c_id"].ToString()) == id
                         select new UserProfile()
                         {
                             Fname = dr["c_fname"].ToString(),
                             Lname = dr["c_lname"].ToString(),
                             Email = dr["c_email"].ToString(),
                             MobileNo = dr["c_mobile"].ToString(),
                             ProfilePhoto = FileName
                         }).ToList().FirstOrDefault();
                conn.Close();
            }
            catch (Exception ex)
            {
                string operation = "Select";
                string pagename = "UserProfile";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
            }
            return users;
        }

        public int Update(UserProfile up, int id)
        {
            try
            {
                NpgsqlCommand cmd;

                if (up.MobileNo == null)
                {
                    cmd = new NpgsqlCommand("UPDATE t_user SET c_fname = @Fname, c_lname = @Lname WHERE c_id = @Cid", conn);
                    cmd.Parameters.AddWithValue("@Cid", id);
                    cmd.Parameters.AddWithValue("@Fname", up.Fname);
                    cmd.Parameters.AddWithValue("@Lname", up.Lname);
                }
                else
                {
                    cmd = new NpgsqlCommand("UPDATE t_user SET c_fname = @Fname, c_lname = @Lname, c_mobile = @MobileNo WHERE c_id = @Cid", conn);
                    cmd.Parameters.AddWithValue("@Cid", id);
                    cmd.Parameters.AddWithValue("@Fname", up.Fname);
                    cmd.Parameters.AddWithValue("@Lname", up.Lname);
                    cmd.Parameters.AddWithValue("@MobileNo", up.MobileNo);
                }

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return 1;
            }
            catch (Exception ex)
            {
                string operation = "Update";
                string pagename = "UserProfile";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return -1;
            }
        }

        public int AddImage(ImageModel data)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Images/");
                HttpPostedFile postedFile = data.request.Files[0];
                string fileName2 = data.id.ToString() + "." + postedFile.ContentType.Split('/')[1];
                fileName2 = Path.Combine(path, fileName2);
                postedFile.SaveAs(fileName2);
                return 1;
            }
            catch (Exception ex)
            {
                string operation = "Add Image";
                string pagename = "UserProfile";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return -1;
            }
        }

        public int RemoveImage(int id)
        {

            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Images/");
                DirectoryInfo hd = new DirectoryInfo(path);
                FileInfo[] filesInDir = hd.GetFiles(id.ToString() + "*.*");
                string extension = filesInDir[0].Extension;
                string newFileName = id.ToString() + extension;


                if (Directory.Exists(path))
                {
                    if (File.Exists(Path.Combine(path, newFileName)))
                    {
                        File.Delete(Path.Combine(path, newFileName));
                    }
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string operation = "Remove Image";
                string pagename = "UserProfile";
                DateTime date = DateTime.Now.Date;
                er.InsertErrorLog(operation, pagename, date, ex.Message);
                return -1;
            }
        }
    }
}
