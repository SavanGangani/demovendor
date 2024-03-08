using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class ResetPass
    {
        [Required(ErrorMessage = "The field should not be empty")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "The field should not be empty")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The field should not be empty")]
        public string ConfirmPassword { get; set; }
    }
}
