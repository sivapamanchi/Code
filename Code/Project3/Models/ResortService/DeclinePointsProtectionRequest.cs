using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.ResortService.DeclinePointsProtection
{


    public class Payment
    {
        public string CreditCardNumber { get; set; }
        public string CreditCardType { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardAuthorization { get; set; }
        public int CreditCardTotal { get; set; }
        public int NonTaxTotal { get; set; }
    }

    public class DeclinePointsProtectionRequest
    {
        public string SiteName { get; set; }
        public string ReservationNumber { get; set; }
        public Payment Payment { get; set; }
    }


}