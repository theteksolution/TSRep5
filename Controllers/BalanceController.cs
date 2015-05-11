using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LedgerAng.Models;

namespace LedgerAng.Controllers
{
    public class BalanceController : ApiController
    {
        // GET api/<controller>
        [Route("api/ObtainBalances/{sortOrder}")]
        [HttpGet]
        public Balances ObtainBalances(string sortOrder = "")
        {

            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            Balances balances = new Balances();

            balances.BalanceList = new List<Balance>();


            var oBal = from b in estEnt.CustomerBalances
                       join c in estEnt.Customers on b.CustomerID equals c.CustomerID
                       select new { c.FirstName, c.LastName, b.Balance, b.DateUpdated, c.Email, c.Phone };

            if (sortOrder != "undefined")
            {
                if (sortOrder == "Balance Ascending")
                {
                    oBal = oBal.OrderBy(m => m.Balance);
                }

                if (sortOrder == "Balance Descending")
                {
                    oBal = oBal.OrderByDescending(m => m.Balance);
                }

                if (sortOrder == "Date Ascending")
                {
                    oBal = oBal.OrderBy(m => m.DateUpdated);
                }

                if (sortOrder == "Date Descending")
                {
                    oBal = oBal.OrderByDescending(m => m.DateUpdated);
                }

                if (sortOrder == "Name Ascending")
                {
                    oBal = oBal.OrderBy(m => m.LastName); //.ThenBy(m => m.FirstName);
                }

                if (sortOrder == "Name Descending")
                {
                    oBal = oBal.OrderByDescending(m => m.LastName); //.ThenByDescending(m => m.FirstName);
                }
            }
            else
            {
                oBal = oBal.OrderByDescending(m => m.DateUpdated);
            }


            foreach (var b1 in oBal)
            {
                balances.BalanceList.Add(new Balance { Name = b1.LastName + ", " + b1.FirstName, Amount = Convert.ToDecimal(b1.Balance), Email = b1.Email, DateUpdated = Convert.ToDateTime(b1.DateUpdated).ToShortDateString(), Phone = b1.Phone });
            }

        
            return balances;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}