using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerAng.Models
{
    public class CustomerTransaction
    {
        public decimal Amount { get; set; }
        public string DateUpdated { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }

    public class CustomerTransactions
    {
        public List<CustomerTransaction> CustomerTransactionList { get; set; }
    }
}