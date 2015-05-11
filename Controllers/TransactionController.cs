using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LedgerAng.Models;
namespace LedgerAng.Controllers
{
    public class TransactionController : ApiController
    {
        // GET api/<controller>
        [Route("api/GetCustomerTransactions/{dtTo}/{dtFrom}/{LocationID}")]
        [HttpGet]
        public CustomerTransactions Get(string dtTo, string dtFrom, string LocationID)
        {

            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            CustomerTransactions transactions = new CustomerTransactions();
            transactions.CustomerTransactionList = new List<CustomerTransaction>();


            // get all transactions
            var oTrans = from t in estEnt.Transactions
                         join c in estEnt.Customers on t.CustomerID equals c.CustomerID
                         join l in estEnt.Locations on t.LocationID equals l.LocationID into joinedT
                         from l in joinedT.DefaultIfEmpty()
                         orderby t.TransactionDate descending
                         select new { t.Amount, t.TransactionDate, c.FirstName, c.LastName, l.LocationName, l.City, l.State, LocationID = l.LocationID == null ? 0 : l.LocationID };



            
                if (dtFrom != "undefined" && dtFrom != "null")
                {
                    
                    
                        DateTime dFrom = Convert.ToDateTime(dtFrom);
                        oTrans = oTrans.Where(m => m.TransactionDate >= dFrom);
                   
                }

                if (dtTo != "undefined" && dtTo != "null")
                {
                    
                        DateTime dTo = Convert.ToDateTime(dtTo).AddDays(1);

                        oTrans = oTrans.Where(m => m.TransactionDate <= dTo);
                   
                }

                if (LocationID != "undefined")
                {
                    int iLocation = Convert.ToInt32(LocationID);
                    if (iLocation > 0)
                    {
                        oTrans = oTrans.Where(m => m.LocationID == iLocation);
                    }
                }
            
            
            
            
            foreach (var t1 in oTrans)
            {
                string sLoc = t1.LocationName + " - " + t1.City + ", " + t1.State;
                if (t1.LocationName != null)
                {
                    sLoc = t1.LocationName;

                    if (t1.City != "")
                    {
                        sLoc += " - " + t1.City;
                    }

                    if (t1.State != "")
                    {
                        sLoc += ", " + t1.State;
                    }
                }
                else
                {
                    sLoc = "None";
                }


                transactions.CustomerTransactionList.Add(new CustomerTransaction { Name = t1.LastName + ", " + t1.FirstName, Amount = Convert.ToDecimal(t1.Amount), Location = sLoc,  DateUpdated = Convert.ToDateTime(t1.TransactionDate).ToShortDateString()});
            }

           
            return transactions; 
           
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