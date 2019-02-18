using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.MortgageService
{
    public class CreditCardMasterMaintenanceRequest
    {
        public CreditCardMasterMaintenanceRequest()
        {
            instfee = " ";
            Status = "";
            member = "";
            user = "Web User";
            sysdate = DateTime.Now.ToString("yyyyMMdd");
            systime = DateTime.Now.ToString("HHmmss");
        }
        public string routingid { get; set; }
        public string project { get; set; }
        public string account { get; set; }
        public string name { get; set; }
        public string autorization { get; set; }
        public string cardtype { get; set; }
        public string merchantid { get; set; }
        public decimal amount { get; set; }
        public decimal reservation { get; set; }
        public string settled { get; set; }
        public string arda { get; set; }
        public string instfee { get; set; }
        public string instplan { get; set; }
        public string retrncd { get; set; }
        public string Status { get; set; }
        public string member { get; set; }
        public string user { get; set; }
        public string sysdate { get; set; }
        public string systime { get; set; }
    }
}