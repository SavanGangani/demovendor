using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class ModelService
    {
        public int ServiceId { get; set; }

        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public string VendorEmail { get; set; }

        public string VendorMobile { get; set; }

        public string ServiceStatus { get; set; }

        public int SubCatId { get; set; }

        public string SubCatName { get; set; }

        public bool Repair { get; set; }

        public bool Service { get; set; }

        public string OrderDate { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime ServiceDate { get; set; }

        public string Notes { get; set; }
    }
}
