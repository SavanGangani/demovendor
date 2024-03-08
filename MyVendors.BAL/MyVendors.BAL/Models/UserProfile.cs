using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class UserProfile
    {
        public int Cid { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ProfilePhoto { get; set; }
        public dynamic request { get; set; }
    }
}
