using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LedgerAng.Models;

namespace LedgerAng.Controllers
{
    public class PurchaseController : ApiController
    {
        // GET api/<controller>


        public SelectItemsVM GetAllCustomers()
        {

            //Connet to db
            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            SelectItemsVM customers = new SelectItemsVM();
            customers.SelectItemsVMList = new List<SelectItemVM>();

            // Get the customers
            var oCust = from c in estEnt.Customers
                        orderby c.LastName
                        select new { c.LastName, c.FirstName, c.CustomerID };

            customers.SelectItemsVMList.Add(new SelectItemVM { Text = "-- Choose Customer --", Value = "0" });


            foreach (var o in oCust)
            {
                customers.SelectItemsVMList.Add(new SelectItemVM { Text = o.LastName + ", " + o.FirstName, Value = o.CustomerID.ToString() });
            }

            return customers;
        }

        public IHttpActionResult GetCustomer(int id)
        {
            //Connet to db
            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            var o = (from c in estEnt.Customers
                     where c.CustomerID == id
                     join b in estEnt.CustomerBalances on c.CustomerID equals b.CustomerID into joinedT
                     from b in joinedT.DefaultIfEmpty()
                     select new
                     {
                         City = c.City,
                         Email = c.Email,
                         FirstName = c.FirstName,
                         LastName = c.LastName,
                         Phone = c.Phone,
                         State = c.State,
                         Street1 = c.Street1,
                         Street2 = c.Street2,
                         Zip = c.Zip,
                         Amount = b.Balance == null ? 0 : (decimal)b.Balance
                     }).FirstOrDefault();



            CustomerInfo oCustInfo = new CustomerInfo() { FirstName = o.FirstName, LastName = o.LastName, Street1 = o.Street1, Street2 = o.Street2, City = o.City, State = o.State, Zip = o.Zip, Phone = o.Phone, Email = o.Email, Balance = o.Amount };

            return Ok(oCustInfo);
        }


        public IHttpActionResult UpdateCustomerBalance(PurchaseData oPurch)
        {//PurchaseData oPurch
           
            //Connet to db
            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            int newBalance = 0;


            var oBal = (from b in estEnt.CustomerBalances
                        where b.CustomerID == oPurch.CustomerID
                        select b).FirstOrDefault();

            if (oBal != null)
            {
                oBal.Balance += Convert.ToDecimal(oPurch.Amount);

                newBalance = Convert.ToInt32(oBal.Balance); 

                oBal.DateUpdated = DateTime.Now;

                // Insert Transaction

                Transaction oTran = new Transaction() { AddedBy = "Admin", CustomerID = oPurch.CustomerID, Amount = oPurch.Amount, LocationID = oPurch.LocationID, TransactionDate = DateTime.Now };

                estEnt.Transactions.Add(oTran);

                estEnt.SaveChanges();
            }
            else
            {
                CustomerBalance oCust = new CustomerBalance() { Balance = oPurch.Amount, CustomerID = oPurch.CustomerID, DateUpdated = DateTime.Now, UpdatedBy = "LR" };
                estEnt.CustomerBalances.Add(oCust);

                Transaction oTran = new Transaction() { AddedBy = "Admin", CustomerID = oPurch.CustomerID, Amount = oPurch.Amount, LocationID = oPurch.LocationID, TransactionDate = DateTime.Now };
                estEnt.Transactions.Add(oTran);

                newBalance = Convert.ToInt32(oPurch.Amount); 

                estEnt.SaveChanges();
            }

            return Ok(newBalance);
        }
    }

}