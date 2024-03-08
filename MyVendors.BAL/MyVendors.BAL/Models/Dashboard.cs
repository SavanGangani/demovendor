using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class Dashboard
    {
        public int c_order_Id { get; set; }
        public int t_service_Id { get; set; }
        public string Vname { get; set; }
        public string Sname { get; set; }
        public int Quanlity { get; set; }
        public string Payment_status { get; set; }

        public string Servicestatus { get; set; }
        public string TypeRepair { get; set; }

        public string month_year { get; set; }
        public int count { get; set; }
    }
}
