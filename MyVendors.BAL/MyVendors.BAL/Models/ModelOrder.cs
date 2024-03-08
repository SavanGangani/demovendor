using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class ModelOrder
    {
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public int VendorId { get; set; }

        public string VendorName { get; set; }

        public int SubCatId { get; set; }

        public string SubCatName { get; set; }

        public int Quantity { get; set; }

        public string OrderType { get; set; }

        public DateTime OrderDate { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime LastPaymentDate { get; set; }

        public string VendorEmail { get; set; }

        public string VendorNo { get; set; }


    }
}
