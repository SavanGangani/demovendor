using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class ModelApprovalStatus
    {
        public int Vid { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string PdfName { get; set; }
    }

}
