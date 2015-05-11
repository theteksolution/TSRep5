using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LedgerAng.Models;

namespace LedgerAng.Controllers
{
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public SelectItemsVM Get()
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

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {

            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            CustomerDetail oCustInfo = new CustomerDetail();


            // Get the customer info
            var oC = (from c in estEnt.Customers
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
                          Amount = b.Balance == null ? 0 : (decimal)b.Balance,
                          CustomerID = c.CustomerID
                      }).FirstOrDefault();

            if (oC != null)
            {
                oCustInfo.City = oC.City;
                oCustInfo.Email = oC.Email;
                oCustInfo.FirstName = oC.FirstName;
                oCustInfo.LastName = oC.LastName;
                oCustInfo.Phone = oC.Phone;
                oCustInfo.State = oC.State;
                oCustInfo.Street1 = oC.Street1;
                oCustInfo.Street2 = oC.Street2;
                oCustInfo.Zip = oC.Zip;
            }

         
            return Ok(oCustInfo);
        }

        // POST api/<controller>
        public IHttpActionResult UpdateCustomerDetail(CustomerDetail oCustInfo)
        {
            EarthSkyTimeEntities1 estEntity = new EarthSkyTimeEntities1();
            int iReturnID = 0;

            if (oCustInfo.CustomerID == 0)
            {
                Customer oCust = new Customer()
                {
                    AddedBy = "Admin",
                    City = oCustInfo.City,
                    FirstName = oCustInfo.FirstName,
                    LastName = oCustInfo.LastName,
                    Phone = oCustInfo.Phone,
                    State = oCustInfo.State,
                    Street1 = oCustInfo.Street1,
                    Street2 = oCustInfo.Street2,
                    Zip = oCustInfo.Zip,
                    Email = oCustInfo.Email
                };


                estEntity.Customers.Add(oCust);
                estEntity.SaveChanges();

                iReturnID = oCust.CustomerID;

                // Setup their Balance information
                CustomerBalance oBal = new CustomerBalance() { Balance = 0, CustomerID = oCust.CustomerID, DateUpdated = DateTime.Now, UpdatedBy = "Admin" };
                estEntity.CustomerBalances.Add(oBal);
                estEntity.SaveChanges();
            }
            else
            {
                // Update the customer
                var oC = (from c in estEntity.Customers
                          where c.CustomerID == oCustInfo.CustomerID
                          select c).FirstOrDefault();

                if (oC != null)
                {
                    oC.City = oCustInfo.City;
                    oC.FirstName = oCustInfo.FirstName;
                    oC.LastName = oCustInfo.LastName;
                    oC.Phone = oCustInfo.Phone;
                    oC.State = oCustInfo.State;
                    oC.Street1 = oCustInfo.Street1;
                    oC.Street2 = oCustInfo.Street2;
                    oC.Zip = oCustInfo.Zip;
                    oC.Email = oCustInfo.Email;

                    estEntity.SaveChanges();

                    iReturnID = oCustInfo.CustomerID;
                }
            }

            return Ok(iReturnID);

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