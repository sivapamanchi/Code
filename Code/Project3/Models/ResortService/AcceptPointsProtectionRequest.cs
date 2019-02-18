using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.AcceptPointsProtection
{
    
    public class Payment
    {
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardAuthorization { get; set; }
        public string CreditCardTotal { get; set; }    //THIS WAS CHANGED FROM INT TO DECIMAL
        public string NonTaxTotal { get; set; }           // THIS WAS CHANGED FROM INT TO DECIMAL
    }

    public class AcceptPointsProtectionRequest
    {
        public string SiteName { get; set; }
        public string ReservationNumber { get; set; }
        public Payment Payment { get; set; }
    }

}