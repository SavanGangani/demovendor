using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.AuthHelper
{
    public class BALErrormanag : Helper
    {
        public void InsertErrorLog(string operation, string pagename, DateTime date, string error)
        {

            var path = HttpContext.Current.Server.MapPath("~/ErrorManagementfile/Errormanag.txt");
            FileStream filestream = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter switer = new StreamWriter(filestream);
            switer.WriteLine("\n" + operation + "\t\t|\t" + pagename + "\t\t|\t" + date + "\t|\t" + error);
            switer.Flush();
            switer.Close();
        }
    }

}
