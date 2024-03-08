using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class Modelbankdetails
    {
        [Key]
        public int c_VendorId { get; set; }

        public string c_VendorName { get; set; }

        public string c_GstNo { get; set; }

        public string c_TaxPreferences { get; set; }

        public string c_BillAmount { get; set; }

        public int c_NoOfServices { get; set; }

        public string c_BillNo { get; set; }

        [Key]
        public int c_BankId { get; set; }

        public string c_BankName { get; set; }

        public string c_IFSCCode { get; set; }

        public string c_AccountNo { get; set; }

        public string c_AccountHolderName { get; set; }
    }

}
