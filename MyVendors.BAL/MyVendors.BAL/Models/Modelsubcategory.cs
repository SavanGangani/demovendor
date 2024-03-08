using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class Modelsubcategory
    {
        [Key]
        public int c_SubId { get; set; }

        public string c_SubName { get; set; }
    }
}
