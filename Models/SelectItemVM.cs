﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LedgerAng.Models
{
    public class SelectItemVM
    {
        public string Text {get; set;}
        public string Value { get; set; }

    }

    public class SelectItemsVM
    {
        public List<SelectItemVM> SelectItemsVMList { get; set; }
    }
}