using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.Models
{
    public class ModelLogger
    {
        public void WrieLog(string page, string operation, int uid)
        {
            //int uid = Convert.ToInt32(GlobalContext.Properties["SessionID"]);
            string logPath = HttpContext.Current.Server.MapPath("~/ErrorManagementfile/Logger.txt");
            using (StreamWriter writer = new StreamWriter(logPath, true))
            {
                var date = DateTime.Now;
                writer.WriteLine(uid + " : " + operation + " : " + page + " : " + date);
                writer.Close();
            }
        }
    }
}
