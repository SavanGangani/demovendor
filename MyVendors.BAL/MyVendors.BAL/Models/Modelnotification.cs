using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    [Table("t_order"), Schema("public")]
    public class Modelnotification
    {
        [Key]
        public int OrderId { get; set; }
        public string VendoreName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime Date { get; set; }

    }
}
