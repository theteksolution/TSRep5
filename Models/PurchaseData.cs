using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerAng.Models
{
    public class PurchaseData
    {
        public int CustomerID { get; set; }
        public int Amount {get; set;}
        public int LocationID { get; set; }
    }
}