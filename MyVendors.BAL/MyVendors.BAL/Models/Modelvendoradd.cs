using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    //Start:8/3/2022, Added By : Dhrupti Thesiya , Description : model for add vendor details

    public class Modelvendoradd
    {
        public int c_VendorId { get; set; }

        //public int c_Id { get; set; }

        public int[] c_IdArray { get; set; }

        public string c_CatName { get; set; }

        public int[] c_SubId { get; set; }

        public string c_VendorName { get; set; }

        public string c_Mobile { get; set; }

        public string c_Email { get; set; }

        public string c_Address { get; set; }

        public string c_Website { get; set; }

        public string c_GstNo { get; set; }

        public string c_BankName { get; set; }

        public string c_IFSCCode { get; set; }

        public string c_AccountNo { get; set; }

        public string c_AccountHolderName { get; set; }

    }

}
