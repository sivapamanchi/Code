using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.Payments
{
    [Serializable]
    public class AccountBalanceInfo
    {
        public string MerchentID { get; set; }
        public string PropertyName { get; set; }
        public string AccountNunmber { get; set; }

        public string SalesType { get; set; }

        public decimal PastDue { get; set; }
        public decimal CurrentDue { get; set; }

        public decimal PaymentAmount { get; set; }
        public decimal ARDAAmount { get; set; }
        
        //need for return value
        public bool IsARDAAmount { get; set; }
        public string AccountHelpContent { get; set; }
    }
}