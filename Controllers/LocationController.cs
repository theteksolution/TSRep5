using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LedgerAng.Models;

namespace LedgerAng.Controllers
{
    public class LocationController : ApiController
    {
        // GET api/<controller>
        public SelectItemsVM Get()
        {

            //Connet to db
            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            SelectItemsVM locations = new SelectItemsVM();
            locations.SelectItemsVMList = new List<SelectItemVM>();

            // Get the customers
            var oCust = from c in estEnt.Locations
                        orderby c.LocationName
                        select new { c.LocationName, c.LocationID };

            locations.SelectItemsVMList.Add(new SelectItemVM { Text = "-- Choose Location --", Value = "0" });
            foreach (var o in oCust)
            {
                locations.SelectItemsVMList.Add(new SelectItemVM { Text = o.LocationName, Value = o.LocationID.ToString() });
            }

            return locations;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {

            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();

            LocationInfo oLocInfo = new LocationInfo();

            var oLocs = (from l in estEnt.Locations
                         where l.LocationID == id
                         select new { l.LocationName, l.Street1, l.Street2, l.City, l.State, l.Zip }).FirstOrDefault();

            if (oLocs != null)
            {
                oLocInfo.City = oLocs.City;
                oLocInfo.LocationName = oLocs.LocationName;
                oLocInfo.State = oLocs.State;
                oLocInfo.Street1 = oLocs.Street1;
                oLocInfo.Street2 = oLocs.Street2;
                oLocInfo.Zip = oLocs.Zip;
            }

            return Ok(oLocInfo);
        }

        // POST api/<controller>
        public IHttpActionResult UpdateLocation(LocationInfo oLocInfo)
        {
            // Connect to db
            EarthSkyTimeEntities1 estEnt = new EarthSkyTimeEntities1();
            int iReturnID = 0;

            // Add a new location
            if (oLocInfo.LocationID == 0)
            {
                Location oLoc = new Location() { City = oLocInfo.City, LocationName = oLocInfo.LocationName, State = oLocInfo.State, Street1 = oLocInfo.Street1, Street2 = oLocInfo.Street2, Zip = oLocInfo.Zip };
                estEnt.Locations.Add(oLoc);
                estEnt.SaveChanges();
                iReturnID = oLoc.LocationID;

            }
            else
            {
                // Update existing location
                var oLoc = (from l in estEnt.Locations
                            where l.LocationID == oLocInfo.LocationID
                            select l).FirstOrDefault();
                if (oLoc != null)
                {
                    oLoc.City = oLocInfo.City;
                    oLoc.LocationName = oLocInfo.LocationName;
                    oLoc.State = oLocInfo.State;
                    oLoc.Street1 = oLocInfo.Street1;
                    oLoc.Street2 = oLocInfo.Street2;
                    oLoc.Zip = oLocInfo.Zip;
                    estEnt.SaveChanges();
                    iReturnID = oLoc.LocationID;
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