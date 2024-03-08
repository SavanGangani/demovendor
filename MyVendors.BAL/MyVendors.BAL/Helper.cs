using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVendors.BAL
{
    //helper
    public class Helper
    {
        protected NpgsqlConnection conn = new NpgsqlConnection("Server=cipg01;Port=5432;Database=myvendors;User Id = postgres;Password=123456");
        protected NpgsqlConnection connection = new NpgsqlConnection("Server=cipg01;Port=5432;Database=myvendors;User Id = postgres;Password=123456");
    }
}
