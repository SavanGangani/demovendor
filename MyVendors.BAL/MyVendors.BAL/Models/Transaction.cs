using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int vendorId { get; set; }
        public int orderId { get; set; }
        public int serviceId { get; set; }
        public int totalItem { get; set; }
        public int itemQty { get; set; }
        public string vname { get; set; }
        public string subCategory { get; set; }
        public int SubCatId { get; set; }
        public string transactionType { get; set; }
        public string deliveryStatus { get; set; }
        public DateTime transactionDate { get; set; }
        public string delivaryStatus { get; set; }
        public string PaymentStatus { get; set; }
        public int TotalPrice { get; set; }
        public DateTime Orderdate { get; set; }
        public string Service { get; set; }
        public string Repair { get; set; }
        public string Notes { get; set; }
        public int ReceviedQauntity { get; set; }
        public string VendorEmail { get; set; }
        public string VendorMobileNo { get; set; }
    }
}
