using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyVendors.BAL.Models;
using MyVendors.BAL.AuthHelper;

namespace MyVendors.API.Controllers
{
    public class TransactionAPIController : ApiController
    {
        TransactionHelper th = new TransactionHelper();
        // GET: api/TransactionAPI
        public IEnumerable<Transaction> Get()
        {
            return th.GetAll();
        }

        // GET: api/TransactionAPI/5
        //[Route("api/TransactionAPI/GetOne")]
        //[AcceptVerbs("GET")]
        //public List<Transaction> GetOne(int id)
        //{
        //    return th.GetOneTransaction(id);
        //}

        // POST: api/TransactionAPI
        public bool Post(List<Transaction> value)
        {
            return th.InsertOrder(value);
        }

        // PUT: api/TransactionAPI/5
        //public int Put(List<Transaction> value)
        //{
        //    int updated = th.UpdateServiceTransaction(value);
        //    return updated;
        //}

        //// DELETE: api/ApiQuestion/5
        //public bool Delete(Transaction data)
        //{
        //    th.Delete(data.TransactionId);
        //    return true;
        //}

    }
}
