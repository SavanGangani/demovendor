using MyVendors.BAL.AuthHelper;
using MyVendors.BAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyVendors.API.Controllers
{
    public class DashBoardAPIController : ApiController
    {
        DashBoardHelper dbh = new DashBoardHelper();
        // GET: DashBoard_API
        // GET: api/DashBoardAPI

        //OderGrid
        [HttpGet]
        [Route("api/DashBoardAPI/GetOrderGrid")]
        public IEnumerable<Dashboard> Get()
        {
            return dbh.GetOrderGrid();
        }

        //ServiceGrid
        [HttpGet]
        [Route("api/DashBoardAPI/GetServiceGrid")]
        public IEnumerable<Dashboard> GetService()
        {
            return dbh.GetServiceGrid();
        }

        //servicechart
        [HttpGet]
        [Route("api/DashBoardAPI/GetOrderChart")]
        public IEnumerable<Dashboard> GetOrderChart()
        {
            return dbh.GetOrderChart();
        }

        [HttpGet]
        [Route("api/DashBoardAPI/GetServicechart")]
        public IEnumerable<Dashboard> GetServiceChart()
        {
            return dbh.GetServiceChart();
        }

        [HttpGet]
        [Route("api/DashBoardAPI/GetVendorsChart")]
        public IEnumerable<Dashboard> GetVendorsChart()
        {
            return dbh.GetVendorsChart();
        }
    }
}
