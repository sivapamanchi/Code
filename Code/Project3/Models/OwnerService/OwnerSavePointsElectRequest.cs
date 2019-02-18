using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BGSitecore.Models.OwnerService.OwnerSavePointsElectRequest
{


    public class CreditCardInfo
    {
        public string CreditCardType { get; set; }
        public string CreditCardNumber { get; set; }
        public string CreditCardExpirationDate { get; set; }
        public string CreditCardCVV { get; set; }
    }

    public class OwnerSavePointsElectRequest
    {
        public string Identifier { get; set; }
        public string NameOnCard { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string MerchantID { get; set; }
        public string ReferenceNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AgentID { get; set; }
        public string Amount { get; set; }
        public CreditCardInfo CreditCardInfo { get; set; }
    }

}