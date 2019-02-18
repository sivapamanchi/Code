using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class MaintenanceFeeStatsRequest
    {
        public string Arvact { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CCBillingCity { get; set; }
        public string CCBillingState { get; set; }
        public string CCBillingZip { get; set; }
        public string CCLast4 { get; set; }
        public string TransactionAmt { get; set; }
        public DateTime RequestDateTime { get; set; }
        public Int32 stepnumber { get; set; }
        public Int64 LogID { get; set; }
        public string AuthCode { get; set; }
        public string FailReason { get; set; }
        public string TransactionID { get; set; }
        public Int32 ResponseFlag { get; set; }
        public DateTime GatewayResponseDate { get; set; }
    }
}