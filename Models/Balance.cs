using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerAng.Models
{
    public class Balance
    {
        public string Name { get; set; }
        public string DateUpdated { get; set; }
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class Balances
    {
        public List<Balance> BalanceList { get; set; }
    }
}