using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Payments
{
    [Serializable]
    public class AccountDetailsForACH
    {
       public string accountNumber { get; set; }
       public int projNumber{ get; set; }
        public decimal paymentamount { get; set; }
        public string merchID { get; set; }
    }
}