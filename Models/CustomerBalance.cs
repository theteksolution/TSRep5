//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LedgerAng.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerBalance
    {
        public int CustomerBalanceID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<decimal> Balance { get; set; }
    }
}