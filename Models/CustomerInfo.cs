using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerAng.Models
{
    public class CustomerInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}