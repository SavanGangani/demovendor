using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL.Models
{
    public class Subcategories
    {
        //Start:10/3/2022, Added By : Henal Patel , Description : Logic for model Sub_Category. 

        public int c_sub_Id { get; set; }
        public int c_Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ActivatedDate { get; set; }
        public string C_status { get; set; }
        public string CategoryName { get; set; }
    }

}
