using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyVendors.BAL.Models
{
    public class ImageModel
    {
        public int id { get; set; }
        public HttpPostedFile file { get; set; }
        public string path { get; set; }
        public dynamic request { get; set; }
    }
}
